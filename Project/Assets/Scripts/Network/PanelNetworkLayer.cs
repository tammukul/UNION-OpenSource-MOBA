using UnityEngine;
using System.Collections;

public class PanelNetworkLayer : MonoBehaviour
{
    /// <summary>
    /// Panel reference.
    /// </summary>
    Panel myPanel;

    /// <summary>
    /// PhotonView reference.
    /// </summary>
    PhotonView photonView;

    string actualPlayerName;

    /// <summary>
    /// Gets all references.
    /// </summary>
    void Start()
    {
        myPanel = this.GetComponent<Panel>();
        photonView = this.GetComponent<PhotonView>();
    }

    /// <summary>
    /// Serialization method. Used to sync properties every frame.
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="info"></param>
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }

    /// <summary>
    /// Event to be called when the panel's owner changes. Sends the data to the network.
    /// Was having trouble with sent messages on this one. Sending a lot of times, to ensure.
    /// </summary>
    public void OnPlayerChanged(string player)
    {
        for (int i = 0; i < 3; i++)
        {
            photonView.RPC("OnNetworkPlayerChanged", PhotonTargets.AllBuffered, player);
        }
    }

    /// <summary>
    /// Receives the network data from the OnPlayerChanged event.
    /// </summary>
    [RPC]
    void OnNetworkPlayerChanged(string player)
    {
        myPanel.OnNetworkPlayerChanged(player);
    }
}