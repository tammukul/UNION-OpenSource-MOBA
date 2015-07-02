using UnityEngine;
using System.Collections;

public class BackButtonManager : BaseScreen
{
    /// <summary>
    /// Scene to load on back button press.
    /// </summary>
    [SerializeField]
    Scenes sceneToLoad;
    /// <summary>
    /// Audio to play on back button press.
    /// </summary>
    [SerializeField]
    AudioClip audioToPlay;

    /// <summary>
    /// Checks for back button press, loads level.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (audioToPlay != null && AudioManager.instance.sfxEnabled)
            {
                this.GetComponent<AudioSource>().clip = audioToPlay;
                GetComponent<AudioSource>().Play();
            }
            SceneLoader.sceneToLoad = sceneToLoad;
            CallChangeScene();
        }
    }
}