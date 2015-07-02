using UnityEngine;
using System.Collections;
using PlayFab;
using PlayFab.ClientModels;
using OnePF;
using System.Collections.Generic;

public class InAppPurchaseManager : MonoBehaviour
{
    public static InAppPurchaseManager instance;

    void Awake()
    {
        instance = this;
    }

    public void Initialize()
    {
        OpenIABEventManager.purchaseSucceededEvent += OnPurchaseSucceeded;

        OpenIAB.mapSku("buyshe", OpenIAB_Android.STORE_GOOGLE, "buyshe");

        var googlePublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAiaoqnY/8dixib9ZY1sYPfBvdc8uhGMZ24HYDGbgtpBiHR5aZ5LuQ9eJgfRLnXeCMZVVD5IqWBtehjTtoq2xsD+yvauYX7mTboq5OEEVwTBfkXYRsGyuIV9CBRBY+2sCE9fGGIu+LwTFA+SucXh3r10J4scANSIINV7VXgqg8AK7xTmU70IwkQwHg2271uIlSHaDpKPXYkYHvo4IifyWOxTE3uiwX4I/RP0IIhRph/zNwGDY6VnhVxjsCS8Ua0EfVdONCLFOiwNitfjdZ22OiAVVfL8aFllVSbGQrC6mzJTtjuSySp+Suu6ZbU+16fmzwFkolybV0ibChNg/ajY3X1QIDAQAB";

        var options = new Options();
        options.checkInventoryTimeoutMs = Options.INVENTORY_CHECK_TIMEOUT_MS * 2;
        options.discoveryTimeoutMs = Options.DISCOVER_TIMEOUT_MS * 2;
        options.checkInventory = false;
        options.verifyMode = OptionsVerifyMode.VERIFY_SKIP;
        options.prefferedStoreNames = new string[] { OpenIAB_Android.STORE_GOOGLE };
        options.availableStoreNames = new string[] { OpenIAB_Android.STORE_GOOGLE };
        options.storeKeys = new Dictionary<string, string> { { OpenIAB_Android.STORE_GOOGLE, googlePublicKey } };
        options.storeSearchStrategy = SearchStrategy.INSTALLER_THEN_BEST_FIT;

        OpenIAB.init(options);
    }

    public void BuyItem()
    {
        OpenIAB.purchaseProduct("buyshe");
    }

    public void OnPurchaseSucceeded(Purchase purchase)
    {
        if (AudioManager.instance.sfxEnabled) this.GetComponent<AudioSource>().Play();

        Debug.Log("OnPurchaseSucceeded");
        OpenIAB.consumeProduct(purchase);

        ValidateGooglePlayPurchaseRequest request = new ValidateGooglePlayPurchaseRequest();
        request.ReceiptJson = purchase.OriginalJson;
        request.Signature = purchase.Signature;
        PlayFabClientAPI.ValidateGooglePlayPurchase(request, OnValidateCompleted, OnValidateError);

		// calling this before validation?
        UpdateUserDataRequest dataRequest = new UpdateUserDataRequest();
        dataRequest.Data = new Dictionary<string, string>();
        dataRequest.Data.Add(GameConstants.boughtSheKey, "true");
        dataRequest.Permission = UserDataPermission.Public;
        PlayFabClientAPI.UpdateUserData(dataRequest, OnDataUpdateCompleted, OnDataUpdateError);

        AccountManager.instance.BuyShe();
    }

    void OnValidateCompleted(ValidateGooglePlayPurchaseResult result)
    {
        Debug.Log("validate completed " + result.ToString());
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnUserInventoryLoaded, OnUserInventoryLoadError);
    }

    void OnValidateError(PlayFabError error)
    {
        Debug.Log("Google Play purchase validation error: " + error.Error + " " + error.ErrorMessage);
    }

	// probably not the best practice to use here, maybe add another check for certain custom properties
    void OnUserInventoryLoaded(GetUserInventoryResult result)
    {
        for (int i = 0; i < result.Inventory.Count; i++)
        {
            if (result.Inventory[i].RemainingUses > 0)
            {
                ConsumeItemRequest request = new ConsumeItemRequest();
                request.ConsumeCount = 1;
                request.ItemInstanceId = result.Inventory[i].ItemInstanceId;

                PlayFabClientAPI.ConsumeItem(request,OnItemConsumed,OnItemConsumeError);

                return;
            }
        }
    }

    void OnUserInventoryLoadError(PlayFabError error)
    {
        Debug.Log("Inventory load error: " + error.Error + " " + error.ErrorMessage);
    }

    void OnItemConsumed(ConsumeItemResult result)
    {
        //Do some kind of feedback stuff
    }

    void OnItemConsumeError(PlayFabError error)
    {
        Debug.Log("Consume item error: " + error.Error + " " + error.ErrorMessage);
    }

    void OnDataUpdateCompleted(UpdateUserDataResult result)
    {
        Debug.Log("Successfully updated data");
    }

    void OnDataUpdateError(PlayFabError error)
    {
        Debug.Log("Update data error: " + error.Error + " " + error.ErrorMessage);
    }
}