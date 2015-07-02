using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// Audio Clip, for the bullet shoot sound.
    /// </summary>
    [SerializeField]
    AudioClip shootSound;

    /// <summary>
    /// Reference to particle when shoots.
    /// </summary>
    [SerializeField]
    GameObject shootParticle;

    /// <summary>
    /// Bullet actual speed.
    /// </summary>
    [SerializeField]
    float speed;

    /// <summary>
    /// Network layer reference.
    /// </summary>
    //BulletNetworkLayer networkLayer;

    /// <summary>
    /// Shooter manager reference.
    /// </summary>
    ShooterManager shooter;
    /// <summary>
    /// Player manager reference.
    /// </summary>
    PlayerManager playerManager;

    /// <summary>
    /// Bullet attack points
    /// </summary>
    float bulletStrength;

    /// <summary>
    /// Sets all necessary references.
    /// </summary>
    /// <param name="shooter"></param>
    public void Setup(ShooterManager shooter)
    {
        //networkLayer = this.GetComponent<BulletNetworkLayer>();
        this.shooter = shooter;
        playerManager = shooter.GetComponent<PlayerManager>();
        //this.collider.enabled = shooter.GetComponent<PhotonView>().isMine;
    }

    /// <summary>
    /// Activates and shoots bullet.
    /// </summary>
    /// <param name="position">Starting position.</param>
    /// <param name="direction">Starting rotation.</param>
    /// <param name="strength">Bullet strength</param>
    public void ShootMe(Vector3 position, Vector3 direction, float strength)
    {
        this.bulletStrength = strength;
        this.transform.forward = direction;
        this.transform.position = position;

        shootParticle.SetActive(true);

        this.GetComponent<Rigidbody>().velocity = this.transform.forward * speed;

        if (AudioManager.instance.sfxEnabled)
        {
            GetComponent<AudioSource>().clip = shootSound;
            GetComponent<AudioSource>().Play();
        }
    }

    /// <summary>
    /// Destroys bullet.
    /// </summary>
    void DestroyMe()
    {
        shootParticle.SetActive(false);
        shooter.DestroyBullet(this);
    }

    /// <summary>
    /// Receives data from network.
    /// </summary>
    /// <param name="myPosition">New position.</param>
    /// <param name="myRotation">New rotation.</param>
    public void OnNetworkUpdate(Vector3 myPosition,Quaternion myRotation)
    {
        this.transform.position = myPosition;
        this.transform.rotation = myRotation;

        this.GetComponent<Rigidbody>().velocity = this.transform.forward * speed;
    }

    /// <summary>
    /// Handles bullet collision.
    /// </summary>
    /// <param name="collider">Object collided.</param>
    void OnTriggerEnter(Collider collider)
    {
		if (playerManager == null)
			return;

        if (collider.name == playerManager.name || collider.tag == "Bullet") return;

        bool willCollide = shooter.GetComponent<PhotonView>().isMine;

        switch (collider.tag)
        {
            case "Bullet":
                break;
            case "Player":
                if (!willCollide) break;

                PlayerManager playerShot = collider.GetComponent<PlayerManager>();
                if (playerShot.myTeam == playerManager.myTeam) break;

                bool killed = playerShot.TakeBullet(bulletStrength);
                if (killed)
                {
                    playerManager.AddKill();
                    GameController.instance.CheckEndGame();
                }
                break;
            case "Tower":
                if (!willCollide) break;

                if (playerManager.myTeam != collider.GetComponent<TowerManager>().myTeam && !collider.GetComponent<TowerManager>().shieldActive)
                {
                    collider.GetComponent<TowerManager>().TakeHit(bulletStrength);
                    playerManager.AddExp(5);
                }
                break;
        }

        this.GetComponent<Rigidbody>().velocity = Vector3.zero;

        Invoke("DestroyMe", 0.2f);
    }
}