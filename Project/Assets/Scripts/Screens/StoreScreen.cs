using UnityEngine;
using System.Collections;

public class StoreScreen : BaseScreen
{
    void Start()
    {
        InAppPurchaseManager.instance.Initialize();
    }

    void Buy()
    {
        InAppPurchaseManager.instance.BuyItem();
    }

    public void OnBackClick()
    {
        SceneLoader.sceneToLoad = Scenes.MainMenu;
        Invoke("CallChangeScene", defaultDelayTime);
    }
}