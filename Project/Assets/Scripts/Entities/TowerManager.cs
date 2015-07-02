using UnityEngine;
using System.Collections;

public class TowerManager : MonoBehaviour
{
    /// <summary>
    /// Audio Clip to be played when the tower takes damage.
    /// </summary>
    [SerializeField]
    AudioClip damageClip;

    /// <summary>
    /// Audio Clip to be played when the tower dies.
    /// </summary>
    [SerializeField]
    AudioClip deathClip;

    /// <summary>
    /// Audio Clip to be played when the tower shoots.
    /// </summary>
    [SerializeField]
    AudioClip shootClip;

    /// <summary>
    /// Tower graphic reference.
    /// </summary>
    [SerializeField]
    GameObject towerGraphic;

    /// <summary>
    /// Tower's team.
    /// </summary>
    [SerializeField]
    TeamTypes _myTeam;

    /// <summary>
    /// Tower's team as a property.
    /// </summary>
    public TeamTypes myTeam
    {
        get { return _myTeam; }
        private set { _myTeam = value; }
    }

    /// <summary>
    /// Tower's attack range.
    /// </summary>
    [SerializeField]
    float towerRange;

    /// <summary>
    /// Delay between shots.
    /// </summary>
    [SerializeField]
    float shootDelay;

    /// <summary>
    /// Tower shot strength.
    /// </summary>
    [SerializeField]
    float shotStrength;

    /// <summary>
    /// Tower's actual state.
    /// </summary>
    public TowerStates actualState { get; private set; }

    /// <summary>
    /// Network layer reference.
    /// </summary>
    TowerNetworkLayer networkLayer;

    /// <summary>
    /// Enemie's reference.
    /// </summary>
    PlayerManager[] enemies;

    /// <summary>
    /// Shield actual state.
    /// </summary>
    public bool shieldActive { get; private set; }

    /// <summary>
    /// Tower's total life points.
    /// </summary>
    float totalLife = 200;
    /// <summary>
    /// Tower's actual life points.
    /// </summary>
    float actualLife;

    /// <summary>
    /// Counter to define when the tower should shoot.
    /// </summary>
    float shooterCounter;

    /// <summary>
    /// Reference to the shot.
    /// </summary>
    TowerShot myshot;

    /// <summary>
    /// Gets all references.
    /// </summary>
    void Start()
    {
        if (enemies == null)
        {
            enemies = new PlayerManager[0];
        }

        networkLayer = GetComponent<TowerNetworkLayer>();

        myshot = this.GetComponentInChildren<TowerShot>();
        myshot.gameObject.SetActive(false);

        actualLife = totalLife;

        this.ActivateShield();
    }

    /// <summary>
    /// Adds player to array.
    /// </summary>
    /// <param name="player">Player to add.</param>
    public void AddPlayer(PlayerManager player)
    {
        if (player.myTeam != _myTeam)
        {
            if (enemies == null) enemies = new PlayerManager[0];

            System.Array.Resize<PlayerManager>(ref enemies, enemies.Length + 1);
            enemies[enemies.Length - 1] = player;
        }
    }

    /// <summary>
    /// Update method. Updates state machine.
    /// </summary>
    void Update()
    {
        this.UpdateStateMachine();
    }

    /// <summary>
    /// Updates tower state machine.
    /// </summary>
    void UpdateStateMachine()
    {
        switch (actualState)
        {
            case TowerStates.Idle:
                this.CheckEnemiesDistance();
                break;
            case TowerStates.Attacking:
                this.CheckEnemiesDistance();
                this.AttackUpdate();
                break;
            case TowerStates.Dead:
                break;
        }
    }

    /// <summary>
    /// Changes state machine to a new state.
    /// </summary>
    /// <param name="newState">New state.</param>
    public void ChangeState(TowerStates newState)
    {
        if (actualState == TowerStates.Dead || newState == actualState) return;

        if (actualState != newState)
        {
            actualState = newState;

            switch (actualState)
            {
                case TowerStates.Idle:
                    break;
                case TowerStates.Attacking:
                    shooterCounter = shootDelay;
                    break;
                case TowerStates.Dead:
                    break;
            }

			GameController.instance.CheckEndGame();
        }
    }

    /// <summary>
    /// Checks if there is any enemy in range, to attack.
    /// </summary>
    void CheckEnemiesDistance()
    {
        bool hasEnemyInRange = false;
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null) continue;

            if (Vector3.Distance(this.transform.position, enemies[i].transform.position) < towerRange)
            {
                hasEnemyInRange = true;
                break;
            }
        }

        if (hasEnemyInRange)
        {
            this.ChangeState(TowerStates.Attacking);
        }
        else
        {
            this.ChangeState(TowerStates.Idle);
        }
    }

    /// <summary>
    /// Attack update method.
    /// </summary>
    void AttackUpdate()
    {
        shooterCounter -= Time.deltaTime;
        if (shooterCounter <= 0)
        {
            shooterCounter = 100;
            this.Shoot();
        }
    }

    /// <summary>
    /// If owner, calls for network shoot.
    /// </summary>
    void Shoot()
    {
        if (networkLayer.isMine)
        {
            networkLayer.OnTowerShoot();
        }
    }

    /// <summary>
    /// Receives shoot call from network layer, actually shoots something.
    /// </summary>
    public void OnNetworkShoot()
    {
        if (AudioManager.instance.sfxEnabled)
        {
            GetComponent<AudioSource>().clip = shootClip;
            GetComponent<AudioSource>().Play();
        }

        shooterCounter = shootDelay;

        myshot.gameObject.SetActive(true);
        myshot.Shoot(networkLayer.isMine, shotStrength,myTeam);
    }

    /// <summary>
    /// Deactivates tower shield.
    /// </summary>
    public void DeactivateShield()
    {
        shieldActive = false;
    }

    /// <summary>
    /// Activates tower shield.
    /// </summary>
    public void ActivateShield()
    {
        shieldActive = true;
    }

    /// <summary>
    /// Takes a hit from an enemy bullet.
    /// </summary>
    /// <param name="bulletStrength">Amount of HP to take.</param>
    public void TakeHit(float bulletStrength)
    {
        if (actualState != TowerStates.Dead)
        {
            networkLayer.OnTakeBullet(bulletStrength);
        }
    }

    /// <summary>
    /// Receives hit from network.
    /// </summary>
    /// <param name="bulletStrength">Amount of HP to take.</param>
    public void OnNetworkTakeHit(float bulletStrength)
    {
        if (AudioManager.instance.sfxEnabled)
        {
            GetComponent<AudioSource>().clip = damageClip;
            GetComponent<AudioSource>().Play();
        }

        if (shieldActive) return;

        this.actualLife -= bulletStrength;

        GameplayUI.instance.UpdateTowerLife(myTeam, actualLife, totalLife);

        if (actualLife <= 0)
        {
            this.KillMe();
        }
    }

    /// <summary>
    /// Kills tower.
    /// </summary>
    void KillMe()
    {
        if (AudioManager.instance.sfxEnabled)
        {
            GetComponent<AudioSource>().clip = deathClip;
            GetComponent<AudioSource>().Play();
        }

        towerGraphic.SetActive(false);
        this.GetComponent<Collider>().enabled = false;

        this.ChangeState(TowerStates.Dead);

        GameController.instance.CheckEndGame();
    }
}