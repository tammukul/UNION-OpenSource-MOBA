using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PhotonView))]
public class PlayerNetworkLayer : MonoBehaviour
{
    /// <summary>
    /// Player reference.
    /// </summary>
    PlayerManager myPlayer;

    /// <summary>
    /// PhotonView reference.
    /// </summary>
    PhotonView photonView;

    /// <summary>
    /// Boolean telling if the player is controleld locally.
    /// </summary>
    public bool isMine { get; private set; }

    /// <summary>
    /// Gets all necessary references.
    /// </summary>
    public void Initialize()
    {
        photonView = this.GetComponent<PhotonView>();
        myPlayer = this.GetComponent<PlayerManager>();

        isMine = photonView.isMine;
    }

    /// <summary>
    /// Serialization method. Used to sync properties every frame.
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="info"></param>
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (myPlayer == null) return;

        Vector3 positionAndRotation = Vector3.zero;
        int actualStateCode = 0;

        Vector2 velocity = Vector3.zero;

        if (stream.isWriting)
        {
            positionAndRotation.x = myPlayer.transform.position.x;
            positionAndRotation.y = myPlayer.childTransform.eulerAngles.y;
            positionAndRotation.z = myPlayer.transform.position.z;

            stream.Serialize(ref positionAndRotation);

            actualStateCode = myPlayer.actualState.GetHashCode();

            velocity = new Vector2(myPlayer.GetComponent<Rigidbody>().velocity.x, myPlayer.GetComponent<Rigidbody>().velocity.z);
            stream.Serialize(ref velocity);

            stream.Serialize(ref actualStateCode);
        }
        else
        {
            stream.Serialize(ref positionAndRotation);

            Vector3 receivedPosition = new Vector3(positionAndRotation.x,this.transform.position.y,positionAndRotation.z);
            Quaternion receivedRotation = Quaternion.Euler(this.transform.eulerAngles.x,positionAndRotation.y,this.transform.eulerAngles.z);

            myPlayer.transform.position = receivedPosition;
            myPlayer.childTransform.rotation = receivedRotation;

            stream.Serialize(ref velocity);

            myPlayer.OnNetworkUpdate(receivedPosition, receivedRotation,velocity);

            stream.Serialize(ref actualStateCode);

            myPlayer.ChangeState((PlayerStates)actualStateCode);

        }
    }

    /// <summary>
    /// Event to be called when the player's life changes. Sends the data to the network.
    /// </summary>
    /// <param name="newLife">New life amount.</param>
    public void OnLifeChanged(float newLife)
    {
        photonView.RPC("OnNetworkLifeChanged", PhotonTargets.AllBuffered, newLife);
    }

    /// <summary>
    /// Event to be called when the player's level changes. Sends the data to the network.
    /// </summary>
    /// <param name="newLevel">New level.</param>
    public void OnLevelChanged(int newLevel)
    {
        photonView.RPC("OnNetworkLevelChanged", PhotonTargets.AllBuffered, newLevel);
    }

    /// <summary>
    /// Event to be called when the player shoots. Sends the data to the network.
    /// </summary>
    public void OnShootBullet()
    {
        photonView.RPC("OnNetworkShootBullet", PhotonTargets.AllBuffered);
    }

    /// <summary>
    /// Evento to be called when the player must respawn
    /// </summary>
    public void RespawnMe(Vector3 respawnPosition)
    {
        photonView.RPC("OnNetworkRespawn", PhotonTargets.AllBuffered,respawnPosition);
    }

    /// <summary>
    /// Event to be called when the player chooses their bonus stat.
    /// </summary>
    /// <param name="stat"></param>
    public void GiveStatBonus(PlayerStats stat)
    {
        photonView.RPC("OnNetworkGiveStatBonus", PhotonTargets.AllBuffered, stat.GetHashCode());
    }

    /// <summary>
    /// Event to be called when the game ends.
    /// </summary>
    /// <param name="winner">Winner team.</param>
	public void OnGameEnded (TeamTypes winner)
	{
		photonView.RPC("OnNetworkGameEnded", PhotonTargets.AllBuffered, winner.GetHashCode());
	}

    /// <summary>
    /// Receives the network data from the OnLifeChanged event.
    /// </summary>
    /// <param name="newLife">New life amount.</param>
    [RPC]
    void OnNetworkLifeChanged(float newLife)
    {
        myPlayer.OnNetworkLifeUpdate(newLife);
    }

    /// <summary>
    /// Receives the network data from the OnLevelChanged event.
    /// </summary>
    /// <param name="newLevel">New level.</param>
    [RPC]
    void OnNetworkLevelChanged(int newLevel)
    {
        myPlayer.OnNetworkLevelUpdate(newLevel);
    }

    /// <summary>
    /// Receives the network data from the OnShootBullet event.
    /// </summary>
    [RPC]
    void OnNetworkShootBullet()
    {
        this.GetComponent<ShooterManager>().CreateBullet();
    }

    /// <summary>
    /// Receives the network data from the RespawnMe event.
    /// </summary>
    [RPC]
    void OnNetworkRespawn(Vector3 respawnPosition)
    {
        myPlayer.OnNetworkRespawn(respawnPosition);
    }

    /// <summary>
    /// Receives the network data from the GiveStatBonus event.
    /// </summary>
    /// <param name="statCode"></param>
    [RPC]
    void OnNetworkGiveStatBonus(int statCode)
    {
        myPlayer.GiveStatusBonus((PlayerStats)statCode);
    }

    /// <summary>
    /// Receives network data from the OnGameEnded event.
    /// </summary>
    /// <param name="winnerID">Winner team HashCode.</param>
    [RPC]
    void OnNetworkGameEnded(int winnerID)
    {
        myPlayer.OnNetworkGameEnded((TeamTypes)winnerID);
    }
}