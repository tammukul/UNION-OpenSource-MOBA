using UnityEngine;
using System.Collections;

public class GameConstants
{
    /// <summary>
    /// PlayFab Key, references player account level.
    /// </summary>
    public static string accountLevelKey = "AccountLevel";
    /// <summary>
    /// PlayFab Key, references player account experience points.
    /// </summary>
    public static string accountExpKey = "AccountExp";

    /// <summary>
    /// PlayFab Key, references kills stat.
    /// </summary>
    public static string killsStatsKey = "Kills";

    /// <summary>
    /// PlayFab Key, references wins stat.
    /// </summary>
    public static string winsStatsKey = "Wins";

    public static string boughtSheKey = "BoughtShe";

    /// <summary>
    /// PlayFabKey, references catalog name.
    /// </summary>
    public static string inGameStoreCatalogName = "InGameStore";
    /// <summary>
    /// PlayFab Key, references currency ID.
    /// </summary>
    public static string inGameCurrencyID = "0";

    /// <summary>
    /// PlayFab Key, references player facebook picture.
    /// </summary>
    public static string facebookPictureKey = "FacebookPicture";

    /// <summary>
    /// PlayerPrefs Key, references music enabled.
    /// </summary>
    public static string musicPlayerPrefsKey = "MusicEnabled";

    /// <summary>
    /// PlayerPrefs Key, references SFX enabled.
    /// </summary>
    public static string SFXPlayerPrefsKey = "SFXEnabled";

    /// <summary>
    /// PlayerPrefs Key, references tutorial enabled.
    /// </summary>
    public static string tutorialPlayerPrefsKey = "TutorialEnabled";

    /// <summary>
    /// PlayerPrefs Key, references if already connected with facebook.
    /// </summary>
    public static string facebookConnectedKey = "FacebookConnected";
}