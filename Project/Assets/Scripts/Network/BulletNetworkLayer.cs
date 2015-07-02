using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PhotonView))]
public class BulletNetworkLayer : MonoBehaviour
{
    /// <summary>
    /// Bullet reference.
    /// </summary>
    Bullet myBullet;

    /// <summary>
    /// PhotonView reference.
    /// </summary>
    PhotonView photonView;

    /// <summary>
    /// Boolean telling if I'm controlling locally this bullet.
    /// </summary>
    public bool isMine { get; private set; }

    /// <summary>
    /// Gets all necessary references.
    /// </summary>
    void Start()
    {
        photonView = this.GetComponent<PhotonView>();
        myBullet = this.GetComponent<Bullet>();

        isMine = photonView.isMine;
    }

    /// <summary>
    /// Serialization method. Used to sync properties every frame.
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="info"></param>
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (myBullet == null) return;

        Vector3 positionAndRotation = Vector3.zero;

        if (stream.isWriting)
        {
            positionAndRotation.x = myBullet.transform.position.x;
            positionAndRotation.y = myBullet.transform.eulerAngles.y;
            positionAndRotation.z = myBullet.transform.position.z;

            stream.Serialize(ref positionAndRotation);
        }
        else
        {
            stream.Serialize(ref positionAndRotation);

            Vector3 receivedPosition = new Vector3(positionAndRotation.x, this.transform.position.y, positionAndRotation.z);
            Quaternion receivedRotation = Quaternion.Euler(this.transform.eulerAngles.x, positionAndRotation.y, this.transform.eulerAngles.z);

            myBullet.OnNetworkUpdate(receivedPosition, receivedRotation);
        }
    }
}