using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels;

public class AccountManager : MonoBehaviour
{
    /// <summary>
    /// Static instance.
    /// </summary>
    public static AccountManager instance;

    /// <summary>
    /// Experience amount needed for each level.
    /// </summary>
    int[] _experienceForEveryLevel = new int[] { 0, 1000, 4153, 7325, 10520, 13740, 16989, 20270, 23587, 26946, 
                                        31487, 36098, 40788, 45569, 50450, 55446, 60572, 65844, 71282, 76906, 
                                        84201, 91793, 99723, 108034, 116775, 126002, 135779, 146177, 157276, 169169, 
                                        184515, 201076, 219011, 238496, 259735, 282955, 308414, 336402, 367248, 401325, 445339, 
                                        494165, 548429, 608837, 676188, 751384, 835446, 929526, 1034927, 1203773 };

    /// <summary>
    /// Property reference for experience amount needed for each level.
    /// </summary>
    public int[] experienceForEveryLevel
    {
        get { return _experienceForEveryLevel; }
    }

    /// <summary>
    /// Login completed callback.
    /// </summary>
    ProjectDelegates.SimpleCallback OnLoginCompleted;

    /// <summary>
    /// Facebook manager reference.
    /// </summary>
    FacebookManager facebookManager;
    /// <summary>
    /// PlayFab manager reference.
    /// </summary>
    PlayFabManager playFabManager;

    /// <summary>
    /// Player inventory.
    /// </summary>
    //List<ItemInstance> inventory;

    /// <summary>
    /// Player display name.
    /// </summary>
    public string displayName { get; private set; }

    /// <summary>
    /// User's picture from facebook.
    /// </summary>
    public string facebookPictureURL { get; private set; }

    /// <summary>
    /// Player virtual currency amount.
    /// </summary>
    public int currencyAmount { get; private set; }

    /// <summary>
    /// Player Account level.
    /// </summary>
    public int accountLevel { get; private set; }
    /// <summary>
    /// Player Account experience points.
    /// </summary>
    public int accountExp { get; private set; }

    /// <summary>
    /// Player's global kill count.
    /// </summary>
    public int killCount { get; set; }

    /// <summary>
    /// Player's number of win.
    /// </summary>
    public int winsAmount { get; set; }

    public bool boughtShe { get; private set; }

    /// <summary>
    /// Prevents object from being destroyed, gets necessary references.
    /// </summary>
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        facebookManager = this.GetComponent<FacebookManager>();
        playFabManager = this.GetComponent<PlayFabManager>();
    }

    /// <summary>
    /// Starts facebook login.
    /// </summary>
    /// <param name="OnLoginCompleted">Login completed callback.</param>
    public void LoginWithFacebook(ProjectDelegates.SimpleCallback OnLoginCompleted)
    {
        this.OnLoginCompleted = OnLoginCompleted;
        facebookManager.Initialize(OnFacebookLoginCompleted);
        //this.OnFacebookLoginCompleted("CAAKVX8jV8zcBACCXCuUF8zsOCZApmCKnqtbpubD0cPIYoasjgtizxF6DmAgDkZBxCpPnfZCgyAPpGob2OkdinoOZCPy35nm1AJ3F3gET9kwS9850iKkwzNhZCARvytWYLTQfvl1Uqe02ANJVy5tPT2jlCmrmXCfi0nW2wWM18aPy58T4TmPkTPXUhJhOgrBUY9xW2e88lNW9Vs3aSBWEewru83KlhkYUZD", "Igor Pereira");
    }

    /// <summary>
    /// Logs out from services.
    /// </summary>
    public void LogoutFromEverything()
    {
        facebookManager.Logout();
        playFabManager.Logout();
    }

    /// <summary>
    /// Called when facebook login completes.
    /// </summary>
    /// <param name="accessToken">Facebook's access token</param>
    /// <param name="facebookName">Facebook's user name</param>
    void OnFacebookLoginCompleted(string accessToken, string facebookName, string facebookPictureURL)
    {
        this.facebookPictureURL = facebookPictureURL;

        playFabManager.LoginWithFacebook(accessToken, facebookName, facebookPictureURL, OnPlayFabLoginCompleted);
    }

    /// <summary>
    /// Called when PlayFab login completed.
    /// </summary>
    /// <param name="inventory">User inventory.</param>
    /// <param name="displayName">User display name.</param>
    /// <param name="currency">User currrency amount.</param>
    /// <param name="accountLevel">User account level.</param>
    /// <param namhttp://open.spotify.com/app/radioe="accountExp">User account experience points.</param>
    void OnPlayFabLoginCompleted(List<ItemInstance> inventory,string displayName, int currency, int accountLevel, int accountExp,bool boughtShe)
    {
        //this.inventory = inventory;
        this.currencyAmount = currency;

        this.displayName = displayName;

        this.accountLevel = accountLevel;
        this.accountExp = accountExp;

        this.boughtShe = boughtShe;

        OnLoginCompleted();

        playFabManager.GetGameNews(21);
    }

    /// <summary>
    /// Method used to update user statistics, before sendinf to PlayFab.
    /// </summary>
    /// <param name="killCount">KillCount number.</param>
    /// <param name="winsCount">Number of wins.</param>
    public void UpdateUserStatistics(int killCount, int winsCount)
    {
        this.killCount = killCount;
        this.winsAmount = winsCount;
    }

    /// <summary>
    /// Gives the player a number of levels.
    /// </summary>
    /// <param name="amount">Number of levels to give.</param>
    public void GiveAccountLevel(int amount)
    {
        accountLevel += amount;

        playFabManager.UpdateAccountExpAndLevel(accountExp, accountLevel);
    }

    /// <summary>
    /// Gives the player account a number of experience points.
    /// </summary>
    /// <param name="amount">Experience points amount to give.</param>
    public void GiveAccountExp(int amount)
    {
        accountExp += amount;
        this.CheckAccountLevel();
    }

    /// <summary>
    /// Checks if the player can level up, and saves everything.
    /// </summary>
    void CheckAccountLevel()
    {
        if (accountLevel == _experienceForEveryLevel.Length-1) return;

        int nextExp = _experienceForEveryLevel[accountLevel + 1];
        if (accountExp > nextExp)
        {
            accountLevel++;
            playFabManager.UpdateAccountExpAndLevel(accountExp, accountLevel);
        }
    }

    /// <summary>
    /// Callback method, usen when the purchase of the second character is complete.
    /// </summary>
    internal void BuyShe()
    {
        this.boughtShe = true;
    }
}