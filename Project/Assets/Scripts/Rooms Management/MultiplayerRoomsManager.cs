using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon;
using ExitGames.Client.Photon;

public class MultiplayerRoomsManager : Photon.MonoBehaviour
{
    /// <summary>
    /// Static instance.
    /// </summary>
    public static MultiplayerRoomsManager instance;

    /// <summary>
    /// Callback on connected to photon.
    /// </summary>
    ProjectDelegates.RoomsListCallback OnPhotonConnected;

    /// <summary>
    /// Callback on joined photon room.
    /// </summary>
    ProjectDelegates.RoomInfoCallback OnJoinedRoomCallback;

    /// <summary>
    /// Callback on another player joined room.
    /// </summary>
    ProjectDelegates.RoomInfoCallback OnRoomPropertiesUpdate;

    /// <summary>
    /// Callback to start the game timer.
    /// </summary>
    ProjectDelegates.SimpleCallback OnTimerStarted;

    /// <summary>
    /// Callback to start game.
    /// </summary>
    ProjectDelegates.SimpleCallback OnStartGame;

    /// <summary>
    /// Callback to stop the game timer.
    /// </summary>
    ProjectDelegates.SimpleCallback OnTimerStop;

    /// <summary>
    /// Actual room info.
    /// </summary>
    public Room myRoomInfo { get; private set; }

    /// <summary>
    /// Feedback to show player the connection has failed.
    /// </summary>
    [SerializeField]
    GameObject connectionErrorFeedback;

    /// <summary>
    /// All players names' reference.
    /// </summary>
    [SerializeField]
    string[] players;

    /// <summary>
    /// Players in the room.
    /// </summary>
    List<PhotonPlayer> playersInRoom;

    /// <summary>
    /// ObjectInstantiator reference.
    /// </summary>
    RoomNetworkLayer networkLayer;

    /// <summary>
    /// Character I chose to use.
    /// </summary>
    CharacterTypes myCharacter = CharacterTypes.He;

    /// <summary>
    /// Team I'm on.
    /// </summary>
    TeamTypes myTeam;

    /// <summary>
    /// Number of players expected in room.
    /// </summary>
    int maxPlayers;

    /// <summary>
    /// Player level;
    /// </summary>
    int level;

    /// <summary>
    /// Boolean checking if the bots has already been instantiated.
    /// </summary>
    bool botsIntantiated;


	public string AppId;

    /// <summary>
    /// Sets all references.
    /// </summary>
    void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// Method used to initialize and connect to Photon servers.
    /// </summary>
    public void Initialize(ProjectDelegates.RoomsListCallback ConnectedCallback)
    {
        playersInRoom = new List<PhotonPlayer>();
        OnPhotonConnected = ConnectedCallback;

        if (!PhotonNetwork.connected)
        {


            PhotonNetwork.autoJoinLobby = true;
            PhotonNetwork.ConnectUsingSettings("4.5");
            PhotonNetwork.playerName = AccountManager.instance.displayName;
        }
        else
        {
            ConnectedCallback(PhotonNetwork.GetRoomList());
        }
    }


    /// <summary>
    /// Updates Callback to handle room properties change.
    /// </summary>
    /// <param name="OnPlayersUpdate">Callback.</param>
    public void SetPropertiesChangeCallback(ProjectDelegates.RoomInfoCallback OnPlayersUpdate)
    {
        OnRoomPropertiesUpdate = OnPlayersUpdate;
    }

    /// <summary>
    /// Updates Callback to handle timer start.
    /// </summary>
    /// <param name="OnTimerStarted">Callback.</param>
    public void SetOnTimerStarted(ProjectDelegates.SimpleCallback OnTimerStarted)
    {
        this.OnTimerStarted = OnTimerStarted;
    }

    /// <summary>
    /// Updates Callback to handle start game.
    /// </summary>
    /// <param name="OnStartGame">Callback.</param>
    public void SetOnStartGame(ProjectDelegates.SimpleCallback OnStartGame)
    {
        this.OnStartGame = OnStartGame;
    }

    /// <summary>
    /// Updates callback to handle timer stop.
    /// </summary>
    /// <param name="OnTimerStop">Callback.</param>
    public void SetOnTimerStop(ProjectDelegates.SimpleCallback OnTimerStop)
    {
        this.OnTimerStop = OnTimerStop;
    }

    /// <summary>
    /// Callback used when connection to Photon servers fail.
    /// </summary>
    /// <param name="cause">The connection fail cause.</param>
    public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.LogError("Failed to connect to Photon Server. Cause: " + cause);

        connectionErrorFeedback.SetActive(true);
    }

    /// <summary>
    /// Callback used when the game has successfully connected to Photon servers, and is on the Lobby.
    /// </summary>
    public virtual void OnJoinedLobby()
    {
        OnPhotonConnected(PhotonNetwork.GetRoomList());
    }

    /// <summary>
    /// Method to be called when the user wants to create a new room.
    /// </summary>
    public void CreateNewRoom(int numberOfPlayers,ProjectDelegates.RoomInfoCallback RoomCreatedCallback, int level)
    {
        OnJoinedRoomCallback = RoomCreatedCallback;

        ExitGames.Client.Photon.Hashtable customParams = new ExitGames.Client.Photon.Hashtable();
        customParams.Add("C0", level);

        RoomOptions options = new RoomOptions();
        options.maxPlayers = numberOfPlayers;

        TypedLobby typedLobby = new TypedLobby("GameLobby",LobbyType.Default);

		//PhotonNetwork.CreateRoom ("",options,typedLobby);
		//PhotonNetwork.CreateRoom ("", true, true, numberOfPlayers);
		// testing this to git rid of the build warning
		PhotonNetwork.CreateRoom("", options, typedLobby);
		
    }

    /// <summary>
    /// Method to be called when the user wants to try to connect to an existing room.
    /// </summary>
    public void JoinRoom(byte maxPlayers, int level, ProjectDelegates.RoomInfoCallback JoinedRoomCallback)
    {
        this.maxPlayers = maxPlayers;
        this.level = level;
        OnJoinedRoomCallback = JoinedRoomCallback;

        string sqlFilter = "C0 > " + (level-3) + " AND C0 < " + (level+3);

		Debug.Log (sqlFilter);

        TypedLobby typedLobby = new TypedLobby("GameLobby",LobbyType.Default);


        PhotonNetwork.JoinRandomRoom(null,maxPlayers,MatchmakingMode.FillRoom,typedLobby,"");
		//PhotonNetwork.JoinRandomRoom (, MatchmakingMode.FillRoom, typedLobby, sqlFilter);
    }

    /// <summary>
    /// Starts game.
    /// </summary>
    public void StartGame()
    {
		ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
		properties.Add("Ready", false);
		PhotonNetwork.player.SetCustomProperties(properties);

        networkLayer.ActivateLevel();
        //networkLayer.AllocateViewIDAndCallInstantiate(myCharacter, myTeam);
    }

    /// <summary>
    /// Rotates player between teams.
    /// </summary>
    public void ChangeTeam(TeamTypes newTeam, bool syncNetwork)
    {
        myTeam = newTeam;

        ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
        properties.Add("Team", myTeam.GetHashCode());
        PhotonNetwork.player.SetCustomProperties(properties);

        if (syncNetwork)
        {
            networkLayer.OnPropertiesChanged();
        }
    }

    /// <summary>
    /// Changes player avatar, to the one the player has chosen.
    /// </summary>
    /// <param name="myType">New character.</param>
    /// <param name="syncNetwork">If will send data to the network.</param>
    public void ChangePlayerAvatar(CharacterTypes myType, bool syncNetwork)
    {
        if (myCharacter != myType)
        {
            myCharacter = myType;

            ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
            properties.Add("Character", myCharacter.GetHashCode());
            PhotonNetwork.player.SetCustomProperties(properties);

            if (syncNetwork)
            {
                networkLayer.OnPropertiesChanged();
            }
        }
    }

    /// <summary>
    /// Callback used when the player successfully joins a room.
    /// </summary>
    public void OnJoinedRoom()
    {
        playersInRoom = new List<PhotonPlayer>(PhotonNetwork.playerList);
        myTeam = (TeamTypes)((playersInRoom.Count + 1) % 2);


        ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
        properties.Add("Team", myTeam.GetHashCode());
        properties.Add("Character", myCharacter.GetHashCode());
        properties.Add("PlayFabID", PlayFabManager.instance.GetMyPlayerID());

        bool containsOwner = false;
        for (int i = 0; i < PhotonNetwork.otherPlayers.Length; i++)
        {
            object ownerName;
            if (PhotonNetwork.otherPlayers[i].customProperties.TryGetValue("Owner", out ownerName))
            {
                containsOwner = true;
                break;
            }
        }
        if (!containsOwner)
        {
            properties.Add("Owner", PhotonNetwork.player.name);
        }
        PhotonNetwork.player.SetCustomProperties(properties);

        myRoomInfo = PhotonNetwork.room;

        OnJoinedRoomCallback(PhotonNetwork.room);
    }

    /// <summary>
    /// Method to be called when the user wants to exit their current room.
    /// </summary>
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    /// <summary>
    /// Event called when connects to Photon.
    /// </summary>
    void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    /// <summary>
    /// Event called when a random room join fails.
    /// </summary>
    void OnPhotonRandomJoinFailed()
    {
		Debug.Log ("Random join failed");
        this.CreateNewRoom(maxPlayers,OnJoinedRoomCallback,level);
    }

    /// <summary>
    /// Callback used when another player joins the room.
    /// </summary>
    /// <param name="netPlayer">The other player info</param>
    void OnPhotonPlayerConnected(PhotonPlayer netPlayer)
    {
        
    }

    /// <summary>
    /// Callback used when another player leaves the room.
    /// </summary>
    /// <param name="netPlayer">The other player info</param>
    void OnPhotonPlayerDisconnected(PhotonPlayer netPlayer)
    {
        playersInRoom.Remove(netPlayer);

        networkLayer.OnPropertiesChanged();
    }

    /// <summary>
    /// Dispose method. Disconnects from Photon, and destroys the GameObject.
    /// </summary>
    public void Dispose()
    {
        PhotonNetwork.Disconnect();
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Gets all players references.
    /// </summary>
    /// <returns></returns>
    public PhotonPlayer[] GetAllPhotonPlayers()
    {
        return playersInRoom.ToArray();
    }

    /// <summary>
    /// Gets network layer reference.
    /// </summary>
    public void InitializeNetworkLayer()
    {
        networkLayer = GameObject.FindObjectOfType<RoomNetworkLayer>();
        networkLayer.roomsManager = this;
    }

    /// <summary>
    /// Event called when some property of some player has changed in the room (team, character, or ready status).
    /// Calles on player join/leave too.
    /// </summary>
    public void OnNetworkRoomChanged()
    {
        OnRoomPropertiesUpdate(PhotonNetwork.room);
    }

    /// <summary>
    /// Gets room capacity.
    /// </summary>
    /// <returnsRoom capacity.></returns>
    public int GetMaxPlayers()
    {
        return maxPlayers;
    }

    /// <summary>
    /// Calls for sync method on the network.
    /// </summary>
    public void CallNetworkSync()
    {
        networkLayer.OnPropertiesChanged();
    }

    /// <summary>
    /// Updates player status to ready.
    /// </summary>
    public void SetReady()
    {
        ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
        properties.Add("Ready", true);
        PhotonNetwork.player.SetCustomProperties(properties);

        networkLayer.OnPropertiesChanged();
    }

    /// <summary>
    /// Starts the game timer.
    /// </summary>
    public void StartTimer()
    {
       networkLayer.StartTimer();
    }

    /// <summary>
    /// If the owner of the room, instantiates bots.
    /// </summary>
    /// <param name="teamsCount"></param>
    public void InstantiateBots(int[] teamsCount)
    {
        if (!botsIntantiated)
        {
            botsIntantiated = true;

            object owner;
            if (PhotonNetwork.player.customProperties.TryGetValue("Owner", out owner))
            {
                ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
                properties.Remove("Owner");
                PhotonNetwork.player.SetCustomProperties(properties);

                string ownerName = owner.ToString();
                if (ownerName == PhotonNetwork.player.name)
                {
                    networkLayer.InstantiateBots(teamsCount);
                }
            }
        }
    }

    /// <summary>
    /// Starts timer, received from network.
    /// </summary>
    public void OnNetworkStartTimer()
    {
        OnTimerStarted();
    }

    /// <summary>
    /// Starts game, received from network.
    /// </summary>
    public void OnNetworkStartGame()
    {
        OnStartGame();

        networkLayer.AllocateViewIDAndCallInstantiate(myCharacter, myTeam);
    }

    /// <summary>
    /// Resets timer.
    /// </summary>
    public void ResetTimer()
    {
        networkLayer.StopTimer();
    }

    /// <summary>
    /// Resets timer, received from network.
    /// </summary>
    public void OnNetworkStopTimer()
    {
        OnTimerStop();
    }
}