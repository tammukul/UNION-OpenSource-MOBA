using UnityEngine;
using System.Collections;

public class TutorialScreen : BaseScreen
{


    /// <summary>
    /// Audio for next button
    /// </summary>
    [SerializeField]
    AudioClip nextSound;


    /// <summary>
    /// Audio for the back button
    /// </summary>
    [SerializeField]
    AudioClip backSound;


    /// <summary>
    /// Gameobject array with tutorial pages
    /// </summary>
	[SerializeField] GameObject[] pages;


    /// <summary>
    /// Value of the current page
    /// </summary>
    int actualPage;

    public void NextClick()
    {
        if (AudioManager.instance.sfxEnabled)
        {
            GetComponent<AudioSource>().clip = nextSound;
            GetComponent<AudioSource>().Play();
        }

		pages [actualPage].SetActive (false);

        actualPage++;
        if (actualPage == pages.Length) {
			this.GoToNextScene ();
		} else {
			pages[actualPage].SetActive(true);
		}
    }

    public void BackClick()
    {
        if (AudioManager.instance.sfxEnabled)
        {
            GetComponent<AudioSource>().clip = backSound;
            GetComponent<AudioSource>().Play();
        }

		pages [actualPage].SetActive (false);
        actualPage--;
		pages [actualPage].SetActive (true);
    }

    void GoToNextScene()
    {
        PlayerPrefs.SetInt(GameConstants.tutorialPlayerPrefsKey, 0);
        
        SceneLoader.sceneToLoad = Scenes.ModeSelect;
        Application.LoadLevel(Scenes.Loading.ToString());
    }
}