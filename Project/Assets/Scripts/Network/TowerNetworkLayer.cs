using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PhotonView))]
public class TowerNetworkLayer : MonoBehaviour
{
    /// <summary>
    /// Boolean telling if the tower is controlled locally.
    /// </summary>
    public bool isMine { get; private set; }

    /// <summary>
    /// Tower reference.
    /// </summary>
    TowerManager myTower;

    /// <summary>
    /// PhotonView reference.
    /// </summary>
    PhotonView photonView;

    /// <summary>
    /// Gets all necessary references.
    /// </summary>
    void Start()
    {
        photonView = this.GetComponent<PhotonView>();
        myTower = this.GetComponent<TowerManager>();

        isMine = photonView.isMine;
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
    /// Event to be called when the tower's shield goes down/up. Sends the data to the network.
    /// </summary>
    /// <param name="shieldDown">Is the shield down?</param>
    public void OnUpdateShield(bool shieldDown)
    {
        photonView.RPC("OnNetworkUpdateShield", PhotonTargets.All, shieldDown);
    }

    /// <summary>
    /// Event to be called when the tower takes a bullet.
    /// </summary>
    /// <param name="bulletStrength">Bullet's strength</param>
    public void OnTakeBullet(float bulletStrength)
    {
        photonView.RPC("OnNetworkTakeBullet", PhotonTargets.All, bulletStrength);
    }

    /// <summary>
    /// Event to be called when the tower shoots. Sends the data to the network.
    /// </summary>
    public void OnTowerShoot()
    {
        photonView.RPC("OnNetworkTowerShoot", PhotonTargets.All);
    }

    /// <summary>
    /// Receives the network data from the OnUpdateShield event.
    /// </summary>
    /// <param name="shieldDown">Is shield down?</param>
    [RPC]
    void OnNetworkUpdateShield(bool shieldDown)
    {

    }

    /// <summary>
    /// Receives the network data from the OnTakeBullet event.
    /// </summary>
    /// <param name="bulletStrength">Bullet's strength.</param>
    [RPC]
    void OnNetworkTakeBullet(float bulletStrength)
    {
        myTower.OnNetworkTakeHit(bulletStrength);
    }

    /// <summary>
    /// Receives the network data from the OnTowerShoot event.
    /// </summary>
    [RPC]
    void OnNetworkTowerShoot()
    {
        myTower.OnNetworkShoot();
    }
}