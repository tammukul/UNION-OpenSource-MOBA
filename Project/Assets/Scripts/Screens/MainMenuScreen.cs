using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuScreen : BaseScreen
{

    /// <summary>
    /// Toggle to set music on/off
    /// </summary>
    [SerializeField]
    Toggle musicToggle;


    /// <summary>
    /// Toggle to set sound effects on/off
    /// </summary>
    [SerializeField]
    Toggle sfxToggle;


    /// <summary>
    /// Toggle to set tutorial on/off
    /// </summary>
    [SerializeField]
    Toggle tutorialToggle;


    /// <summary>
    /// Boolean to check if the tutorial option is on or off
    /// </summary>
	bool tutorialEnabled;

	void Start()
	{
		tutorialEnabled = PlayerPrefs.GetInt(GameConstants.tutorialPlayerPrefsKey, 1) == 1;

        musicToggle.isOn = !AudioManager.instance.musicEnabled;
        sfxToggle.isOn = AudioManager.instance.sfxEnabled;
        tutorialToggle.isOn = tutorialEnabled;
	}

    public void OnPlayClick()
    {
        if (AudioManager.instance.sfxEnabled) this.GetComponent<AudioSource>().Play();

		if (tutorialEnabled) {
			SceneLoader.sceneToLoad = Scenes.Tutorial;
		} else {
			SceneLoader.sceneToLoad = Scenes.ModeSelect;
		}
		Invoke ("CallChangeScene", defaultDelayTime);
    }

    public void OnStoreClick()
    {
        if (AudioManager.instance.sfxEnabled) this.GetComponent<AudioSource>().Play();

        SceneLoader.sceneToLoad = Scenes.Store;
        Invoke("CallChangeScene", defaultDelayTime);
    }

    public void OnProfileClick()
    {
        if (AudioManager.instance.sfxEnabled) this.GetComponent<AudioSource>().Play();

        SceneLoader.sceneToLoad = Scenes.Profile;
        Invoke("CallChangeScene", defaultDelayTime);
    }

    public void OnOptionsClick()
    {
        if (AudioManager.instance.sfxEnabled) this.GetComponent<AudioSource>().Play();

        SceneLoader.sceneToLoad = Scenes.MainMenu;
        Invoke("CallChangeScene", defaultDelayTime);
    }

    public void OnMusicClick(bool enabled)
    {
        enabled = !enabled;

        PlayerPrefs.SetInt(GameConstants.musicPlayerPrefsKey, enabled ? 1 : 0);

        AudioManager.instance.ToggleMusic(enabled);

        if (AudioManager.instance.sfxEnabled) this.GetComponent<AudioSource>().Play();
    }

    public void OnSFXClick(bool enabled)
    {
        PlayerPrefs.SetInt(GameConstants.SFXPlayerPrefsKey, enabled ? 1 : 0);

        AudioManager.instance.ToggleSFX(enabled);

        if (AudioManager.instance.sfxEnabled) this.GetComponent<AudioSource>().Play();
    }

    public void OnTutorialClick(bool enabled)
    {
		tutorialEnabled = enabled;

        PlayerPrefs.SetInt(GameConstants.tutorialPlayerPrefsKey, tutorialEnabled ? 1 : 0);

        if (AudioManager.instance.sfxEnabled) this.GetComponent<AudioSource>().Play();
    }

    public void OnLogoutClick()
    {
        AccountManager.instance.LogoutFromEverything();
        PlayerPrefs.DeleteAll();

        SceneLoader.sceneToLoad = Scenes.Landing;
        Invoke("CallChangeScene", defaultDelayTime);
    }
}