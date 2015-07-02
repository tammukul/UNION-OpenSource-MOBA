using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PhotonView))]
public class GunDropsManagerNetwork : MonoBehaviour
{
    /// <summary>
    /// Photon view reference.
    /// </summary>
    PhotonView photonView;
    /// <summary>
    /// Manager reference.
    /// </summary>
    GunDropsManager myDropsManager;

    /// <summary>
    /// Initialization method. Gets all necessary references.
    /// </summary>
    void Start()
    {
        photonView = this.GetComponent<PhotonView>();
        myDropsManager = this.GetComponent<GunDropsManager>();
    }

    /// <summary>
    /// Sends data to network, to create a new drop.
    /// </summary>
    /// <param name="type">Drop type.</param>
    /// <param name="position">Position to create.</param>
    public void CreateNewDrop(GunTypes type,Vector3 position)
    {
        photonView.RPC("OnNetworkCreateNewDrop", PhotonTargets.All, type.GetHashCode(), position);
    }

    /// <summary>
    /// Receives data from network, sends to drop manager to create a new drop on level.
    /// </summary>
    /// <param name="type">Drop type.</param>
    /// <param name="position">Position to create.</param>
    [RPC]
    void OnNetworkCreateNewDrop(int type, Vector3 position)
    {
        myDropsManager.OnNetworkCreateNewDrop((GunTypes)type, position);
    }

    /// <summary>
    /// Sends data to network, to catch a scpecific item.
    /// </summary>
    /// <param name="item">Item caught.</param>
    /// <param name="player">Player who caught it.</param>
    public void CatchItem(GunItem item, PlayerManager player)
    {
        photonView.RPC("OnNetworkCatchItem", PhotonTargets.All, item.name, player.name);
    }

    /// <summary>
    /// Receives data from network, to locally catch a specific item.
    /// </summary>
    /// <param name="itemName">Item caught name.</param>
    /// <param name="playerName">Player who caught it name.</param>
    [RPC]
    void OnNetworkCatchItem(string itemName, string playerName)
    {
        myDropsManager.OnNetworkPickUp(itemName, playerName);
    }

    /// <summary>
    /// Serialization method. Not used for this object.
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="info"></param>
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}