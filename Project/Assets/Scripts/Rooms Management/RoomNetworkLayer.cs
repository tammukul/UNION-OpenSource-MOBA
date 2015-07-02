using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class RoomNetworkLayer : MonoBehaviour
{
    /// <summary>
    /// Characters' prefabs reference.
    /// </summary>
    [SerializeField]
    GameObject[] charactersPrefabs;

    /// <summary>
    /// Bots'prefabs reference.
    /// </summary>
    [SerializeField]
    GameObject[] botsPrefabs;

    /// <summary>
    /// PhotonView reference.
    /// </summary>
    PhotonView view;

    /// <summary>
    /// Boolean containing if the player has already entered the game.
    /// </summary>
    bool enteredGame;

    public MultiplayerRoomsManager roomsManager { get; set; }

    /// <summary>
    /// Initializes references.
    /// </summary>
    void Start()
    {
        view = this.GetComponent<PhotonView>();
    }

    /// <summary>
    /// Calls the event to update players' properties locally.
    /// </summary>
    public void OnPropertiesChanged()
    {
        view.RPC("OnNetworkPropertiesChanged", PhotonTargets.All);
    }

    /// <summary>
    /// Updates players' properties locally.
    /// </summary>
    [RPC]
    void OnNetworkPropertiesChanged()
    {
        roomsManager.OnNetworkRoomChanged();
    }

    /// <summary>
    /// Calls the event to start timer locally.
    /// </summary>
    public void StartTimer()
    {
        view.RPC("OnNetworkStartTimer", PhotonTargets.All);
    }

    /// <summary>
    /// Starts timer locally.
    /// </summary>
    [RPC]
    void OnNetworkStartTimer()
    {
        roomsManager.OnNetworkStartTimer();
    }

    /// <summary>
    /// Allocates a new ViewID from Photon, and sends message to instantiate a new character.
    /// </summary>
    /// <param name="myCharacter">Character prefab to use.</param>
    public void AllocateViewIDAndCallInstantiate(CharacterTypes myCharacter,TeamTypes team)
    {
        if (!enteredGame)
        {
            enteredGame = true;

            int myID = PhotonNetwork.AllocateViewID();

            view.RPC("InstantiateObject", PhotonTargets.AllBuffered, myID, myCharacter.GetHashCode(), team.GetHashCode(),PlayFabManager.instance.GetMyPlayerID(),PlayFabManager.instance.playerDisplayName);
        }
    }

    /// <summary>
    /// Instantiates a new character.
    /// </summary>
    /// <param name="viewID">Photon view ID.</param>
    /// <param name="playerType">Character prefab to use.</param>
    [RPC]
    void InstantiateObject(int viewID, int playerType, int team, string playerPlayFabID, string playFabName)
    {
        GameObject obj = Instantiate(charactersPrefabs[playerType], Vector3.zero, Quaternion.identity) as GameObject;

        PhotonView objView = obj.GetComponent<PhotonView>();
        objView.viewID = viewID;
        
        obj.name = playFabName;

        PlayerNetworkLayer playerNetwork = obj.GetComponent<PlayerNetworkLayer>();
        playerNetwork.Initialize();

        PlayerManager playerManager = obj.GetComponent<PlayerManager>();
        playerManager.StartPlayer(true, (TeamTypes)team, playerPlayFabID,playFabName);

        MovementManager movementManager = obj.GetComponent<MovementManager>();
        
        movementManager.StartMovementManager(objView.isMine);

        GameController.instance.AddPlayer(playerManager);


        SkinnedMeshRenderer[] renderers = playerManager.GetComponentsInChildren<SkinnedMeshRenderer>();
        for (int j = 0; j < renderers.Length; j++)
        {
            ProceduralMaterial material = renderers[j].material as ProceduralMaterial;
            material.SetProceduralBoolean("Team", (TeamTypes)team == TeamTypes.Blue);
            material.RebuildTextures();
        }

        AudioManager.instance.PlayGameplayMusic();
    }

    /// <summary>
    /// Calls network to activate the game level GameObject.
    /// </summary>
    public void ActivateLevel()
    {
        view.RPC("OnNetworkActivateLevel", PhotonTargets.All);
    }

    /// <summary>
    /// Activates the game level GameObject.
    /// </summary>
    [RPC]
    void OnNetworkActivateLevel()
    {
        PhotonNetwork.room.open = false;

        roomsManager.OnNetworkStartGame();
    }

    /// <summary>
    /// Allocates some view IDs, and tells network to instantiate bots for all available spaces.
    /// </summary>
    /// <param name="teamsCount"></param>
    public void InstantiateBots(int[] teamsCount)
    {
        for (int i = 0; i < teamsCount.Length; i++)
        {
            for (int j = 0; j < teamsCount[i]; j++)
            {
                int viewID = PhotonNetwork.AllocateViewID();
                int playerType = Random.Range(0, CharacterTypes.He.GetHashCode()+1);
                view.RPC("OnNetworkInstantiateBot",PhotonTargets.All, viewID, playerType, i);
                Debug.Log("instantiate bot of team " + (TeamTypes)i);
            }
        }
    }

    /// <summary>
    /// Instantiate a new bot locally.
    /// </summary>
    /// <param name="viewID">View ID received from Photon.</param>
    /// <param name="playerType">Player avatar HashCode.</param>
    /// <param name="team">Player team HashCode.</param>
    [RPC]
    void OnNetworkInstantiateBot(int viewID, int playerType, int team)
    {
        GameObject obj = Instantiate(botsPrefabs[playerType], Vector3.zero, Quaternion.identity) as GameObject;
        obj.name = "Bot" + viewID;

        PhotonView objView = obj.GetComponent<PhotonView>();
        objView.viewID = viewID;

        PlayerNetworkLayer playerNetwork = obj.GetComponent<PlayerNetworkLayer>();
        playerNetwork.Initialize();

        PlayerManager playerManager = obj.GetComponent<PlayerManager>();
        playerManager.StartPlayer(false, (TeamTypes)team,"","Bot"+viewID);

        MovementManager movementManager = obj.GetComponent<MovementManager>();
        movementManager.StartMovementManager(false);

        object owner;
        if (PhotonNetwork.player.customProperties.TryGetValue("Owner", out owner))
        {
            string ownerName = owner.ToString();

            if (ownerName == PhotonNetwork.player.name)
            {
                PlayerAIManager aiManager = obj.GetComponent<PlayerAIManager>();
                aiManager.StartAI();
            }
        }

        GameController.instance.AddPlayer(playerManager);

        SkinnedMeshRenderer[] renderers = playerManager.GetComponentsInChildren<SkinnedMeshRenderer>();
        for (int j = 0; j < renderers.Length; j++)
        {
            ProceduralMaterial material = renderers[j].material as ProceduralMaterial;
            material.SetProceduralBoolean("Team", (TeamTypes)team == TeamTypes.Blue);
            material.RebuildTextures();
        }
    }

    /// <summary>
    /// Network serialization method.
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="info"></param>
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }

    /// <summary>
    /// Calls network to stop timer locally.
    /// </summary>
    public void StopTimer()
    {
        view.RPC("OnNetworkStopTimer", PhotonTargets.All);
    }

    /// <summary>
    /// Stops timer locally.
    /// </summary>
    [RPC]
    void OnNetworkStopTimer()
    {
        roomsManager.OnNetworkStopTimer();
    }
}