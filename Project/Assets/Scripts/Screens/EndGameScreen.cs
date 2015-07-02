using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGameScreen : MonoBehaviour
{
    /// <summary>
    /// Reference to winner team.
    /// </summary>
    public static TeamTypes winnerTeam;

    /// <summary>
    /// Reference to text that will show who won.
    /// </summary>
    [SerializeField]
    Text winnerText;

    /// <summary>
    /// Shows winner.
    /// </summary>
    void Start()
    {
        AudioManager.instance.PlayMenuMusic();
        winnerText.text = "Team " + winnerTeam.ToString() + " won the game!";

        MultiplayerRoomsManager.instance.Dispose();
    }

    /// <summary>
    /// On Button Click, goes back to main menu.
    /// </summary>
    public void OnMenuClick()
    {
        Application.LoadLevel(Scenes.MainMenu.ToString());
    }

    /// <summary>
    /// On Button Click, goes to store.
    /// </summary>
    public void OnStoreClick()
    {
        Application.LoadLevel(Scenes.Store.ToString());
    }

    /// <summary>
    /// On Button Click, goes to mode selection.
    /// </summary>
    public void OnPlayAgainClick()
    {
        Application.LoadLevel(Scenes.ModeSelect.ToString());
    }
}