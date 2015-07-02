using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MatchRoomScreen : MonoBehaviour
{

    /// <summary>
    /// GameObject contaning the Room Screen
    /// </summary>
    [SerializeField]
    GameObject roomScreen;


    /// <summary>
    /// GameObject contaning the Match Screen
    /// </summary>
    [SerializeField]
    GameObject matchScreen;

    /// <summary>
    /// GameObject array contaning the characthers avatars
    /// </summary>
    [SerializeField]
    GameObject[] charactersAvatars;


    /// <summary>
    /// Text of the game timer
    /// </summary>
    [SerializeField]
    Text gameTimerText;

    /// <summary>
    /// Text of the characters stats
    /// </summary>
    [SerializeField]
    Text[] characterStatsTexts;


    /// <summary>
    /// Portrait border of the blue team
    /// </summary>
    [SerializeField]
    Image[] blueTeamPortraitBorders;


    /// <summary>
    /// Portraits of the blue team
    /// </summary>
    [SerializeField]
    Image[] blueTeamPortraits;

    /// <summary>
    /// Text of the blue team name
    /// </summary>
    [SerializeField]
    Text[] blueTeamNames;


    /// <summary>
    /// Portrait border of the orange team
    /// </summary>
    [SerializeField]
    Image[] orangeTeamPortraitBorders;

    /// <summary>
    /// Portraits of the orange team
    /// </summary>
    [SerializeField]
    Image[] orangeTeamPortraits;

    /// <summary>
    /// Text of the orange team name
    /// </summary>
    [SerializeField]
    Text[] orangeTeamNames;

    /// <summary>
    /// She character portraits
    /// </summary>
    [SerializeField]
    Sprite[] shePortraits;


    /// <summary>
    /// He character portraits
    /// </summary>
    [SerializeField]
    Sprite[] hePortraits;


    /// <summary>
    /// Button to use She character
    /// </summary>
    [SerializeField]
    GameObject useSheButton;

    CharacterTypes selectedCharacter;

    TeamTypes selectedTeam;


    /// <summary>
    /// Holds the player count of the teams
    /// </summary>
    int[] teamsCount;

    /// <summary>
    /// Check if the player is ready
    /// </summary>
    bool ready;

    /// <summary>
    /// Total of ready players
    /// </summary>
    int playersReady;

    /// <summary>
    /// Check if the time is started
    /// </summary>
    bool timerStarted;

    /// <summary>
    /// Value of the time counter
    /// </summary>
    float timerCounter;

    void Start()
    {
        teamsCount = new int[] { 0, 0 };
        useSheButton.SetActive(AccountManager.instance.boughtShe);

        ChatManager.instance.StartConnection();
        
        MultiplayerRoomsManager.instance.SetPropertiesChangeCallback(OnRoomPropertiesChanged);
        MultiplayerRoomsManager.instance.SetOnTimerStarted(OnTimerStarted);
        MultiplayerRoomsManager.instance.SetOnStartGame(OnStartGame);
        MultiplayerRoomsManager.instance.SetOnTimerStop(OnTimerStop);
        MultiplayerRoomsManager.instance.InitializeNetworkLayer();

        this.UpdateTeamPortraits();

        MultiplayerRoomsManager.instance.CallNetworkSync();
    }

    void Update()
    {
        if (timerStarted && timerCounter > 0)
        {
            timerCounter -= Time.deltaTime;

            if (timerCounter <= 0)
            {
                timerCounter = 0;
                MultiplayerRoomsManager.instance.StartGame();
            }

            string timerText = "0:" + (int)timerCounter;

            if (gameTimerText.text != timerText)
            {
                gameTimerText.text = timerText;
            }
        }
    }

    public void OnExitClick()
    {
        MultiplayerRoomsManager.instance.Dispose();
        ChatManager.instance.LeaveChat();

        SceneLoader.sceneToLoad = Scenes.MainMenu;
        Application.LoadLevel(Scenes.Loading.ToString());
    }

    public void OnConfirmClick()
    {
        ready = true;

        MultiplayerRoomsManager.instance.SetReady();
    }

    public void OnCharacterButtonClick(string characterName)
    {
        this.SelectChar(characterName, true);

        if (AudioManager.instance.sfxEnabled) this.GetComponent<AudioSource>().Play();
    }

    public void SelectChar(string characterName, bool syncNetwork)
    {
        if (ready) return;

        CharacterTypes newType = (CharacterTypes)System.Enum.Parse(typeof(CharacterTypes), characterName, true);

        if (newType != selectedCharacter)
        {
            charactersAvatars[selectedCharacter.GetHashCode()].SetActive(false);
            selectedCharacter = newType;
            charactersAvatars[selectedCharacter.GetHashCode()].SetActive(true);
            this.RebuildAvatarTextures();

            this.ChangeStatsValues();
        }

        MultiplayerRoomsManager.instance.ChangePlayerAvatar(selectedCharacter,syncNetwork);
    }

    public void ChangeTeam(bool syncNetwork)
    {
        if (ready) return;

        Debug.Log("change team from " + selectedTeam);
        int newID = selectedTeam.GetHashCode();
        newID++;
        if (newID > TeamTypes.Orange.GetHashCode())
        {
            newID = 0;
        }

        TeamTypes newTeam = (TeamTypes)newID;

        if (teamsCount[newTeam.GetHashCode()] == PhotonNetwork.room.maxPlayers / 2) return;

        if (newTeam != selectedTeam)
        {
            selectedTeam = newTeam;
            this.RebuildAvatarTextures();

            MultiplayerRoomsManager.instance.ChangeTeam(selectedTeam,syncNetwork);
        }
    }

    void RebuildAvatarTextures()
    {
        SkinnedMeshRenderer[] renderers = charactersAvatars[selectedCharacter.GetHashCode()].GetComponentsInChildren<SkinnedMeshRenderer>();
        for (int j = 0; j < renderers.Length; j++)
        {
            ProceduralMaterial material = renderers[j].sharedMaterial as ProceduralMaterial;
            material.SetProceduralBoolean("Team", selectedTeam == TeamTypes.Blue);
            material.RebuildTextures();
        }
    }

    void ChangeStatsValues()
    {
        PlayerProperties props = charactersAvatars[selectedCharacter.GetHashCode()].GetComponent<PlayerProperties>();

        characterStatsTexts[0].text = props.baseAtk.ToString();
        characterStatsTexts[1].text = props.baseDef.ToString();
        characterStatsTexts[2].text = props.baseFireRate.ToString();
        characterStatsTexts[3].text = props.baseHP.ToString();
        characterStatsTexts[4].text = props.baseMovSpd.ToString();
    }

    void UpdateTeamPortraits()
    {
        int maxPlayersInTeam = MultiplayerRoomsManager.instance.GetMaxPlayers() / 2;

        for (int i = 0; i < blueTeamPortraits.Length; i++)
        {
            if (i >= maxPlayersInTeam)
            {
                blueTeamPortraits[i].gameObject.SetActive(false);
                blueTeamPortraitBorders[i].gameObject.SetActive(false);
                //if (blueTeamNames != null) blueTeamNames[i].gameObject.SetActive(false);

                orangeTeamPortraits[i].gameObject.SetActive(false);
                orangeTeamPortraitBorders[i].gameObject.SetActive(false);
                //if (orangeTeamNames != null) orangeTeamNames[i].gameObject.SetActive(false);
            }
        }
    }

    void ClearPortraits()
    {
        teamsCount[TeamTypes.Blue.GetHashCode()] = 0;
        teamsCount[TeamTypes.Orange.GetHashCode()] = 0;

        for (int i = 0; i < blueTeamPortraits.Length; i++)
        {
            blueTeamPortraits[i].sprite = null;
            blueTeamPortraitBorders[i].color = Color.white;
            //if (blueTeamNames != null) blueTeamNames[i].text = "";

            orangeTeamPortraits[i].sprite = null;
            orangeTeamPortraitBorders[i].color = Color.white;
            //if (orangeTeamNames != null) orangeTeamNames[i].text = ""
        }
    }

    void OnRoomPropertiesChanged(RoomInfo roomInfo)
    {
        this.ClearPortraits();

        this.CheckStartTimer();

        PhotonPlayer[] players = PhotonNetwork.playerList;

        if (!timerStarted)
        {
            gameTimerText.text = players.Length + "/" + PhotonNetwork.room.maxPlayers;
        }

        int readyCount = 0;

        for (int i = 0; i < players.Length; i++)
        {
            string playerName = players[i].name;
            TeamTypes playerTeam = (TeamTypes)int.Parse(players[i].customProperties["Team"].ToString());
            CharacterTypes playerChar = (CharacterTypes)int.Parse(players[i].customProperties["Character"].ToString());
            bool playerReady = System.Convert.ToBoolean(players[i].customProperties["Ready"]);
            
            bool isMe = players[i].name == AccountManager.instance.displayName;

            if (playerReady) readyCount++;

            if (isMe) this.CheckMyPlayerProperties(playerName,playerTeam,playerChar);

            int myTeamCount = teamsCount[playerTeam.GetHashCode()];

            if (playerTeam == TeamTypes.Blue)
            {
                //blueTeamNames[myTeamCount].text = playerName;
                if (playerChar == CharacterTypes.He)
                    blueTeamPortraits[myTeamCount].sprite = hePortraits[playerTeam.GetHashCode()];
                else
                    blueTeamPortraits[myTeamCount].sprite = shePortraits[playerTeam.GetHashCode()];

                if (isMe) blueTeamPortraitBorders[myTeamCount].color = Color.yellow;
                else blueTeamPortraitBorders[myTeamCount].color = Color.white;
            }
            else
            {
                //orangeTeamNames[myTeamCount].text = playerName;
                if (playerChar == CharacterTypes.He)
                    orangeTeamPortraits[myTeamCount].sprite = hePortraits[playerTeam.GetHashCode()];
                else
                    orangeTeamPortraits[myTeamCount].sprite = shePortraits[playerTeam.GetHashCode()];

                if (isMe) orangeTeamPortraitBorders[myTeamCount].color = Color.yellow;
                else orangeTeamPortraitBorders[myTeamCount].color = Color.white;
            }

            teamsCount[playerTeam.GetHashCode()]++;
        }

        playersReady = readyCount;

        if (playersReady == PhotonNetwork.room.playerCount)
        {
            timerCounter = 0;
            MultiplayerRoomsManager.instance.StartGame();
        }
        
        if(timerStarted && players.Length < PhotonNetwork.room.playerCount)
        {
            MultiplayerRoomsManager.instance.ResetTimer();
        }
    }

    private void CheckStartTimer()
    {
        if (PhotonNetwork.room.maxPlayers == PhotonNetwork.room.playerCount && !timerStarted)
        {
            MultiplayerRoomsManager.instance.StartTimer();
        }
    }

    void CheckMyPlayerProperties(string playerName, TeamTypes playerTeam, CharacterTypes playerChar)
    {
        
        if (selectedTeam != playerTeam)
        {
            this.ChangeTeam(false);
        }

        if (selectedCharacter != playerChar)
        {
            this.SelectChar(playerChar.ToString(), false);
        }
    }

    public void OnTimerStarted()
    {
        timerStarted = true;
        timerCounter = 45;

        string timerText = "0:" + (int)timerCounter;
        gameTimerText.text = timerText;
    }

    public void OnStartGame()
    {
        ChatManager.instance.LeaveChat();
        matchScreen.SetActive(true);
        roomScreen.SetActive(false);

        GameController.instance.localPlayerTeam = selectedTeam;

        int[] botsCount = new int[teamsCount.Length];

        for (int i = 0; i < botsCount.Length; i++)
        {
            botsCount[i] = PhotonNetwork.room.maxPlayers / 2 - teamsCount[i];
        }

        MultiplayerRoomsManager.instance.InstantiateBots(botsCount);
    }

    public void OnTimerStop()
    {
        timerStarted = false;

        gameTimerText.text = PhotonNetwork.playerList.Length + "/" + PhotonNetwork.room.maxPlayers;
    }
}