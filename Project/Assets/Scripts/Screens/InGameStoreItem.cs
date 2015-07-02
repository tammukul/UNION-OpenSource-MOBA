using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using PlayFab.ClientModels;

public class InGameStoreItem : MonoBehaviour
{
    /// <summary>
    /// Text to display the item name
    /// </summary>
    [SerializeField]
    Text displayNameText;

    /// <summary>
    /// Text to display the item description
    /// </summary>
    [SerializeField]
    Text descriptionText;

    /// <summary>
    /// Text to display the item price
    /// </summary>
    [SerializeField]
    Text priceText;

    CatalogItem myInfo;

    public void Initialize(CatalogItem itemInfo)
    {
        myInfo = itemInfo;

        displayNameText.text = itemInfo.DisplayName;
        descriptionText.text = itemInfo.Description;
        priceText.text = itemInfo.VirtualCurrencyPrices[GameConstants.inGameCurrencyID].ToString();
    }

    public void OnButtonClick()
    {
        InGameStoreAndCurrencyManager.instance.BuyItemWithVirtualCurrency(myInfo);
        GameplayUI.instance.DismissPanel();
    }
}