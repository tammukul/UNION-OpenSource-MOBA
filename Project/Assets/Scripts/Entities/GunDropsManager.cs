using UnityEngine;
using System.Collections;

public class GunDropsManager : MonoBehaviour
{
    /// <summary>
    /// Static instance.
    /// </summary>
    public static GunDropsManager instance;

    /// <summary>
    /// Network layer reference.
    /// </summary>
    GunDropsManagerNetwork networkLayer;

    /// <summary>
    /// Items buffer reference.
    /// </summary>
    GunItem[] buffer;

    /// <summary>
    /// Initialization method. Initializes static instance.
    /// </summary>
    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Start method. Gets buffer reference, and network layer reference.
    /// </summary>
    void Start()
    {
        buffer = this.GetComponentsInChildren<GunItem>(true);

        networkLayer = this.GetComponent<GunDropsManagerNetwork>();
    }

    /// <summary>
    /// Creates new drop from buffer.
    /// </summary>
    /// <param name="type">Gun type to create.</param>
    /// <param name="position">Position to insert drop.</param>
    public void CreateNewDrop(GunTypes type,Vector3 position)
    {
        if (type == GunTypes.None) return;
        networkLayer.CreateNewDrop(type, position);
    }

    /// <summary>
    /// Receives data from network, to create new gun drop.
    /// </summary>
    /// <param name="type">Gun type to create.</param>
    /// <param name="position">Position to insert drop.</param>
    public void OnNetworkCreateNewDrop(GunTypes type, Vector3 position)
    {
        for (int i = 0; i < buffer.Length; i++)
        {
            if (!buffer[i].gameObject.activeSelf)
            {
                buffer[i].gameObject.SetActive(true);
                buffer[i].Initialize(type,position);
                return;
            }
        }
    }

    /// <summary>
    /// Picks up drop, sends data to network.
    /// </summary>
    /// <param name="item">Item received.</param>
    /// <param name="player">Player who caught it.</param>
    public void PickUp(GunItem item, PlayerManager player)
    {
        networkLayer.CatchItem(item, player);
    }

    /// <summary>
    /// Receives network data, gives gun to the player.
    /// </summary>
    /// <param name="item">Item received.</param>
    /// <param name="player">Player who caught it.</param>
    public void OnNetworkPickUp(string itemName, string playerName)
    {
        PlayerManager player = GameController.instance.GetPlayer(playerName);

        for (int i = 0; i < buffer.Length; i++)
        {
            if (buffer[i].name == itemName)
            {
                player.UpdateAlternativeWeapon(buffer[i].myType);
                buffer[i].ResetMe();
            }
        }
    }
}