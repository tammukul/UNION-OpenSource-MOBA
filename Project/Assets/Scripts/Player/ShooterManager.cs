using UnityEngine;
using System.Collections;

public class ShooterManager : MonoBehaviour
{
    /// <summary>
    /// Point where the bullet will begin travelling.
    /// </summary>
    public Transform shootPoint;

    /// <summary>
    /// Prefab of the bullet.
    /// </summary>
    public GameObject[] bulletPrefabs;
    /// <summary>
    /// Size of bullet buffer to be created.
    /// </summary>
    public int bufferSize;

    /// <summary>
    /// Array of bullets unused in buffer.
    /// </summary>
    Bullet[] unusedBullets;
    /// <summary>
    /// Array of bullets active in buffer.
    /// </summary>
    Bullet[] usedBullets;

    /// <summary>
    /// Counter since last bullet.
    /// </summary>
    float timeSinceLastBullet;

    /// <summary>
    /// PlayerManager reference.
    /// </summary>
    PlayerManager playerManager;
    /// <summary>
    /// PlayerProperties reference.
    /// </summary>
    PlayerProperties myProperties;

    /// <summary>
    /// Array containing all the guns the player owns
    /// </summary>
    GunTypes[] myGuns;
    /// <summary>
    /// Index from myGuns array, defining the active gun.
    /// </summary>
   // int activeGunIndex;

    /// <summary>
    /// Initializes shooter system.
    /// </summary>
    public void Start()
    {
        myGuns = new GunTypes[] { GunTypes.Default, GunTypes.None };
       // activeGunIndex = 0;

        timeSinceLastBullet = Time.time;

        playerManager = GetComponent<PlayerManager>();
        myProperties = GetComponent<PlayerProperties>();
    }

    /// <summary>
    /// Creates bullet buffer.
    /// </summary>
    public void CreateBuffer(TeamTypes myTeam)
    {
        unusedBullets = new Bullet[bufferSize];
        usedBullets = new Bullet[bufferSize];


        int playerPhotonID = this.GetComponent<PhotonView>().viewID;
        GameObject _obj;
        for (int i = 0; i < unusedBullets.Length; i++)
        {
            _obj = (GameObject)GameObject.Instantiate(bulletPrefabs[myTeam.GetHashCode()]);
            _obj.name = "Player" + playerPhotonID + "Bullet" + i;
            unusedBullets[i] = _obj.GetComponent<Bullet>();
            unusedBullets[i].Setup(this);
            _obj.SetActive(false);
        }
    }

    /// <summary>
    /// Shoots a new bullet.
    /// </summary>
    public void CreateBullet()
    {
        for (int i = 0; i < unusedBullets.Length; i++)
        {
            if (unusedBullets[i] != null)
            {
                usedBullets[i] = unusedBullets[i];
                unusedBullets[i] = null;

                usedBullets[i].gameObject.SetActive(true);
                
                Vector3 forward = shootPoint.forward;
                forward.y = 0;

                usedBullets[i].ShootMe(shootPoint.position, forward, myProperties.actualAtk);

                return;
            }
        }
    }

    /// <summary>
    /// Recicles a bullet in array.
    /// </summary>
    /// <param name="bullet">Bullet to be recicled.</param>
    public void DestroyBullet(Bullet bullet)
    {
        for (int i = 0; i < usedBullets.Length; i++)
        {
            if (usedBullets[i] != null)
            {
                if (usedBullets[i].name == bullet.name)
                {
                    unusedBullets[i] = usedBullets[i];
                    usedBullets[i] = null;

                    unusedBullets[i].gameObject.SetActive(false);
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Updates time since last bullet, if bigger than fire rate, shoots a new bullet.
    /// </summary>
    public void UpdateShoot()
    {
        if (Time.time - timeSinceLastBullet > myProperties.actualFireRate)
        {
            playerManager.ShootBullet();
            timeSinceLastBullet = Time.time;
        }
    }

    /// <summary>
    /// Drops the alternative gun, when death occurs.
    /// </summary>
    public void DropAlternativeGun()
    {
        for (int i = 1; i < myGuns.Length; i++)
        {
            GunDropsManager.instance.CreateNewDrop(myGuns[i], this.transform.position);
            myGuns[i] = GunTypes.None;
        }
    }

    /// <summary>
    /// Set a new alternative gun for the player, if available.
    /// </summary>
    /// <param name="newGun">New gun type.</param>
    public void SetAlternativeGun(GunTypes newGun)
    {
        for (int i = 1; i < myGuns.Length; i++)
        {
            if (myGuns[i] == GunTypes.None)
            {
                myGuns[i] = newGun;
                return;
            }
        }
    }
}