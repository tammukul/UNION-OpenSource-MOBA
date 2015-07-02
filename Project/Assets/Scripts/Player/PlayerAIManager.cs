using UnityEngine;
using System.Collections;

public class PlayerAIManager : MonoBehaviour
{
    /// <summary>
    /// Player Manager Reference.
    /// </summary>
    PlayerManager playerManager;

    /// <summary>
    /// Player properties reference.
    /// </summary>
    PlayerProperties myProperties;

    /// <summary>
    /// Nav Mesh Agent reference.
    /// </summary>
    NavMeshAgent navMeshAgent;

    /// <summary>
    /// Boolean containing if the local player is controlling the AI.
    /// </summary>
    bool controlledLocally;

    /// <summary>
    /// Actual AI state.
    /// </summary>
    PlayerAIStates actualState;

    /// <summary>
    /// Map width, used to calculate random point in it.
    /// </summary>
    [SerializeField]
    float mapWidth;
    /// <summary>
    /// Map height, used to calculate random point in it.
    /// </summary>
    [SerializeField]
    float mapHeight;

    /// <summary>
    /// Max distance from other entities, to calculate behaviour.
    /// </summary>
    [SerializeField]
    float maxActionDistance;

    /// <summary>
    /// Time to count before changing player direction.
    /// </summary>
    [SerializeField]
    float maxMovementDirectionTime;
    /// <summary>
    /// Counter to check how long has passed since movement started.
    /// </summary>
    float movementDirectionCounter;

    /// <summary>
    /// Time to count before changing player attack movement.
    /// </summary>
    [SerializeField]
    float maxAttackMovementTime;
    /// <summary>
    /// Counter to check how long has passed since last attack movement change.
    /// </summary>
    float attackMovementCounter;

    /// <summary>
    /// Direction the player is moving in attack.
    /// </summary>
    int attackMovementDirection = 1;

    /// <summary>
    /// Counter to handle if time to occupy panel has aleady passed.
    /// </summary>
    float occupyingPanelCounter;

    /// <summary>
    /// All panels reference.
    /// </summary>
    Panel[] gamePanels;
    /// <summary>
    /// All enemies reference.
    /// </summary>
    PlayerManager[] enemies;

    /// <summary>
    /// Enemy tower reference.
    /// </summary>
    TowerManager enemyTower;
    /// <summary>
    /// Panel the AI is targeting.
    /// </summary>
    Panel targetPanel;
    /// <summary>
    /// Player the AI is targeting.
    /// </summary>
    PlayerManager target;

    /// <summary>
    /// Reference to the enemy team.
    /// </summary>
    TeamTypes enemyTeam;

    /// <summary>
    /// Initializes AI. Gets all references, and starts moving.
    /// </summary>
    public void StartAI()
    {
        playerManager = this.GetComponent<PlayerManager>();
        myProperties = this.GetComponent<PlayerProperties>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        navMeshAgent.speed = myProperties.actualMovSpd;

        controlledLocally = true;

        this.FindNewPoint();

        gamePanels = GameController.instance.GetGamePanels();

        int enemyID = playerManager.myTeam.GetHashCode();
        enemyID++;
        if (enemyID > TeamTypes.Orange.GetHashCode())
        {
            enemyID = 0;
        }

        enemyTeam = (TeamTypes)enemyID;
        enemyTower = GameController.instance.GetTower(enemyTeam);

        enemies = GameController.instance.GetPlayersOfTeam(enemyTeam);
    }

    /// <summary>
    /// Update. Checks if is controlling locally, if so, updates state machine.
    /// </summary>
    void Update()
    {
        if (!controlledLocally) return;

        this.UpdateStateMachine();
    }

    /// <summary>
    /// Updates state machine actual state, and starts everything needed.
    /// </summary>
    /// <param name="newState">State to change to.</param>
    public void ChangeState(PlayerAIStates newState)
    {
        if (controlledLocally && actualState == newState) return;

        if ((actualState == PlayerAIStates.AttackingPlayer || actualState == PlayerAIStates.AttackingTower
             || actualState == PlayerAIStates.Fleeing) && newState != PlayerAIStates.Fleeing)
        {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            playerManager.StopShooting();
        }

        actualState = newState;

        navMeshAgent.stoppingDistance = 0;
        switch (actualState)
        {
            case PlayerAIStates.None:
                target = null;
                targetPanel = null;
                break;
            case PlayerAIStates.Moving:
                movementDirectionCounter = maxMovementDirectionTime;
                playerManager.ChangeState(PlayerStates.Moving);
                break;
            case PlayerAIStates.MovingToPanel:
                navMeshAgent.SetDestination(targetPanel.transform.position);
                break;
            case PlayerAIStates.OcuppyingPanel:
                playerManager.StopMoving();
                occupyingPanelCounter = 1;
                break;
            case PlayerAIStates.AttackingPlayer:
            case PlayerAIStates.AttackingTower:
                navMeshAgent.stoppingDistance = 7;
                navMeshAgent.SetDestination(enemyTower.transform.position);
                playerManager.StopMoving();
                playerManager.StartShoot();
                attackMovementCounter = maxAttackMovementTime;
                break;
        }
    }

    /// <summary>
    /// Updates the state machine. Checks the actual state, and handles each one.
    /// </summary>
    void UpdateStateMachine()
    {
        switch (actualState)
        {
            case PlayerAIStates.None:
                break;
            case PlayerAIStates.Moving:
                this.UpdateMovingState();
                playerManager.Move(transform.forward,false);
                break;
            case PlayerAIStates.MovingToPanel:
                this.UpdateMovingToPanelState();
                playerManager.Move(transform.forward, false);
                break;
            case PlayerAIStates.OcuppyingPanel:
                this.UpdateOccupyingPanel();
                break;
            case PlayerAIStates.AttackingPlayer:
                this.UpdateAttackingState();
                break;
            case PlayerAIStates.AttackingTower:
                playerManager.Move(transform.forward, false);
                break;
        }
    }

    /// <summary>
    /// Handles movement state.
    /// </summary>
    void UpdateMovingState()
    {
        movementDirectionCounter -= Time.deltaTime;

        if (movementDirectionCounter <= 0 ||
            navMeshAgent.remainingDistance < 2f)
        {
            this.FindNewPoint();
        }

        this.CheckDistances();
    }

    /// <summary>
    /// Handles moving to panel state.
    /// </summary>
    void UpdateMovingToPanelState()
    {
        if (navMeshAgent.remainingDistance < 2)
        {
            this.ChangeState(PlayerAIStates.OcuppyingPanel);
        }
        this.CheckDistances();
    }

    /// <summary>
    /// Handles occupying panel state.
    /// </summary>
    void UpdateOccupyingPanel()
    {
        occupyingPanelCounter -= Time.deltaTime;
        if (occupyingPanelCounter < 0)
        {
            this.ChangeState(PlayerAIStates.Moving);
        }
        this.CheckDistances();
    }

    /// <summary>
    /// Handles attacking state.
    /// </summary>
    void UpdateAttackingState()
    {
        this.GetComponent<Rigidbody>().velocity = transform.right * attackMovementDirection * myProperties.actualMovSpd;

        Quaternion newRotation;
        if (actualState == PlayerAIStates.AttackingPlayer)
        {
            newRotation = Quaternion.LookRotation(target.transform.position - this.transform.position);
            
            float distance = Vector3.Distance(this.transform.position, target.transform.position);
            if (distance > maxActionDistance)
            {
                this.ChangeState(PlayerAIStates.Moving);
            }
        }
        else
        {
            newRotation = Quaternion.LookRotation(enemyTower.transform.position - this.transform.position);

            float distance = Vector3.Distance(this.transform.position, enemyTower.transform.position);
            if (distance > maxActionDistance || enemyTower.actualState == TowerStates.Dead)
            {
                this.ChangeState(PlayerAIStates.Moving);
            }
        }
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, newRotation, Time.deltaTime * 7);


        attackMovementCounter -= Time.deltaTime;
        if (attackMovementCounter < 0)
        {
            attackMovementDirection *= -1;
            attackMovementCounter = maxAttackMovementTime;
        }
    }

    /// <summary>
    /// Checks distances from all entities, and updates state, depending on what is happening.
    /// </summary>
    void CheckDistances()
    {
        if (enemies.Length < GameController.instance.GetPlayers().Length / 2)
        {
            enemies = GameController.instance.GetPlayersOfTeam(enemyTeam);
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            float distance = Vector3.Distance(this.transform.position, enemies[i].transform.position);
            if (distance < maxActionDistance)
            {
                target = enemies[i];
                this.ChangeState(PlayerAIStates.AttackingPlayer);
                return;
            }
        }

        if (targetPanel == null)
        {
            for (int i = 0; i < gamePanels.Length; i++)
            {
                if (gamePanels[i].myOwner == null || gamePanels[i].myOwner != playerManager)
                {
                    float distance = Vector3.Distance(this.transform.position, gamePanels[i].transform.position);
                    if (distance < maxActionDistance)
                    {
                        targetPanel = gamePanels[i];
                        this.ChangeState(PlayerAIStates.MovingToPanel);
                        return;
                    }
                }
            }
        }

        if (!enemyTower.shieldActive)
        {
            float distance = Vector3.Distance(this.transform.position, enemyTower.transform.position);
            if (distance < maxActionDistance * 2)
            {
                this.ChangeState(PlayerAIStates.AttackingTower);
                return;
            }
        }
    }

    /// <summary>
    /// Finds a new random point in map, and starts moving to it.
    /// </summary>
    private void FindNewPoint()
    {
        this.ChangeState(PlayerAIStates.None);
        this.ChangeState(PlayerAIStates.Moving);

        Vector3 randomPoint = new Vector3(Random.Range(-mapWidth,mapWidth),-0.9f,Random.Range(-mapHeight,mapHeight));

        navMeshAgent.SetDestination(randomPoint);
    }
}