using UnityEngine;
using System.Collections;

public class GeneralNetworkLayer : MonoBehaviour
{
    /// <summary>
    /// Photon view reference.
    /// </summary>
    PhotonView photonView;

    /// <summary>
    /// Gets photon view reference.
    /// </summary>
    void Start()
    {
        photonView = this.GetComponent<PhotonView>();
    }

    /// <summary>
    /// Event to be called every X seconds, to send all the game's data again.
    /// </summary>
    public void ResyncGame()
    {
        photonView.RPC("NetworkResyncGame", PhotonTargets.All);
    }

    /// <summary>
    /// Event to be called when some new player joins the game. Sends the data to the network.
    /// </summary>
    public void OnPlayerJoined()
    {
        photonView.RPC("OnNetworkPlayerJoined", PhotonTargets.All);
    }

    /// <summary>
    /// Event to be called when some player left the game. Sends the data to the network.
    /// </summary>
    public void OnPlayerLeft()
    {
        photonView.RPC("OnNetworkPlayerLeft", PhotonTargets.All);
    }

    /// <summary>
    /// Event to be called when the game ends. Sends the data to the network.
    /// </summary>
    public void EndGame()
    {
        photonView.RPC("NetworkEndGame", PhotonTargets.All);
    }

    /// <summary>
    /// Receives the network data from the ResyncGame event.
    /// </summary>
    [RPC]
    public void NetworkResyncGame()
    {

    }

    /// <summary>
    /// Receives the network data from the OnPlayerJoined event.
    /// </summary>
    [RPC]
    public void OnNetworkPlayerJoined()
    {

    }

    /// <summary>
    /// Receives the network data from the OnPlayerLeft event.
    /// </summary>
    [RPC]
    public void OnNetworkPlayerLeft()
    {

    }

    /// <summary>
    /// Receives the network data from the EndGame event.
    /// </summary>
    [RPC]
    public void NetworkEndGame()
    {

    }
}