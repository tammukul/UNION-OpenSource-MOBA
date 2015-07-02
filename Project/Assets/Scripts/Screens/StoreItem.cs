using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using PlayFab.ClientModels;

public class StoreItem : MonoBehaviour
{
    /// <summary>
    /// Text of the item name
    /// </summary>
    [SerializeField]
    Text itemNameText;
    /// <summary>
    /// Text of the item description
    /// </summary>
    [SerializeField]
    Text itemDescriptionText;

    /// <summary>
    /// Text of the item price
    /// </summary>
    [SerializeField]
    Text itemPriceText;

    /// <summary>
    /// Screen manager reference
    /// </summary>
    StoreScreen_OLD screenManager;


    /// <summary>
    /// Catalog item info
    /// </summary>
    CatalogItem myInfo;

    public void Initialize(StoreScreen_OLD screenManager, CatalogItem info)
    {   
        this.screenManager = screenManager;
        this.myInfo = info;

        itemNameText.text = myInfo.DisplayName;
        itemDescriptionText.text = myInfo.Description;
        itemPriceText.text = myInfo.VirtualCurrencyPrices["1"].ToString();
    }

    public void OnItemClick()
    {
        screenManager.BuyItem(myInfo);
    }
}