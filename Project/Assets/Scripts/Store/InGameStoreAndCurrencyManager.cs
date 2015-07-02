using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;

/// <summary>
/// Class created to handle in-game currency and store systems.
/// </summary>
public class InGameStoreAndCurrencyManager : MonoBehaviour
{
    /// <summary>
    /// Instance reference.
    /// </summary>
    public static InGameStoreAndCurrencyManager instance;

    /// <summary>
    /// Currency amount to give to the player, at game start.
    /// </summary>
    [SerializeField]
    int startingCurrency = 100;

    /// <summary>
    /// Callback list, to handle stats upgrades, after sucessfully buying something.
    /// </summary>
    ProjectDelegates.OnPlayerBoughtStatUpgradeCallback[] OnStatsUpgradesCallback;

    /// <summary>
    /// Catalog containing all the items which can be bought.
    /// </summary>
    List<CatalogItem> catalogItems;

    /// <summary>
    /// Reference to the actual currency amount.
    /// </summary>
    int actualCurrency = 0;

    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Initialization method. Starts in game currency, and loads all catalog items.
    /// </summary>
    public void Initialize()
    {
        this.AddInGameCurrency(startingCurrency);
        this.LoadAllCatalogItems();
    }

    /// <summary>
    /// Sets the callback list that handles stats upgrades.
    /// </summary>
    /// <param name="statsCallbacks">Callbacks list, ordered by enum HashCode</param>
    public void SetUpgradesCallbacks(ProjectDelegates.OnPlayerBoughtStatUpgradeCallback[] statsCallbacks)
    {
        this.OnStatsUpgradesCallback = statsCallbacks;
    }

    /// <summary>
    /// Sends request to PlayFab, to load the catalog.
    /// </summary>
    public void LoadAllCatalogItems()
    {
        GetCatalogItemsRequest request = new GetCatalogItemsRequest();
        request.CatalogVersion = GameConstants.inGameStoreCatalogName;
        PlayFabClientAPI.GetCatalogItems(request, OnCatalogItemsLoaded, OnCatalogItemsError);
    }

    /// <summary>
    /// Callback called when PlayFab request completes.
    /// </summary>
    /// <param name="result">Catalog list.</param>
    void OnCatalogItemsLoaded(GetCatalogItemsResult result)
    {
        catalogItems = result.Catalog;

        GameplayUI.instance.CreateStoreButtons(this.GetCatalog());
    }

    /// <summary>
    /// Callback called when some error occurs when trying to load catalog items.
    /// </summary>
    /// <param name="error">Error information.</param>
    void OnCatalogItemsError(PlayFabError error)
    {
        Debug.LogError("Error loading ingame store items: " + error.Error + " " + error.ErrorMessage);
    }

    /// <summary>
    /// Gets the catalog list, if populated.
    /// </summary>
    /// <returns>Catalog list.</returns>
    public List<CatalogItem> GetCatalog()
    {
        if (catalogItems == null) 
            Debug.LogWarning("Trying to get catalog before it's loaded. Call Initialize() before, or wait a few seconds.");

        return catalogItems;
    }

    /// <summary>
    /// Starts the process of buying something from the store.
    /// </summary>
    /// <param name="item">Item to be bought.</param>
    public void BuyItemWithVirtualCurrency(CatalogItem item)
    {
        if (OnStatsUpgradesCallback == null)
        {
            Debug.LogWarning("Set all the necessary callbacks using SetUpgradesCallbacks() before starting a purchase");
            return;
        }

        PurchaseItemRequest request = new PurchaseItemRequest();
        request.ItemId = item.ItemId;
        request.Price = (int)item.VirtualCurrencyPrices[GameConstants.inGameCurrencyID];
        request.VirtualCurrency = GameConstants.inGameCurrencyID;

        PlayFabClientAPI.PurchaseItem(request, OnPurchaseSuccess, OnPurchaseError);
    }

    /// <summary>
    /// Callback called when the user successfully buys something from the in-game store.
    /// </summary>
    /// <param name="result">Transaction result.</param>
    void OnPurchaseSuccess(PurchaseItemResult result)
    {
        GameplaySFXManager.instance.PlayItemUseSound();

        for (int i = 0; i < result.Items.Count; i++)
        {
            this.ConsumeItem(result.Items[i].ItemInstanceId);
            this.actualCurrency -= (int)result.Items[i].UnitPrice;

            string[] itemInfo = result.Items[i].ItemId.Split('-');

            PlayerStats statBought = (PlayerStats)System.Enum.Parse(typeof(PlayerStats), itemInfo[0]);
            float multiplier = float.Parse(itemInfo[1]);

            Debug.Log(statBought + " " + multiplier);

            GameplayUI.instance.UpdateCurrencyText(actualCurrency);

            OnStatsUpgradesCallback[statBought.GetHashCode()](multiplier);
        }
    }

    /// <summary>
    /// Callback called when an error happens in buying some item.
    /// </summary>
    /// <param name="error">Error details.</param>
    void OnPurchaseError(PlayFabError error)
    {
        Debug.LogError("Error buying ingame item: " + error.Error + " " + error.ErrorMessage);
    }

    /// <summary>
    /// Function used to tell PlayFab that the bought item will be consumed. Called "automatically" after transaction success.
    /// </summary>
    /// <param name="itemInstanceID">Item instance ID.</param>
    void ConsumeItem(string itemInstanceID)
    {
        ConsumeItemRequest request = new ConsumeItemRequest();
        request.ItemInstanceId = itemInstanceID;
        request.ConsumeCount = 1;

        PlayFabClientAPI.ConsumeItem(request, OnConsumeItemSuccess, OnConsumeItemError);
    }

    /// <summary>
    /// Callback called when user successfully consumes a bought item.
    /// </summary>
    /// <param name="result">Result.</param>
    void OnConsumeItemSuccess(ConsumeItemResult result)
    {
        Debug.Log("successfully consumed item");
    }

    /// <summary>
    /// Callback called when an error happens in consuming some item.
    /// </summary>
    /// <param name="error">Error details.</param>
    void OnConsumeItemError(PlayFabError error)
    {
        Debug.LogError("Error consuming item: " + error.Error + " " + error.ErrorMessage);
    }



    /// <summary>
    /// Method called to give the player a certain amount of in-game currency.
    /// </summary>
    /// <param name="amount">Currency amount to give.</param>
    public void AddInGameCurrency(int amount)
    {
        AddUserVirtualCurrencyRequest request = new AddUserVirtualCurrencyRequest();
        request.VirtualCurrency = GameConstants.inGameCurrencyID;
        request.Amount = amount;
        PlayFabClientAPI.AddUserVirtualCurrency(request, OnAddedInGameCurrency, OnAddGameCurrencyError);
    }

    /// <summary>
    /// Callback called when successfully gives the player a certain amount of in-game currency.
    /// </summary>
    /// <param name="result">Transaction result.</param>
    void OnAddedInGameCurrency(ModifyUserVirtualCurrencyResult result)
    {
        actualCurrency += result.Balance;

        GameplayUI.instance.UpdateCurrencyText(actualCurrency);
    }

    /// <summary>
    /// Callback called when an error occurs in giving the player in-game currency amount.
    /// </summary>
    /// <param name="error">Error details.</param>
    void OnAddGameCurrencyError(PlayFabError error)
    {
        Debug.LogError("Error adding in game currency: " + error.Error + " " + error.ErrorMessage);
    }

    /// <summary>
    /// Resets the player in-game currency.
    /// </summary>
    public void ResetCurrency()
    {
        this.AddInGameCurrency(-actualCurrency);
    }
}