using UnityEngine;
using System.Collections;

public class GameplaySFXManager : MonoBehaviour
{
    /// <summary>
    /// Static instance, to be called anywhere from the code.
    /// </summary>
    public static GameplaySFXManager instance;

    /// <summary>
    /// Audio Clip to be played when the local player kills someone.
    /// </summary>
    [SerializeField]
    AudioClip killSound;

    /// <summary>
    /// Audio Clip to be played when the local player dies.
    /// </summary>
    [SerializeField]
    AudioClip deathSound;

    /// <summary>
    /// Audio Clip to be played when the local player team wins.
    /// </summary>
    [SerializeField]
    AudioClip winSound;

    /// <summary>
    /// Audio Clip to be played when the local player team loses.
    /// </summary>
    [SerializeField]
    AudioClip loseSound;

    /// <summary>
    /// Audio Clip to be played when the local player levels up.
    /// </summary>
    [SerializeField]
    AudioClip levelUpSound;

    /// <summary>
    /// Audio Clip to be played when the local player buys an item.
    /// </summary>
    [SerializeField]
    AudioClip itemUseSound;

    /// <summary>
    /// Bool containing if sound effects are enabled ingame.
    /// </summary>
    bool sfxEnabled;

    /// <summary>
    /// Initialization method. Initializes static reference, gets if sound effects are enabled.
    /// </summary>
    void Awake()
    {
        instance = this;

        sfxEnabled = AudioManager.instance.sfxEnabled;
    }

    /// <summary>
    /// Plays kill sound, if possible.
    /// </summary>
    public void PlayKillSound()
    {
        if (sfxEnabled)
        {
            GetComponent<AudioSource>().clip = killSound;
            GetComponent<AudioSource>().Play();
        }
    }

    /// <summary>
    /// Playes death sound, if possible.
    /// </summary>
    public void PlayDeathSound()
    {
        if (sfxEnabled)
        {
            GetComponent<AudioSource>().clip = deathSound;
            GetComponent<AudioSource>().Play();
        }
    }

    /// <summary>
    /// Plays win sound, if possible.
    /// </summary>
    public void PlayWinSound()
    {
        if (sfxEnabled)
        {
            GetComponent<AudioSource>().clip = winSound;
            GetComponent<AudioSource>().Play();
        }
    }

    /// <summary>
    /// Plays lose sound, if possible.
    /// </summary>
    public void PlayLoseSound()
    {
        if (sfxEnabled)
        {
            GetComponent<AudioSource>().clip = loseSound;
            GetComponent<AudioSource>().Play();
        }
    }

    /// <summary>
    /// Plays level up sound, if possible.
    /// </summary>
    public void PlayLevelUpSound()
    {
        if (sfxEnabled)
        {
            GetComponent<AudioSource>().clip = levelUpSound;
            GetComponent<AudioSource>().Play();
        }
    }

    /// <summary>
    /// Plays item use sound, if possible.
    /// </summary>
    public void PlayItemUseSound()
    {
        if (sfxEnabled)
        {
            GetComponent<AudioSource>().clip = itemUseSound;
            GetComponent<AudioSource>().Play();
        }
    }
}