using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels;

public class RankingsScreen : MonoBehaviour
{
    /// <summary>
    /// Prefab of leaderboard item.
    /// </summary>
    public GameObject leaderboardItemPrefab;

    /// <summary>
    /// Table item reference.
    /// </summary>
    public Transform tableRoot;

    /// <summary>
    /// Gets leaderboards from PlayFab.
    /// </summary>
    void Start()
    {
        PlayFabManager.instance.GetLeaderboardAroundMe(GameConstants.killsStatsKey, 10, OnLeaderboardLoaded);
    }

    /// <summary>
    /// Leadeboards loaded, shows on screen.
    /// </summary>
    /// <param name="leaderboard">Leaderboard info.</param>
    void OnLeaderboardLoaded(List<PlayerLeaderboardEntry> leaderboard)
    {
        Debug.Log("leaderboard length " + leaderboard.Count);
        for (int i = 0; i < leaderboard.Count; i++)
        {
            GameObject obj = (GameObject)GameObject.Instantiate(leaderboardItemPrefab);
            
            Text[] texts = obj.GetComponentsInChildren<Text>();

            texts[0].text = (leaderboard[i].Position + 1).ToString();
            texts[1].text = leaderboard[i].DisplayName;
            texts[2].text = leaderboard[i].StatValue.ToString();

            obj.transform.SetParent(tableRoot,false);
            //RectTransform rect = (obj.transform as RectTransform);

            (obj.transform as RectTransform).anchoredPosition = new Vector2(0, 200 - (50 * i));
            (obj.transform as RectTransform).sizeDelta = new Vector2(300, 50);

        }
    }

    /// <summary>
    /// On Button Click, goes back to main menu.
    /// </summary>
    public void OnMainMenuClick()
    {
        Application.LoadLevel("Landing");
    }
}