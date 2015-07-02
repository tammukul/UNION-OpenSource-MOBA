using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// Static instance, to be called anywhere in the project.
    /// </summary>
    public static AudioManager instance;

    /// <summary>
    /// AudioClip for the menu music.
    /// </summary>
    [SerializeField]
    AudioClip menuClip;
    /// <summary>
    /// Audio clip for the gameplay music.
    /// </summary>
    [SerializeField]
    AudioClip gameplayClip;

    /// <summary>
    /// Boolean containing if music is enabled. Controlled from inside the class.
    /// </summary>
    public bool musicEnabled { get; private set; }
    /// <summary>
    /// Boolean containing if sound effects are enabled. Controlled from inside the class.
    /// </summary>
    public bool sfxEnabled { get; private set; }

    /// <summary>
    /// Initialization method.
    /// Gets data from PlayerPrefs, prevents GameObject destruction, initializes static reference.
    /// </summary>
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        this.ToggleMusic(PlayerPrefs.GetInt(GameConstants.musicPlayerPrefsKey, 1) == 1);
        this.ToggleSFX(PlayerPrefs.GetInt(GameConstants.SFXPlayerPrefsKey, 1) == 1);
    }

    /// <summary>
    /// Toggles music On/Off. If On, resets music.
    /// </summary>
    /// <param name="enabled">Will enable music?</param>
    public void ToggleMusic(bool enabled)
    {
        Debug.Log("toggle music " + enabled);
        musicEnabled = enabled;

        if (musicEnabled) PlayMenuMusic();
        else StopMusic();
    }

    /// <summary>
    /// Toggles sound effects On/Off.
    /// </summary>
    /// <param name="enabled">Will enable SFX?</param>
    public void ToggleSFX(bool enabled)
    {
        Debug.Log("toggle SFX " + enabled);
        sfxEnabled = enabled;
    }

    /// <summary>
    /// Method called to play menu music.
    /// </summary>
    public void PlayMenuMusic()
    {
        this.StopMusic();
        if (musicEnabled && menuClip != null)
        {
            GetComponent<AudioSource>().clip = menuClip;
            GetComponent<AudioSource>().Play();
        }
    }

    /// <summary>
    /// Method called to play gameplay music.
    /// </summary>
    public void PlayGameplayMusic()
    {
        this.StopMusic();
        if (musicEnabled && gameplayClip != null)
        {
            GetComponent<AudioSource>().clip = gameplayClip;
            GetComponent<AudioSource>().Play();
        }
    }

    /// <summary>
    /// Stops current music.
    /// </summary>
    public void StopMusic()
    {
        GetComponent<AudioSource>().Stop();
    }
}