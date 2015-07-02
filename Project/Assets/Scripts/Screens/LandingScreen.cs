using UnityEngine;
using System.Collections;

public class LandingScreen : BaseScreen
{

    /// <summary>
    /// Connect screen gameobject
    /// </summary>
    [SerializeField]
    GameObject connectScreen;


    /// <summary>
    /// loading screen gameobject
    /// </summary>
    [SerializeField]
    GameObject loadingScreen;

    void Start()
    {
        PushNotificationsManager.instance.Initialize();

        if (PlayerPrefs.GetInt(GameConstants.facebookConnectedKey, 0) == 1)
        {
            this.OnConnectClick();
        }
        else
        {
            connectScreen.SetActive(true);
        }
    }

    public void OnConnectClick()
    {
        connectScreen.SetActive(false);
        loadingScreen.SetActive(true);
        PlayerPrefs.SetInt(GameConstants.facebookConnectedKey, 1);
        AccountManager.instance.LoginWithFacebook(OnLoginCompleted);
    }

    void OnLoginCompleted()
    {
        SceneLoader.sceneToLoad = Scenes.MainMenu;
        Invoke("CallChangeScene", defaultDelayTime);
    }
}