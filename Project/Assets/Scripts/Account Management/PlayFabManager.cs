using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabManager : MonoBehaviour
{
    /// <summary>
    /// Static instance, enabling the class to be called anywhere in the project.
    /// </summary>
    public static PlayFabManager instance;

    /// <summary>
    /// All the game news' reference.
    /// </summary>
    public List<TitleNewsItem> gameNews { get; private set; }

    /// <summary>
    /// Callback to be called when PlayFab login process completes.
    /// </summary>
    ProjectDelegates.PlayFabLoginCallback OnLoginCompletedCallback;

    /// <summary>
    /// Game ID given when the developer creates a new PlayFab game.
    /// </summary>
    [SerializeField]
    string playFabGameID;


	public string AppId = "40ada087-34dd-460d-899f-6014b051ce33";

    /// <summary>
    /// Reference of all the title data, stored in PlayFab setting.
    /// </summary>
    public Dictionary<string, string> titleData { get; private set; }

    /// <summary>
    /// User's unique PlayFab ID.
    /// </summary>
    string playerID;

    /// <summary>
    /// User's unique PlayFab username.
    /// </summary>
    public string playerUsername { get; private set; }

    /// <summary>
    /// Photon authetication token
    /// </summary>
    public string playerPhotonToken { get; private set; }


    /// <summary>
    /// User's display name (facebook name).
    /// </summary>
    public string playerDisplayName { get; private set; }

    /// <summary>
    /// User's facebook picture URL.
    /// </summary>
    string playerPictureURL;

    /// <summary>
    /// Callback to be called when getting leaderboards process completes.
    /// </summary>
    ProjectDelegates.PlayFabLeaderboardCallback OnLeaderboardLoadedCallback;

    /// <summary>
    /// Callback to be called when getting store catalog process completes.
    /// </summary>
    ProjectDelegates.PlayFabCatalogListCallback OnCatalogLoadedCallback;

    /// <summary>
    /// Callback to be called when buying item with virtual currency process completes.
    /// </summary>
    ProjectDelegates.PlayFabItemBuyCallback OnBuySuccessCallback;

    /// <summary>
    /// Store's catalog items.
    /// </summary>
    List<CatalogItem> catalogItems;

	/// <summary>
	/// Number to be added to the player's name, if there's another player with the same name.
	/// </summary>
	int userNameIndex;

    void Awake()
    {
        instance = this;

    }

    /// <summary>
    /// Function to be called when the user will create a new account from scratch.
    /// </summary>
    /// <param name="username">User's chosen username</param>
    /// <param name="password">User's chosen password</param>
    /// <param name="email">User's  chosen email</param>
    /// <param name="OnUserCreatedCallback">Function to call after process ends.</param>
    public void CreateNewUser(string username, string password, string email, ProjectDelegates.PlayFabLoginCallback OnUserCreatedCallback)
    {
        this.OnLoginCompletedCallback = OnUserCreatedCallback;

        PlayFabSettings.TitleId = playFabGameID;

        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest();
        request.Username = username;
        request.Password = password;
        request.Email = email;
        request.TitleId = playFabGameID;

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegistrationCompleted, OnLoginError);
    }

    /// <summary>
    /// Function used to login an existing player with PlayFab.
    /// </summary>
    /// <param name="username">User's username</param>
    /// <param name="password">User's password</param>
    /// <param name="OnLoginCompletedCallback">Function to call after process ends</param>
    public void LoginWithPlayFab(string username, string password, ProjectDelegates.PlayFabLoginCallback OnLoginCompletedCallback)
    {
        this.OnLoginCompletedCallback = OnLoginCompletedCallback;

        PlayFabSettings.TitleId = playFabGameID;



        LoginWithPlayFabRequest request = new LoginWithPlayFabRequest();
        request.Username = username;
        request.Password = password;
        request.TitleId = playFabGameID;

        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginCompleted, OnLoginError);
    }

    /// <summary>
    /// Function to be called to link Facebook user with PlayFab user 
    /// (creates a new one if not exists, logins existing user if exists).
    /// </summary>
    /// <param name="facebookAccessToken">Facebook's access token</param>
    /// <param name="OnLoginCompletedCallback">Function to call after process ends</param>
    public void LoginWithFacebook(string facebookAccessToken, string facebookName, string facebookPictureURL, ProjectDelegates.PlayFabLoginCallback OnLoginCompletedCallback)
    {
        this.OnLoginCompletedCallback = OnLoginCompletedCallback;

        this.playerDisplayName = facebookName;
        this.playerPictureURL = facebookPictureURL;

        PlayFabSettings.TitleId = playFabGameID;

        LoginWithFacebookRequest facebookRequest = new LoginWithFacebookRequest();
        facebookRequest.CreateAccount = true;
        facebookRequest.TitleId = playFabGameID;
        facebookRequest.AccessToken = facebookAccessToken;

        PlayFabClientAPI.LoginWithFacebook(facebookRequest, OnLoginCompleted, OnLoginError);
    }

    /// <summary>
    /// Removes all references from playFab user.
    /// </summary>
    public void Logout()
    {
        playerID = "";
        playerUsername = "";
    }

    /// <summary>
    /// Callback called when new user creation completes.
    /// </summary>
    /// <param name="result">Result of user creation</param>
    void OnRegistrationCompleted(RegisterPlayFabUserResult result)
    {
        playerID = result.PlayFabId;
        playerUsername = result.Username;

        Dictionary<string, string> playerData = new Dictionary<string, string>();
        playerData.Add(GameConstants.accountLevelKey, "0");
        playerData.Add(GameConstants.accountExpKey, "0");

        UpdateUserDataRequest request = new UpdateUserDataRequest();
        request.Data = playerData;
        request.Permission = UserDataPermission.Public;

        PlayFabClientAPI.UpdateUserData(request, OnAddDataSuccess, OnAddDataError);
    }

    /// <summary>
    /// Callback called when user login completes.
    /// </summary>
    /// <param name="result">Result of user login</param>
    void OnLoginCompleted(LoginResult result)
    {
        playerID = result.PlayFabId;

        PushNotificationsManager.instance.Register();

        this.LoadTitleData();

				GetPhotonAuthenticationTokenRequest tokenrequest = new GetPhotonAuthenticationTokenRequest();
		tokenrequest.PhotonApplicationId = AppId;
		
				PlayFabClientAPI.GetPhotonAuthenticationToken(tokenrequest, OnPhotonAuthenticationSuccess, OnPlayFabError);


        if (result.NewlyCreated)
        {

            Dictionary<string, string> playerData = new Dictionary<string, string>();
            playerData.Add(GameConstants.accountLevelKey, "0");
            playerData.Add(GameConstants.accountExpKey, "0");
            playerData.Add(GameConstants.facebookPictureKey,playerPictureURL);

            UpdateUserDataRequest request = new UpdateUserDataRequest();
            request.Data = playerData;
            request.Permission = UserDataPermission.Public;

            PlayFabClientAPI.UpdateUserData(request, OnAddDataSuccess, OnAddDataError);
        }
        else
        {
            Dictionary<string, string> playerData = new Dictionary<string, string>();
            playerData.Add(GameConstants.facebookPictureKey, playerPictureURL);

            UpdateUserDataRequest request = new UpdateUserDataRequest();
            request.Data = playerData;
            request.Permission = UserDataPermission.Public;

            PlayFabClientAPI.UpdateUserData(request, OnAddDataSuccess, OnAddDataError);
        }
    }

	void OnPhotonAuthenticationSuccess(GetPhotonAuthenticationTokenResult result)
	{
		Debug.Log("token!");
        playerPhotonToken = result.PhotonCustomAuthenticationToken;
	}
	
	void OnPlayFabError(PlayFabError error)
	{
		Debug.Log ("Got an error: " + error.ErrorMessage);
	}


    /// <summary>
    /// Callback called when an error occurs.
    /// </summary>
    /// <param name="error">Error information</param>
    void OnLoginError(PlayFabError error)
    {
        Debug.Log("Login error: " + error.Error + " " + error.ErrorMessage);
    }

    /// <summary>
    /// Callback called if the data saving is successful.
    /// </summary>
    /// <param name="result">Update result.</param>
    void OnAddDataSuccess(UpdateUserDataResult result)
    {
        //Everything related to login completed. Time to go back to the manager.
        //OnLoginCompletedCallback(0, 0);

        this.UpdateUserDisplayName();
    }

    /// <summary>
    /// Callback called when an error occurs.
    /// </summary>
    /// <param name="error">Error information</param>
    void OnAddDataError(PlayFabError error)
    {
        Debug.Log("Add data error: " + error.Error + " " + error.ErrorMessage);
    }

    /// <summary>
    /// Callback used when getting user data scceeded.
    /// </summary>
    /// <param name="result">User data</param>
    void OnGetUserDataSuccess(GetUserDataResult result)
    {
        //int accountLevel = int.Parse(result.Data[GameConstants.accountLevelKey].Value);
        //int accountExp = int.Parse(result.Data[GameConstants.accountExpKey].Value);

        //OnLoginCompletedCallback(accountLevel, accountExp);
        this.UpdateUserDisplayName();
    }

    /// <summary>
    /// Updates user display name.
    /// </summary>
    void UpdateUserDisplayName()
    {
        UpdateUserTitleDisplayNameRequest displayNameRequest = new UpdateUserTitleDisplayNameRequest();
        displayNameRequest.DisplayName = playerDisplayName;

        PlayFabClientAPI.UpdateUserTitleDisplayName(displayNameRequest, OnUpdatedDisplayName, OnUpdateNameError);
    }

    /// <summary>
    /// User display name successfully updated.
    /// </summary>
    /// <param name="result">Result.</param>
    void OnUpdatedDisplayName(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Display name update success. new name " + result.DisplayName);
        playerDisplayName = result.DisplayName;

        PlayFabClientAPI.GetUserCombinedInfo(new GetUserCombinedInfoRequest(), OnGetUserCombinedInfoResult, OnGetUserCombinedInfoError);
    }

    /// <summary>
    /// User display name update failed.
	/// Will try again, with a number by the username's end.
    /// </summary>
    /// <param name="error">Error details.</param>
    void OnUpdateNameError(PlayFabError error)
    {
        Debug.LogError("Update display name error : " + error.Error + " " + error.ErrorMessage);

		playerDisplayName = playerDisplayName.Replace(userNameIndex.ToString(), "");
		userNameIndex++;
		playerDisplayName += userNameIndex;
		this.UpdateUserDisplayName();
    }

    /// <summary>
    /// Getting user combined info successfully completed.
    /// </summary>
    /// <param name="result">User combined info.</param>
    private void OnGetUserCombinedInfoResult(GetUserCombinedInfoResult result)
    {
        playerID = result.PlayFabId;
        playerUsername = result.AccountInfo.Username;
        
        int currency = result.VirtualCurrency["1"];

		if (!result.Data.ContainsKey(GameConstants.accountLevelKey))
		{
			Dictionary<string, string> playerData = new Dictionary<string, string>();
			playerData.Add(GameConstants.accountLevelKey, "0");
			playerData.Add(GameConstants.accountExpKey, "0");
			playerData.Add(GameConstants.facebookPictureKey, playerPictureURL);

			UpdateUserDataRequest request = new UpdateUserDataRequest();
			request.Data = playerData;
			request.Permission = UserDataPermission.Public;

			PlayFabClientAPI.UpdateUserData(request, OnAddDataSuccess, OnAddDataError);
		}
		else
		{
			int level = int.Parse(result.Data[GameConstants.accountLevelKey].Value);
			int exp = int.Parse(result.Data[GameConstants.accountExpKey].Value);
			bool boughtShe = result.Data.ContainsKey(GameConstants.boughtSheKey);

			this.GetUserStatistics();

			OnLoginCompletedCallback(result.Inventory, playerDisplayName, currency, level, exp, boughtShe);
		}
    }

    /// <summary>
    /// Getting user combined info failed.
	/// When failed, will create all necessary keys again, and continue the login process.
    /// </summary>
    /// <param name="error">Error details.</param>
    private void OnGetUserCombinedInfoError(PlayFabError error)
    {
        Debug.Log("Get user combined info error: " + error.Error + " " + error.ErrorMessage);

		Dictionary<string, string> playerData = new Dictionary<string, string>();
		playerData.Add(GameConstants.accountLevelKey, "0");
		playerData.Add(GameConstants.accountExpKey, "0");
		playerData.Add(GameConstants.facebookPictureKey, playerPictureURL);

		UpdateUserDataRequest request = new UpdateUserDataRequest();
		request.Data = playerData;
		request.Permission = UserDataPermission.Public;

		PlayFabClientAPI.UpdateUserData(request, OnAddDataSuccess, OnAddDataError);
    }

    /// <summary>
    /// Callback called when an error occurs.
    /// </summary>
    /// <param name="error">Error information</param>
    void OnGetUserDataError(PlayFabError error)
    {
        Debug.Log("Get user data error: " + error.Error + " " + error.ErrorMessage);
    }

    /// <summary>
    /// Uploads the new account level and exp to the server.
    /// </summary>
    /// <param name="accountLevel">New account level.</param>
    /// <param name="accountExp">New account exp.</param>
    public void UpdateAccountExpAndLevel(int accountExp, int accountLevel)
    {
        Dictionary<string, string> playerData = new Dictionary<string, string>();
        playerData.Add(GameConstants.accountExpKey, accountExp.ToString());
        playerData.Add(GameConstants.accountLevelKey, accountLevel.ToString());

        UpdateUserDataRequest request = new UpdateUserDataRequest();
        request.Data = playerData;
        request.Permission = UserDataPermission.Public;

        PlayFabClientAPI.UpdateUserData(request, OnUpdateAccountExpAndLevelSuccess, OnAddDataError);
    }

    /// <summary>
    /// Callback used when the account level and exp update succeeds.
    /// </summary>
    /// <param name="result">Result.</param>
    void OnUpdateAccountExpAndLevelSuccess(UpdateUserDataResult result)
    {

    }

    /// <summary>
    /// Method used to get a defined number of users around the current user, based on a defined stat.
    /// </summary>
    /// <param name="property">Property to define the leaderboard.</param>
    /// <param name="maxResults">Maximum number of players to return.</param>
    /// <param name="OnLeaberboardLoaded">Callback when the leaderboard is loaded.</param>
    public void GetLeaderboardAroundMe(string property, int maxResults, ProjectDelegates.PlayFabLeaderboardCallback OnLeaberboardLoaded)
    {
        OnLeaderboardLoadedCallback = OnLeaberboardLoaded;

        GetLeaderboardAroundCurrentUserRequest request = new GetLeaderboardAroundCurrentUserRequest();
        request.StatisticName = property;
        request.MaxResultsCount = maxResults;

        PlayFabClientAPI.GetLeaderboardAroundCurrentUser(request, OnGetLeaderboardAroundMeResult, OnGetLeaderboardError);
    }

    /// <summary>
    /// Method used to get a defined number of users starting from a defined position, based on a defined stat.
    /// </summary>
    /// /// <param name="property">Property to define the leaderboard.</param>
    /// <param name="maxResults">Maximum number of players to return.</param>
    /// /// <param name="startPosition">Position to start getting the leaderboard.</param>
    /// <param name="OnLeaberboardLoaded">Callback when the leaderboard is loaded.</param>
    public void GetLeaderboardFromPosition(string property, int maxResults, int startPosition, ProjectDelegates.PlayFabLeaderboardCallback OnLeaberboardLoaded)
    {
        OnLeaderboardLoadedCallback = OnLeaberboardLoaded;

        GetLeaderboardRequest request = new GetLeaderboardRequest();
        request.StatisticName = property;
        request.MaxResultsCount = maxResults;
        request.StartPosition = startPosition;

        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboardFromPositionResult, OnGetLeaderboardError);
    }

    /// <summary>
    /// Callback to be called when loading the leaderboard around the current user completes.
    /// </summary>
    /// <param name="result">Leaderboard returned.</param>
    void OnGetLeaderboardAroundMeResult(GetLeaderboardAroundCurrentUserResult result)
    {
        OnLeaderboardLoadedCallback(result.Leaderboard);
    }

    /// <summary>
    /// Callback to be called when loading the leaderboard starting from a defined position completes.
    /// </summary>
    /// <param name="result">Leaderboard returned.</param>
    void OnGetLeaderboardFromPositionResult(GetLeaderboardResult result)
    {
        OnLeaderboardLoadedCallback(result.Leaderboard);
    }

    /// <summary>
    /// Callback to be called when leaderboard fails loading.
    /// </summary>
    /// <param name="error">Error info.</param>
    void OnGetLeaderboardError(PlayFabError error)
    {
        Debug.LogError("Error getting leaderboard. Error: " + error.Error + " " + error.ErrorMessage);
    }

    /// <summary>
    /// Gets all catalog items.
    /// </summary>
    /// <param name="OnCatalogLoaded">Callback when completes.</param>
    public void GetAllCatalogItems(ProjectDelegates.PlayFabCatalogListCallback OnCatalogLoaded)
    {
        this.OnCatalogLoadedCallback = OnCatalogLoaded;

        if (catalogItems == null)
        {
            GetCatalogItemsRequest request = new GetCatalogItemsRequest();
            request.CatalogVersion = "Test";
            PlayFabClientAPI.GetCatalogItems(request, OnCatalogItemsLoaded, OnCatalogItemsError);
        }
        else
        {
            OnCatalogLoadedCallback(catalogItems);
        }
    }

    /// <summary>
    /// Catalog items loaded.
    /// </summary>
    /// <param name="result">Catalog.</param>
    void OnCatalogItemsLoaded(GetCatalogItemsResult result)
    {
        catalogItems = result.Catalog;

        OnCatalogLoadedCallback(catalogItems);
    }

    /// <summary>
    /// Catalog items failed to load.
    /// </summary>
    /// <param name="error">Error details.</param>
    void OnCatalogItemsError(PlayFabError error)
    {
        Debug.LogError("Error loading store items: " + error.Error + " " + error.ErrorMessage);
    }

    /// <summary>
    /// Tries to buy item with virtual currency.
    /// </summary>
    /// <param name="item">Item to buy.</param>
    /// <param name="OnBuySuccess">Callback in case scceeds.</param>
    public void BuyItemWithVirtualCurrency(CatalogItem item,ProjectDelegates.PlayFabItemBuyCallback OnBuySuccess)
    {
        this.OnBuySuccessCallback = OnBuySuccess;

        PurchaseItemRequest request = new PurchaseItemRequest();
        request.ItemId = item.ItemId;
        request.Price = (int)item.VirtualCurrencyPrices["1"];
        request.VirtualCurrency = "1";

        PlayFabClientAPI.PurchaseItem(request, OnPurchaseSuccess, OnPurchaseError);
    }

    /// <summary>
    /// Purchase with virtual currency succeeded.
    /// </summary>
    /// <param name="result">Purchase info.</param>
    void OnPurchaseSuccess(PurchaseItemResult result)
    {
        this.OnBuySuccessCallback(result.Items);
    }

    /// <summary>
    /// Purchase with virtual currency failed.
    /// </summary>
    /// <param name="error">Error details.</param>
    void OnPurchaseError(PlayFabError error)
    {
        Debug.LogError("Error buying item: " + error.Error + " " + error.ErrorMessage);
    }

    /// <summary>
    /// Trying to consume item we just bought.
    /// </summary>
    /// <param name="itemInstanceID">Item instance ID.</param>
    public void ConsumeItem(string itemInstanceID)
    {
        ConsumeItemRequest request = new ConsumeItemRequest();
        request.ItemInstanceId = itemInstanceID;
        request.ConsumeCount = 1;

        PlayFabClientAPI.ConsumeItem(request, OnConsumeItemSuccess, OnConsumeItemError);
    }

    /// <summary>
    /// Successfully consumed item.
    /// </summary>
    /// <param name="result">Result.</param>
    void OnConsumeItemSuccess(ConsumeItemResult result)
    {
        Debug.Log("successfully consumed item");
    }

    /// <summary>
    /// Consuming item failed.
    /// </summary>
    /// <param name="error">Error details.</param>
    void OnConsumeItemError(PlayFabError error)
    {
        Debug.LogError("Error consuming item: " + error.Error + " " + error.ErrorMessage);
    }

    /// <summary>
    /// Reports user's kill statistics.
    /// </summary>
    /// <param name="amount"></param>
    public void ReportKillStats(int amount)
    {
        UpdateUserStatisticsRequest request = new UpdateUserStatisticsRequest();
        request.UserStatistics = new Dictionary<string, int>();
        request.UserStatistics.Add(GameConstants.killsStatsKey, amount);

        PlayFabClientAPI.UpdateUserStatistics(request, OnUpdateStatsCompleted, OnUpdateStatsError);
    }

    /// <summary>
    /// Reports user's wins statistics.
    /// </summary>
    /// <param name="amount"></param>
    public void ReportWins(int amount)
    {
        UpdateUserStatisticsRequest request = new UpdateUserStatisticsRequest();
        request.UserStatistics = new Dictionary<string, int>();
        request.UserStatistics.Add(GameConstants.winsStatsKey, amount);

        PlayFabClientAPI.UpdateUserStatistics(request, OnUpdateStatsCompleted, OnUpdateStatsError);
    }

    /// <summary>
    /// Updating stats completed.
    /// </summary>
    /// <param name="result">Result.</param>
    void OnUpdateStatsCompleted(UpdateUserStatisticsResult result)
    {
        Debug.Log("Successfully updated stats");
    }

    /// <summary>
    /// Updating kill stats failed.
    /// </summary>
    /// <param name="error">Error details.</param>
    void OnUpdateStatsError(PlayFabError error)
    {
        Debug.LogError("Error updating stat: " + error.Error + " " + error.ErrorMessage);
    }

    /// <summary>
    /// Method used to get user statistics from PlayFab.
    /// </summary>
    public void GetUserStatistics()
    {
        GetUserStatisticsRequest request = new GetUserStatisticsRequest();
        PlayFabClientAPI.GetUserStatistics(request, OnUserStatisticsLoaded, OnGetUserStatisticsError);
    }

    /// <summary>
    /// Callback used when user statistics load scceeds.
    /// </summary>
    /// <param name="result">User statistics info.</param>
    void OnUserStatisticsLoaded(GetUserStatisticsResult result)
    {
        int kills;
        result.UserStatistics.TryGetValue(GameConstants.killsStatsKey,out kills);
        int wins;
        result.UserStatistics.TryGetValue(GameConstants.winsStatsKey, out wins);
        AccountManager.instance.UpdateUserStatistics(kills, wins);
    }

    /// <summary>
    /// Getting user statistics failed.
    /// </summary>
    /// <param name="error">Error details.</param>
    void OnGetUserStatisticsError(PlayFabError error)
    {
        Debug.LogError("Error getting stats: " + error.Error + " " + error.ErrorMessage);
    }

    /// <summary>
    /// Method used to ask PlayFab server for ingame news.
    /// </summary>
    /// <param name="amount">Amount of news to bring.</param>
    public void GetGameNews(int amount)
    {
        GetTitleNewsRequest request = new GetTitleNewsRequest();
        request.Count = amount;

        PlayFabClientAPI.GetTitleNews(request, OnGameNewsLoaded, OnGameNewsLoadFailed);
    }

    /// <summary>
    /// Callback received when ingame news are loaded.
    /// </summary>
    /// <param name="result">Result from PlayFab.</param>
    void OnGameNewsLoaded(GetTitleNewsResult result)
    {
        gameNews = result.News;
    }

    /// <summary>
    /// Callback received when ingame news loading returns an error.
    /// </summary>
    /// <param name="error">Error details.</param>
    void OnGameNewsLoadFailed(PlayFabError error)
    {
        Debug.LogError("Error getting game news: " + error.Error + " " + error.ErrorMessage);
    }

    /// <summary>
    /// Gets logged player's PlayFab ID.
    /// </summary>
    /// <returns>PlayFab ID.</returns>
    public string GetMyPlayerID()
    {
        return playerID;
    }

	// this should be done with cloud script
    /// <summary>
    /// Sends a push notification request to PlayFab.
    /// </summary>
    /// <param name="playFabID">Recipient's PlayFab ID.</param>
    /// <param name="message">Message to send.</param>
    public void SendPush(string playFabID, string message)
    {
        PlayFab.Server.SendPushNotificationRequest request = new PlayFab.Server.SendPushNotificationRequest();
        request.Recipient = playFabID;
        request.Message = message;
//        PlayFabServerAPI.SendPushNotification(request, OnSendPushNotificationSuccess, OnSendPushNotificationError);

    }

    /// <summary>
    /// Event called when push notification is successfully sent.
    /// </summary>
    /// <param name="result">Result data.</param>
    private void OnSendPushNotificationSuccess(PlayFab.Server.SendPushNotificationResult result)
    {
        Debug.Log("Successfully sent push notification");
    }

    /// <summary>
    /// Event called when push notification has failed to send.
    /// </summary>
    /// <param name="error">Error details.</param>
    private void OnSendPushNotificationError(PlayFabError error)
    {
        Debug.LogError("Error sending push notification: " + error.Error + " " + error.ErrorMessage);
    }

    /// <summary>
    /// Gets All title data from PlayFab.
    /// </summary>
    public void LoadTitleData()
    {
        GetTitleDataRequest request = new GetTitleDataRequest();
        request.Keys = new List<string>() { "Key_Home", "Key_Play", "Key_Store", "Key_Settings", "Key_Tutorial", 
            "Key_MainMenuBtn", "Key_StoreBtn", "Key_PlayAgainBtn", "Key_Loading", "Key_SelectChar", "Key_Stats", 
            "Key_ChangeTeam", "Key_Vs", "Key_TypeMessage", "Key_LeaveGameBtn", "Key_Player", "Key_Statistics", 
            "Key_Options", "Key_LeaveGameTitle", "Key_LeaveGameText", "Key_Yes", "Key_No", "Key_ModeSelectTitle", 
            "Key_CreateMatch", "Key_JoinMatch", "Key_ComingSoon", "Key_LeaderboardTitle", "Key_Level", "Key_Kills", 
            "Key_Wins", "Key_Inventory", "Key_XP", "Key_BuySecondChar", "Key_BuySecondCharText", "Key_NoThanks", 
            "Key_OhYes", "Key_Tutorial_Pg1Item1", "Key_Tutorial_Pg1Item2", "Key_Tutorial_Pg1Item3", "Key_Tutorial_Pg1Item4", 
            "Key_Tutorial_Pg2Item1", "Key_Tutorial_Pg2Item2", "Key_Tutorial_Pg2Item3", "Key_Tutorial_Pg2Item4" };

        PlayFabClientAPI.GetTitleData(request, OnTitleDataLoadSuccess, OnTitleDataLoadError);
    }

    /// <summary>
    /// Event called when title data load completes.
    /// </summary>
    /// <param name="result">Title data.</param>
    private void OnTitleDataLoadSuccess(GetTitleDataResult result)
    {
        titleData = result.Data;
    }

    /// <summary>
    /// Event called when title data load fails.
    /// </summary>
    /// <param name="error">Error data.</param>
    private void OnTitleDataLoadError(PlayFabError error)
    {
        Debug.LogError("Error getting title data: " + error.Error + " " + error.ErrorMessage);
    }

    /// <summary>
    /// Gets a specific title value, based on sent key. Must be called after LoadTitleData() completes.
    /// </summary>
    /// <param name="key">Key to find value.</param>
    /// <returns>Value, if found.</returns>
    public string GetTitleValue(string key)
    {
        string value = "";
        if (titleData != null)
        {
            titleData.TryGetValue(key, out value);
        }
        return value;
    }
}