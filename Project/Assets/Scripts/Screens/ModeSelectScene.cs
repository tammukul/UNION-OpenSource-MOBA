using UnityEngine;
using System.Collections;

public class ModeSelectScene : BaseScreen
{

    /// <summary>
    /// Gameobject of the mode select screen
    /// </summary>
    [SerializeField]
    GameObject modeSelectScreen;


    /// <summary>
    /// GameObject of the loading screen
    /// </summary>
    [SerializeField]
    GameObject loadingScreen;

    /// <summary>
    /// GameObject cointaing the UI Canvas 
    /// </summary>
	public GameObject ui_canvas;

    /// <summary>
    /// Check is Photon is connected
    /// </summary>
    bool photonConnected;

    void Start()
    {
        MultiplayerRoomsManager.instance.Initialize(OnConnectionCompleted);
    }

    void OnConnectionCompleted(RoomInfo[] rooms)
    {
        loadingScreen.SetActive(false);
        modeSelectScreen.SetActive(true);

        photonConnected = true;
        Debug.Log("connected to photon");
    }

    public void FindRandomMatch(int maxPlayers)
    {
        if (photonConnected)
        {
			if (AudioManager.instance.sfxEnabled) this.ui_canvas.GetComponent<AudioSource>().Play();

            MultiplayerRoomsManager.instance.JoinRoom((byte)maxPlayers, AccountManager.instance.accountLevel, OnJoinedRoomCallback);

            Invoke("ShowLoading", 0.5f);
        }
    }

    void ShowLoading()
    {
        loadingScreen.SetActive(true);
        modeSelectScreen.SetActive(false);
    }

    void OnJoinedRoomCallback(Room room)
    {
        SceneLoader.sceneToLoad = Scenes.MatchRoom;
        Invoke("CallChangeScene", defaultDelayTime);
    }

    public void CreateCustomMatch()
    {
		Debug.LogWarning("CreateCustomMatch: Not Implemented");
    }

    public void FindCustomMatch()
    {
		Debug.LogWarning("FindCustomMatch: Not Implemented");
    }

    public void BackToMenu()
    {
        MultiplayerRoomsManager.instance.Dispose();

        SceneLoader.sceneToLoad = Scenes.MainMenu;
        Invoke("CallChangeScene", defaultDelayTime);
    }
}