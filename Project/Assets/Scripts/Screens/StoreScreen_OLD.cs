using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels;

public class StoreScreen_OLD : MonoBehaviour
{
    [SerializeField]
    GameObject storeItemPrefab;

    [SerializeField]
    GameObject tableParent;

    void Start()
    {
        PlayFabManager.instance.GetAllCatalogItems(OnCatalogItemsLoaded);
    }

    void OnCatalogItemsLoaded(List<CatalogItem> catalogItems)
    {
        for (int i = 0; i < catalogItems.Count; i++)
        {
            GameObject obj = GameObject.Instantiate(storeItemPrefab) as GameObject;

            obj.GetComponent<StoreItem>().Initialize(this, catalogItems[i]);

            obj.transform.SetParent(tableParent.transform, false);
        }
    }

    public void BuyItem(CatalogItem item)
    {
        PlayFabManager.instance.BuyItemWithVirtualCurrency(item, OnBuySuccess);
    }

    void OnBuySuccess(List<PurchasedItem> items)
    {
        PlayFabManager.instance.ConsumeItem(items[0].ItemInstanceId);

        AccountManager.instance.GiveAccountLevel(1);
    }

    public void OnExitClick()
    {
        Application.LoadLevel("Landing");
    }
}