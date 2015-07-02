using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.MiniJSON;

public class FacebookManager : MonoBehaviour
{
    /// <summary>
    /// Callback to be called when Facebook login process completes.
    /// </summary>
    ProjectDelegates.FacebookLoginCallback OnFacebookLoginCompletedCallback;

    /// <summary>
    /// User's facebook name.
    /// </summary>
    string facebookName;

    /// <summary>
    /// User's facebook picture URL.
    /// </summary>
    string facebookPictureUrl;


    /// <summary>
    /// Function used to initialize facebook.
    /// </summary>
    public void Initialize(ProjectDelegates.FacebookLoginCallback OnLoginCompletedCallback)
    {
        this.OnFacebookLoginCompletedCallback = OnLoginCompletedCallback;

        if (!FB.IsLoggedIn)
        {
            FB.Init(OnInitCompleted, OnHideUnity);
        }
        else
        {
            //this.OnFacebookLoginCompletedCallback(FB.AccessToken);
            FB.API("me", Facebook.HttpMethod.GET, OnProfileInfoLoaded);
        }
    }

    /// <summary>
    /// Callback used when Facebook initialization is complete.
    /// </summary>
    void OnInitCompleted()
    {
        if (!FB.IsLoggedIn)
        {
            FB.Login("", OnLoginCompleted);
        }
        else
        {
            FB.API("me", Facebook.HttpMethod.GET, OnProfileInfoLoaded);
            //this.OnFacebookLoginCompletedCallback(FB.AccessToken);
        }
    }

    /// <summary>
    /// Callback used when Facebook login is complete.
    /// </summary>
    /// <param name="result">Login result</param>
    void OnLoginCompleted(FBResult result)
    {
        if (result.Error != null)
        {
            Debug.Log("error facebook " + result.Error);
        }
        else if (!FB.IsLoggedIn)
        {
            Debug.Log("login cancelled, by user or error");
        }
        else
        {
            Debug.Log("login successful");
            FB.API("me", Facebook.HttpMethod.GET, OnProfileInfoLoaded);
        }
    }

    /// <summary>
    /// Callback used when user's profile info loads.
    /// </summary>
    /// <param name="result"></param>
    void OnProfileInfoLoaded(FBResult result)
    {
        Dictionary<string, object> profileInfo = (Dictionary<string, object>)Json.Deserialize(result.Text);

        facebookName = profileInfo["name"].ToString();

        FB.API("me/picture?width=100&height=100&redirect=false", Facebook.HttpMethod.GET, OnPictureLoaded);
    }

    /// <summary>
    /// Callback used when user's facebook picture loads.
    /// </summary>
    /// <param name="result"></param>
    void OnPictureLoaded(FBResult result)
    {
        Dictionary<string, object> receivedInfo = (Dictionary<string, object>)Json.Deserialize(result.Text);
        Dictionary<string, object> pictureInfo = (Dictionary<string, object>)receivedInfo["data"];

        facebookPictureUrl = pictureInfo["url"].ToString();

        this.OnFacebookLoginCompletedCallback(FB.AccessToken, facebookName,facebookPictureUrl);
    }

    /// <summary>
    /// Function needed to handle application win/lose focus
    /// </summary>
    /// <param name="isGameShown">Is focus on app?</param>
    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // start the game back up - we're getting focus again
            Time.timeScale = 1;
        }
    }

    /// <summary>
    /// Facebook Logout
    /// </summary>
    public void Logout()
    {
        FB.Logout();
    }
}