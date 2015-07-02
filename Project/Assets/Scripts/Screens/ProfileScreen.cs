using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels;

public class ProfileScreen : BaseScreen
{
    /// <summary>
    /// Player picture reference.
    /// </summary>
    [SerializeField]
    Image myPicture;

    /// <summary>
    /// Name text reference.
    /// </summary>
    [SerializeField]
    Text nameTxt;

    /// <summary>
    /// Level text reference.
    /// </summary>
    [SerializeField]
    Text levelTxt;

    /// <summary>
    /// Experience text reference.
    /// </summary>
    [SerializeField]
    Text expTxt;

    /// <summary>
    /// Experience bar reference.
    /// </summary>
    [SerializeField]
    Image expBarFill;

    /// <summary>
    /// Kills number text reference.
    /// </summary>
    [SerializeField]
    Text killsTxt;

    /// <summary>
    /// Wins number text reference.
    /// </summary>
    [SerializeField]
    Text winsTxt;

    /// <summary>
    /// Leaderboard items reference.
    /// </summary>
    [SerializeField]
    LeaderboardItem[] leaderboardItems;

    /// <summary>
    /// Populates texts with data.
    /// </summary>
    void Start()
    {
        nameTxt.text = AccountManager.instance.displayName;
        levelTxt.text = AccountManager.instance.accountLevel.ToString();
        expTxt.text = AccountManager.instance.accountExp.ToString();
        killsTxt.text = AccountManager.instance.killCount.ToString();
        winsTxt.text = AccountManager.instance.winsAmount.ToString();

        StartCoroutine(LoadPicutre());

        PlayFabManager.instance.GetLeaderboardFromPosition(GameConstants.killsStatsKey, 10, 0, OnLeadeboardLoaded);

        if (AccountManager.instance.accountLevel == AccountManager.instance.experienceForEveryLevel.Length-1) return;

        int lastLevelExp = AccountManager.instance.experienceForEveryLevel[AccountManager.instance.accountLevel];
        int nextLevelExp = AccountManager.instance.experienceForEveryLevel[AccountManager.instance.accountLevel+1];
        int actualExp = AccountManager.instance.accountExp;

        float fillAmount = (float)(actualExp - lastLevelExp) / (float)(nextLevelExp - lastLevelExp);

        Debug.Log(lastLevelExp + " " + nextLevelExp + " " + actualExp + " " + fillAmount);
        expBarFill.fillAmount = fillAmount;
    }

    IEnumerator LoadPicutre()
    {
        WWW request = new WWW(AccountManager.instance.facebookPictureURL);

        yield return request;

        Rect pictureSize = new Rect(0,0,request.texture.width,request.texture.height);
        myPicture.sprite = Sprite.Create(request.texture, pictureSize, new Vector2(0.5f, 0.5f));
    }

    /// <summary>
    /// On Button Click, goes back to main menu.
    /// </summary>
    public void OnMainMenuClick()
    {
        SceneLoader.sceneToLoad = Scenes.MainMenu;
        Invoke("CallChangeScene", defaultDelayTime);

        StopAllCoroutines();
    }

    void OnLeadeboardLoaded(List<PlayerLeaderboardEntry> leaderboard)
    {
        Debug.Log("leaderboard loaded. length " + leaderboard.Count);
        for (int i = 0; i < leaderboard.Count; i++)
        {
            leaderboardItems[i].gameObject.SetActive(true);
            leaderboardItems[i].Populate(leaderboard[i]);
        }
    }
}