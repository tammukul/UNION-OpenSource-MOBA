using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class LeaderboardItem : MonoBehaviour
{

    /// <summary>
    /// Player picture
    /// </summary>
    [SerializeField]
    Image pictureImage;


    /// <summary>
    /// Player position text
    /// </summary>
    [SerializeField]
    Text positionTxt;


    /// <summary>
    /// Player name text
    /// </summary>
    [SerializeField]
    Text nameTxt;

    /// <summary>
    /// Total kills text
    /// </summary>
    [SerializeField]
    Text killsTxt;

    public void Populate(PlayerLeaderboardEntry myInfo)
    {
        positionTxt.text = (myInfo.Position + 1).ToString();
        nameTxt.text = myInfo.DisplayName;
        killsTxt.text = myInfo.StatValue + " kills";

        GetUserDataRequest request = new GetUserDataRequest();

        request.PlayFabId = myInfo.PlayFabId;
        request.Keys = new List<string>();
        request.Keys.Add(GameConstants.facebookPictureKey);

        PlayFabClientAPI.GetUserData(request, OnPlayerInfoLoaded, OnPlayerInfoLoadError);
    }

    void OnPlayerInfoLoaded(GetUserDataResult result)
    {
        UserDataRecord dataRecord;
        result.Data.TryGetValue(GameConstants.facebookPictureKey,out dataRecord);

        if (dataRecord != null)
        {
            string pictureURL = dataRecord.Value;

            StartCoroutine(LoadPicutre(pictureURL));
        }
    }

    void OnPlayerInfoLoadError(PlayFabError error)
    {
        Debug.Log("Get User error: " + error.Error + " " + error.ErrorMessage);
    }

    IEnumerator LoadPicutre(string url)
    {
        WWW request = new WWW(url);

        yield return request;
        if(request.error == null)
        {
            Rect pictureSize = new Rect(0, 0, request.texture.width, request.texture.height);
            pictureImage.sprite = Sprite.Create(request.texture, pictureSize, new Vector2(0.5f, 0.5f));
        }
    }
}