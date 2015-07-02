using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Class that manages player behaviour. Comunicates with shooter, animation and movement managers, and sends/receives data from network.
/// </summary>
public class PlayerManager : MonoBehaviour
{
    /// <summary>
    /// World point reference to position the screen lifebar;
    /// </summary>
    public Transform lifebarWorldPoint;

    /// <summary>
    /// Player lifebar reference.
    /// </summary>
    public PlayerWorldLifebar myLifebar { get; set; }

    /// <summary>
    /// Reference to the sprite under the avatars' feet.
    /// </summary>
    [SerializeField]
    SpriteRenderer baseSprite;

    /// <summary>
    /// Reference to the colors the sprite must have, depending on the team he's on.
    /// </summary>
    [SerializeField]
    Color[] spriteTeamColors;

    /// <summary>
    /// Experience amount needed for each level.
    /// </summary>
    int[] experienceForEachLevel = new int[] { 1000, 2100, 3310, 4641, 3106, 7715, 9487, 11436, 13579, 15937 };

    /// <summary>
    /// Player actual state.
    /// </summary>
    public PlayerStates actualState { get; private set; }
    /// <summary>
    /// Boolean that shows if the player is trying to shoot. Separate from state machine because can happen while moving.
    /// </summary>
    bool shooting = false;

    /// <summary>
    /// Player team.
    /// </summary>
    [SerializeField]
    public TeamTypes myTeam { get; set; }

    /// <summary>
    /// Reference to the player's PlayFabID. Used to send push notification on end game.
    /// </summary>
    public string playerPlayFabID {get;private set;}

    /// <summary>
    /// Level up effects, for both teams.
    /// </summary>
    [SerializeField]
    GameObject[] levelUpEffects;

    /// <summary>
    /// Reference do COG pivot. Used to properly rotate character.
    /// </summary>
    public Transform childTransform;

    /// <summary>
    /// Reference to player specific properties.
    /// </summary>
    PlayerProperties myProperties;
    /// <summary>
    /// Reference to the network layer.
    /// </summary>
    PlayerNetworkLayer networkLayer;
    /// <summary>
    /// Reference to the animation manager.
    /// </summary>
    PlayerAnimationManager animationManager;
    /// <summary>
    /// Reference to the shooter manager.
    /// </summary>
    ShooterManager shooterManager;

    /// <summary>
    /// Reference to the AI manager, case the character is a bot.
    /// </summary>
    PlayerAIManager aiManager;

    /// <summary>
    /// Reference to the player's team tower.
    /// </summary>
    TowerManager myTower;

    /// <summary>
    /// Text reference to show player actual Level.
    /// </summary>
    //Text levelText;

    /// <summary>
    /// Text reference to show player actual experience points.
    /// </summary>
    Text expText;

    /// <summary>
    /// Reference to last velocity received from network.
    /// </summary>
    Vector3 lastVelocity;

    /// <summary>
    /// Player actual experience points.
    /// </summary>
    int playerExp;

    /// <summary>
    /// Player actual level.
    /// </summary>
    int playerLevel;

    /// <summary>
    /// Number of players killed in the match.
    /// </summary>
    int totalKills;

    /// <summary>
    /// Bool containing if the character is a bot, or is player controlled.
    /// </summary>
    bool playerControlled;

    /// <summary>
    /// Reference to the player's PlayFab username.
    /// </summary>
    public string playerPlayFabName { get; private set; }

    /// <summary>
    /// Start method. Gets all references.
    /// </summary>
    public void StartPlayer(bool playerControlled, TeamTypes myTeam, string playerPlayFabID, string playFabName)
    {
        this.playerPlayFabID = playerPlayFabID;
        this.myTeam = myTeam;
        this.playerControlled = playerControlled;
        this.playerPlayFabName = playFabName;

        myProperties = this.GetComponent<PlayerProperties>();
        networkLayer = this.GetComponent<PlayerNetworkLayer>();
        animationManager = this.GetComponent<PlayerAnimationManager>();
        shooterManager = this.GetComponent<ShooterManager>();
        aiManager = this.GetComponent<PlayerAIManager>();

        shooterManager.CreateBuffer(myTeam);

        myTower = GameController.instance.GetTower(myTeam);

        playerExp = playerLevel = 0;

        myProperties.Initialize();

        baseSprite.color = spriteTeamColors[myTeam.GetHashCode()];

        Debug.Log("initializing player. is human? " + playerControlled+" is local? "+networkLayer.isMine);
        if (networkLayer.isMine)
        {
            if (playerControlled)
            {
                Camera.main.transform.SetParent(this.transform, true);

                //levelText = GameController.instance.GetLevelText();
                expText = GameController.instance.GetExpText();

                GameplayUI.instance.Initialize(myTeam,this);
                GameplayUI.instance.UpdateSidebarStats(myProperties);

                InGameStoreAndCurrencyManager.instance.SetUpgradesCallbacks(new ProjectDelegates.OnPlayerBoughtStatUpgradeCallback[]{
                    myProperties.IncreaseAtk,myProperties.IncreaseMovSpd,myProperties.IncreaseHP
                });
            }

            this.transform.position = GameController.instance.GetRandomSpawnPoint(myTeam);
        }
    }

    /// <summary>
    /// Updates the state machine, according to actual state.
    /// </summary>
    void Update()
    {
        switch (actualState)
        {
            case PlayerStates.None:
                break;
            case PlayerStates.Idle:
                this.GetComponent<Rigidbody>().velocity = Vector3.zero;
                break;
            case PlayerStates.Moving:
                if (!networkLayer.isMine)
                {
                    this.GetComponent<Rigidbody>().velocity = lastVelocity;
                }
                break;
            case PlayerStates.Dead:
                break;
            default:
                break;
        }

        if (shooting)
        {
            shooterManager.UpdateShoot();
        }
    }

    /// <summary>
    /// Changes state machine actual state, and handles everything.
    /// </summary>
    /// <param name="newState">State machine's new state.</param>
    public void ChangeState(PlayerStates newState)
    {
        actualState = newState;

        switch (newState)
        {
            case PlayerStates.None:
                break;
            case PlayerStates.Idle:
                animationManager.PlayIdle();
                break;
            case PlayerStates.Moving:
                animationManager.PlayWalk(childTransform.localRotation);
                break;
            case PlayerStates.Dead:
                animationManager.PlayDie();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Moves the player, changes state machine.
    /// </summary>
    /// <param name="direction"></param>
    public void Move(Vector3 direction,bool fromInput)
    {
        if (this.actualState == PlayerStates.Dead) return;
        if (!shooting)
        {
            childTransform.LookAt(this.transform.position + direction);
        }
        this.ChangeState(PlayerStates.Moving);
        if (fromInput)
        {
            this.GetComponent<Rigidbody>().velocity = direction * myProperties.actualMovSpd;
        }
    }

    /// <summary>
    /// Stops moving, changes state machine.
    /// </summary>
    public void StopMoving()
    {
        if (this.actualState == PlayerStates.Dead) return;

        this.ChangeState(PlayerStates.Idle);
    }

    /// <summary>
    /// Rotates the player COG pivot.
    /// </summary>
    /// <param name="direction"></param>
    public void LookAt(Vector3 direction)
    {
        if (this.actualState == PlayerStates.Dead) return;

        childTransform.LookAt(direction);
    }

    /// <summary>
    /// Starts to try shooting.
    /// </summary>
    public void StartShoot()
    {
        shooting = true;
    }

    /// <summary>
    /// Shoots a new bullet.
    /// </summary>
    public void ShootBullet()
    {
        networkLayer.OnShootBullet();
        animationManager.PlayShoot();
    }

    /// <summary>
    /// Stops shooting.
    /// </summary>
    public void StopShooting()
    {
        shooting = false;
    }

    /// <summary>
    /// Called on collision with bullet. Receives bullet damage, tells network, and checks death.
    /// </summary>
    /// <param name="bulletStrength">Amount of life to take</param>
    /// <returns>Boolean telling if the player will be dead with this damage.</returns>
    public bool TakeBullet(float bulletStrength)
    {
		if (actualState == PlayerStates.Dead)
			return false;

        bulletStrength -= (myProperties.actualDef / 100f);

        bool dead = myProperties.UpdateHP(-bulletStrength);

        networkLayer.OnLifeChanged(myProperties.actualHP);

        return dead;
    }

    /// <summary>
    /// Gives the player a certain amount of experience points.
    /// </summary>
    /// <param name="amount">Amount of experience points.</param>
    public void AddExp(int amount)
    {
        playerExp += amount;

        if (networkLayer.isMine && playerControlled)
        {
            GameplayUI.instance.UpdateExpBar((playerExp * 1.0f) / (experienceForEachLevel[playerLevel] * 1.0f));
        }
        
        if (expText != null)
        {
            expText.text = "Exp: " + playerExp;
        }

        this.CheckLevelChange();
    }

    /// <summary>
    /// Handles player death, and checks if the game ended.
    /// </summary>
    public void Die()
    {
        GameplaySFXManager.instance.PlayDeathSound();
        shooterManager.DropAlternativeGun();

        if (aiManager != null)
        {
            aiManager.ChangeState(PlayerAIStates.None);
        }

        this.ChangeState(PlayerStates.Dead);

        Panel[] panels = GameController.instance.GetGamePanels();
        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i].myOwner == null) continue;

            if (panels[i].myOwner.name == this.name)
            {
                panels[i].RemoveOwner();
            }
        }

        GameController.instance.CheckEndGame();

        Invoke("Respawn", 1);
    }

    /// <summary>
    /// Event called on game end. Sends data to server.
    /// </summary>
    /// <param name="winner">Winner team.</param>
	public void OnGameEnded(TeamTypes winner){
        if (playerControlled)
        {
            networkLayer.OnGameEnded(winner);
        }
	}

    /// <summary>
    /// Event called on game end. Handles local behaviours.
    /// </summary>
    /// <param name="winner">Winner team.</param>
	public void OnNetworkGameEnded(TeamTypes winner){
		GameController.instance.FinishGame (winner);
	}

    /// <summary>
    /// Respawn player, and tells network to do the same.
    /// </summary>
    void Respawn()
    {
        if (myTower.actualState != TowerStates.Dead)
        {
            networkLayer.RespawnMe(GameController.instance.GetRandomSpawnPoint(myTeam));
        }
        else
        {
            //this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Event received from network, tells the player to respawn.
    /// </summary>
    public void OnNetworkRespawn(Vector3 spawnPosition)
    {
        myProperties.ResetHP();

        myLifebar.UpdateLifebarFill(myProperties.actualHP, myProperties.baseHP);
        
        this.transform.position = spawnPosition;
        this.ChangeState(PlayerStates.Idle);

        if (aiManager != null)
        {
            aiManager.ChangeState(PlayerAIStates.Moving);
        }
    }

    /// <summary>
    /// Checks level change, based on actual experience points.
    /// </summary>
    void CheckLevelChange()
    {
        if (playerLevel == experienceForEachLevel.Length - 1) return;

        int nextExp = experienceForEachLevel[playerLevel];
        if (playerExp >= nextExp)
        {
            this.LevelUp();
        }
    }

    /// <summary>
    /// Tells the network to level up.
    /// </summary>
    void LevelUp()
    {
        GameplaySFXManager.instance.PlayLevelUpSound();
        networkLayer.OnLevelChanged(playerLevel+1);
    }

    /// <summary>
    /// Receives network update.
    /// </summary>
    /// <param name="receivedPosition">New player position.</param>
    /// <param name="receivedRotation">New player rotation.</param>
    /// <param name="velocity">New player velocity.</param>
    public void OnNetworkUpdate(Vector3 receivedPosition, Quaternion receivedRotation, Vector2 velocity)
    {
        this.lastVelocity = new Vector3(velocity.x,0,velocity.y);

        this.transform.position = receivedPosition;
        this.transform.rotation = receivedRotation;
    }

    /// <summary>
    /// Receives new life points from network.
    /// </summary>
    /// <param name="newLife">New life points</param>
    public void OnNetworkLifeUpdate(float newLife)
    {
        myProperties.actualHP = newLife;

        myLifebar.UpdateLifebarFill(myProperties.actualHP, myProperties.baseHP);

        if (networkLayer.isMine && playerControlled)
		{
			GameplayUI.instance.UpdatePlayerLife(myProperties.actualHP, myProperties.baseHP);
		}

        if (newLife <= 0)
        {
            this.Die();
            if (myTeam == TeamTypes.Blue)
            {
                GameController.instance.ReportKill(TeamTypes.Orange);
            }
            else
            {
                GameController.instance.ReportKill(TeamTypes.Blue);
            }
        }
    }

    /// <summary>
    /// Receives new level from network.
    /// </summary>
    /// <param name="newLevel">New player level.</param>
    public void OnNetworkLevelUpdate(int newLevel)
    {
        playerLevel = newLevel;

        levelUpEffects[myTeam.GetHashCode()].GetComponent<ParticleSystem>().Stop(true);
        levelUpEffects[myTeam.GetHashCode()].GetComponent<ParticleSystem>().Play(true);

        myProperties.LevelUpStats(playerLevel);

        if (networkLayer.isMine && playerControlled)
        {
            GameplayUI.instance.UpdatePlayerLevel(playerLevel);
            GameplayUI.instance.ShowLevelUpPopup(myProperties.GetBonusesOptions(playerLevel));
            GameplayUI.instance.UpdateExpBar((playerExp * 1.0f) / (experienceForEachLevel[playerLevel] * 1.0f));
            GameplayUI.instance.UpdateSidebarStats(myProperties);
        }
    }

    /// <summary>
    /// Counts a kill for the player.
    /// </summary>
    public void AddKill()
    {
        this.AddExp(800);
        GameplaySFXManager.instance.PlayKillSound();
        totalKills++;
    }

    /// <summary>
    /// Reports kill amount to PlayFab.
    /// </summary>
    public void ReportKills()
    {
        if (networkLayer.isMine && playerControlled)
        {
            AccountManager.instance.killCount += totalKills;
            PlayFabManager.instance.ReportKillStats(AccountManager.instance.killCount);
        }
    }

    /// <summary>
    /// Reports a new win to PlayFab, sends push to losers.
    /// </summary>
    public void ReportWin()
    {
        if (networkLayer.isMine && playerControlled)
        {
            GameplaySFXManager.instance.PlayWinSound();
            AccountManager.instance.winsAmount++;
            PlayFabManager.instance.ReportWins(AccountManager.instance.winsAmount);

            TeamTypes otherTeam = myTeam == TeamTypes.Blue ? TeamTypes.Orange : TeamTypes.Blue;

            PlayerManager[] losers = GameController.instance.GetPlayersOfTeam(otherTeam);

            for (int i = 0; i < losers.Length; i++)
            {
                if (losers[i].playerPlayFabID.Length > 0)
                {
                  	//TO DO: add Cloud Script to do this
                    // PlayFabManager.instance.SendPush(losers[i].playerPlayFabID, PlayFabManager.instance.playerUsername + " has just defeated you!");
                }
            }
        }
    }

    /// <summary>
    /// Loses game. Just plays the sound.
    /// </summary>
    public void LoseGame()
    {
        if (networkLayer.isMine && playerControlled)
        {
            GameplaySFXManager.instance.PlayLoseSound();
        }
    }

    /// <summary>
    /// Sends match experience points to account.
    /// </summary>
    public void GiveExpToAccount()
    {
        AccountManager.instance.GiveAccountExp(playerExp);
    }

    /// <summary>
    /// "Bridge" method. Used to tell ShooterManager that the player caught a new weapon.
    /// </summary>
    /// <param name="newGun">New gun type.</param>
    public void UpdateAlternativeWeapon(GunTypes newGun)
    {
        shooterManager.SetAlternativeGun(newGun);
    }

    public void GiveStatusBonus(PlayerStats bonusStat)
    {
        myProperties.GiveBonus(bonusStat, playerLevel);

        if (networkLayer.isMine && playerControlled)
        {
            GameplayUI.instance.UpdateSidebarStats(myProperties);
        }
    }
}