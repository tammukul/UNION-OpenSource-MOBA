using UnityEngine;
using System.Collections;

public class DIsconnectedFeedback : MonoBehaviour
{
    public void OnClick()
    {
        MultiplayerRoomsManager.instance.Dispose();
        SceneLoader.sceneToLoad = Scenes.MainMenu;
        Application.LoadLevel(Scenes.Loading.ToString());
    }
}