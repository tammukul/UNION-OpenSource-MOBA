Union: (A PlayFab-Photon MOBA) README
========
1. Overview:
----
Union is a sample multiplayer online battle arena (MOBA) game for mobile devices, designed to show off Photon and PlayFab integration with a Unity game. It is fast-paced, intense and combat focused, with two teams of players teaming up for a common objective: destroy the enemy’s tower and all enemy players. All the assets, prefabs, models, sounds and code are open source and can be used in your own game.


##### This demo illustrates:
* How to use PlayFab and Facebook for:
  * Getting and sending authentication tokens
  * Populate PlayFab account details with Facebook data (profile picture, account name)
* Use PlayFab TitleData as a string table for 'hot-loading' your game's text content
* Making Google Play IAP requests and validating the receipt with PlayFab
* Example of how to create real-time multiplayer MOBA with Photon PUN
  * Simple game system examples (movement, combat, defense towers, UI, AI)
  * Dual touchscreen input for mobile platforms
  * Photon networking implementation using PUN, with extensive examples of Views, Syncs and RPCs
  * In-game store for character upgrades 
  * Kills and win/loss ratios are tracked via PlayFab PlayerStatistics
  * Compare your stats to others via PlayFab Leaderboards


2. Prerequisites:
----
To get the most from these samples you should:
* Be familiar with the basics of PlayFab 
* Be familiar with the basics of Photon (PUN)
* Play:
  * For best results: 
    * Have the ability to build and deploy to an android device (.apk provided as an alternative)
    * Have a Facebook account for authentication purposes
  * Alternatively:
    * Run in the UnityEditor with mouse support
    * Requires a Facebook Developer Account to generate an authorization token

To connect to the PlayFab service, your machine must be running TLS v1.1 or better.
  * For Windows, this means Windows 7 and above
    * [Official Microsoft Documentation](https://msdn.microsoft.com/en-us/library/windows/desktop/aa380516%28v=vs.85%29.aspx)
    * [Support for SSL/TLS protocols on Windows](http://blogs.msdn.com/b/kaushal/archive/2011/10/02/support-for-ssl-tls-protocols-on-windows.aspx)

3. Source Code & Key Repository Components:
----
This repository contains the entire Unity3d project. 

#### Main Classes that drive featured functionality:
* AccountManager
  * Persistent to control all account properties getters and setters. Communicates with Facebook and PlayFab, and holds every property any of these communications may return. Called anywhere in the game, because account’s properties can be changed on gameplay, on store, and will be shown in profile screen.
* PhotonManager
  * Persistent to control connection. Connects to Photon and finds a room on ModeSelect, handles all gameplay syncs and events on MatchRoom, and gets destroyed on EndGame. This maintains connection between scenes.
* AudioManager
  * Persistent to handle properties and play music. Has an AudioSource, that keeps playing menu (or gameplay) music in loop, and holds booleans for music enabled, and SFX enabled. Because there is SFX and music in every scene, the manager is persistent.

#### 3rd Party Plugins:
| Class        | Function           | External Site  |
| ------------- | ------------- | ----- |
| Photon (PUN) | SDK for accessing networking features | https://www.photonengine.com/en/PUN |
| OpenIAB      | SDK for accessing native billing APIs   | https://github.com/onepf/OpenIAB |
| Unity-GCM | SDK for registering to the GCM service | https://github.com/kobakei/unity-gcm |
| Facebook | SDK for accessing Facebook APIs | https://developers.facebook.com/docs/unity |

#### Singleton Usage, allows easy access to these systems from anywhere in the project
##### Documented here for posterity:
* AccountManager
* PlayFabManager
* AudioManager
* GameController
* GameplayUI
* PlayFabPlayerStats
* PlayFabItemsController
* GunDropsManager
* GameController
* GameplaySFXManager
* InputManager
* PushNotificationsManager
* MultiplayerRoomsManager
* ChatManager
* InGameStoreAndCurrencyManager
* InAppPurchaseManager


4. Installation & Configuration Instructions:
----
After downloading, the Unity project will be under the Project folder.
If some errors occurs, it is recommended to delete the folder Photon Unity Networking, and re-import the plugin PUN from Asset Store.

##### How to run the game in editor mode:
* This requires you to have access to a Facebook developer account
1. Find a Facebook App ID to use
2. Enter the App ID in the Unity Facebook SDK editor menu (Facebook > Edit Settings > [App ID located in the inspector panel, paste in your App ID])
3. Open the Landing.unity scene, Play in editor
4. While logged in as a Facebook developer, navigate to the Facebook Access Token Tool 
  * https://developers.facebook.com/tools/accesstoken/
5. Enter the provided UserToken as your credential
6. To enable mouse support:
  * Find the GameControls GameObject in the scene
  * Check the UseWithMouse property on the InputManager 
  * [note:] the mouse is acts as a single touch input. Although it may be a bit awkward in the editor, click->dragging on the left-half of the screen will move the character directionally, while click->dragging on the right-half of the screen will shoot directionally. For best results play on a multi-touch Android device.

##### How to replicate the Union PlayFab environment to your own title
* InAppPurchasing will require you to have your own GooglePlay Account information
* Obtain your own Photon Application ID via the Game Manager > Servers > Photon

1. Upload the game logic (/Documentation/CloudScript.js) via the Game Manager > Servers > CloudScripts > Create New Revision
2. Upload the game item catalog (/Documentation/InGameStore.json) via the Game Manager > Economy > Catalogs > Upload JSON
3. Upload the TitleData via the [Admin API](https://api.playfab.com/Documentation/Admin/method/SetTitleData)
  * Run the following command for each title data element in /Documentation/TitleData.json:
    `
    curl -XPOST https://2ABE.playfabapi.com/admin/SetTitleData \
    -H "Content-Type: application/json" \
    -H "X-SecretKey: 000-ENTER-YOUR-KEY-HERE-000" \
    -d "COPY AND PASTE EACH ELEMENT FROM /Documentation/TitleData.json"   
    `
4. Ensure your title's virtual currencies match those listed in /Documentation/VirtualCurrencies.json:
  1. Manually adding them via the Game Manager > Economy > Currencies
  2. Run the following [Admin command](https://api.playfab.com/Documentation/Admin/method/AddVirtualCurrencyTypes) with the data in /Documentation/VirtualCurrencies.json:
    `
    curl -XPOST https://2ABE.playfabapi.com/admin/AddVirtualCurrencyTypes \
    -H "Content-Type: application/json" \
    -H "X-SecretKey: 000-ENTER-YOUR-KEY-HERE-000" \
    -d "COPY AND PASTE THE CONTENTS FROM /Documentation/VirtualCurrencies.json" 
    `

5. Usage Instructions:
----
The game must​ be played (started) from the Landing scene. It will initialize all the needed references (Facebook, PlayFab, audio manager, etc), and start the login process, to be able to access all features, and properly connect to Photon and join a room.

See extra documentation:
  1. Documentation/Union-Usagedocument.pdf
  2. Documentation/Union Visual Guide.pptx

6. Troubleshooting:
----
For a complete list of available PlayFab APIs, check out the [online documentation](http://api.playfab.com/Documentation/).

#### Contact Us
We love to hear from our developer community! 
Do you have ideas on how we can make our products and services better? 

Our Developer Success Team can assist with answering any questions as well as process any feedback you have about PlayFab services.

[Forums, Support and Knowledge Base](https://support.playfab.com/support/home)

#### Known Issues:
* Complete build for Android only. Building to other platforms may not work as expected
* PUN does not currently support Photon webhooks
* PlayerName & HealthBar UI sometimes separate from the actor GameObject
* TouchScreen joystick UI is not under the touch

7. Copyright and Licensing Information:
----
  Apache License -- 
  Version 2.0, January 2004
  http://www.apache.org/licenses/

  Full details available within the LICENSE file.

#### Special Thanks
* Union MOBA was developed for PlayFab by [Flux Studios](http://www.fluxgamestudio.com/)


8. Version History:
----
* (v1.00) Initial Release

