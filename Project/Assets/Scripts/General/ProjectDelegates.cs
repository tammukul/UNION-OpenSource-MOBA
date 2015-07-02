using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels;

/// <summary>
/// Delegates class, containing references for all the delegates used.
/// </summary>
public class ProjectDelegates
{
    /// <summary>
    /// Delegate used when facebook login completes.
    /// </summary>
    /// <param name="accessToken">Access Token from Facebook.</param>
    /// <param name="facebookName">User's Facebook name.</param>
    /// <param name="facebookPictureURL">User's Facebook picture URL.</param>
    public delegate void FacebookLoginCallback(string accessToken, string facebookName, string facebookPictureURL);

    /// <summary>
    /// Delegate used when user enters the lobby.
    /// </summary>
    /// <param name="rooms">All the rooms in the lobby.</param>
    public delegate void RoomsListCallback(RoomInfo[] rooms);

    /// <summary>
    /// Delegate used when user enters a room.
    /// </summary>
    /// <param name="room">Room info.</param>
    public delegate void RoomInfoCallback(Room room);

    /// <summary>
    /// Delegate used when some method is needed to call, and has no parameters in it.
    /// </summary>
    public delegate void SimpleCallback();

    /// <summary>
    /// Delegate used when PlayFab login completes.
    /// </summary>
    /// <param name="inventory">Player inventory list.</param>
    /// <param name="displayName">Player's display name.</param>
    /// <param name="currencyAmount">Player's currency amount.</param>
    /// <param name="accountLevel">Player level.</param>
    /// <param name="accountExp">Player exp amount.</param>
    /// <param name="boughtShe">Has the player bought the second character?</param>
    public delegate void PlayFabLoginCallback(List<ItemInstance> inventory, string displayName, int currencyAmount, int accountLevel, int accountExp,bool boughtShe);

    /// <summary>
    /// Delegate used when PlayFab leaderboard is loaded.
    /// </summary>
    /// <param name="leaderboard">Leaderboard info.</param>
    public delegate void PlayFabLeaderboardCallback(List<PlayerLeaderboardEntry> leaderboard);

    /// <summary>
    /// Delegate used when PlayFab store catalog is loaded.
    /// </summary>
    /// <param name="catalogItems">Catalog info.</param>
    public delegate void PlayFabCatalogListCallback(List<CatalogItem> catalogItems);

    /// <summary>
    /// Delegate used when the player successfully buys an item from PlayFab.
    /// </summary>
    /// <param name="itemsBought">Items the user bought.</param>
    public delegate void PlayFabItemBuyCallback(List<PurchasedItem> itemsBought);

    /// <summary>
    /// Delegate used to handle stat upgrade, after item purchase.
    /// </summary>
    /// <param name="multiplier">Stat multiplier.</param>
    public delegate void OnPlayerBoughtStatUpgradeCallback(float multiplier);

    /// <summary>
    /// Delegate used when screen touch starts.
    /// </summary>
    /// <param name="touch">Touch info.</param>
    public delegate void TouchStartCallback(CustomTouch touch);
    /// <summary>
    /// Delegate usen when screen touch moves.
    /// </summary>
    /// <param name="touch">Touch info.</param>
    /// <param name="deltaPosition">Changed position.</param>
    public delegate void TouchMoveCallback(CustomTouch touch, Vector2 deltaPosition);
    /// <summary>
    /// Delegate used when screen touch ends.
    /// </summary>
    /// <param name="touch">Touch info.</param>
    public delegate void TouchEndCallback(CustomTouch touch);
}