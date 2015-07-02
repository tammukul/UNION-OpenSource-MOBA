using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// Static instance.
    /// </summary>
    public static GameController instance;

    /// <summary>
    /// Reference to the fadeout animation to play when the game ends.
    /// </summary>
    [SerializeField]
    GameObject blackFadeout;

    /// <summary>
    /// Reference to all game towers.
    /// </summary>
    [SerializeField]
    TowerManager[] gameTowers;

    /// <summary>
    /// Reference to all game panels.
    /// </summary>
    [SerializeField]
    Panel[] gamePanels;

    /// <summary>
    /// Number of panels to disable tower's shield.
    /// </summary>
    [SerializeField]
    int panelsToDisableShield;

    /// <summary>
    /// Reference to level text on screen.
    /// </summary>
    [SerializeField]
    Text levelTxt;

    /// <summary>
    /// Reference ot experience text on screen.
    /// </summary>
    [SerializeField]
    Text expTxt;

    /// <summary>
    /// Blue team's spawn points reference;
    /// </summary>
    [SerializeField]
    GameObject[] blueSpawnPoints;

    /// <summary>
    /// Orange team's spawn points reference;
    /// </summary>
    [SerializeField]
    GameObject[] orangeSpawnPoints;

    /// <summary>
    /// Reference of my team's screen lifebars.
    /// </summary>
    [SerializeField]
    PlayerWorldLifebar[] myTeamLifebars;

    /// <summary>
    /// Reference of enemy team's screen lifebars.
    /// </summary>
    [SerializeField]
    PlayerWorldLifebar[] enemyTeamLifebars;

    /// <summary>
    /// Reference to the local player's team.
    /// </summary>
    public TeamTypes localPlayerTeam { get; set; }

    /// <summary>
    /// All players' reference.
    /// </summary>
    PlayerManager[] players;

    /// <summary>
    /// Game's actual state.
    /// </summary>
    GameStates actualState;

    /// <summary>
    /// Total game time.
    /// </summary>
    float gameTimeInSeconds;

    /// <summary>
    /// Counter of orange team kills.
    /// </summary>
    int orangeTeamKillCount;
    /// <summary>
    /// Counter of blue team kills.
    /// </summary>
    int blueTeamKillCount;

    /// <summary>
    /// Initializes lists, sets static reference.
    /// </summary>
    void Awake()
    {
        instance = this;

        players = new PlayerManager[0];
    }

    /// <summary>
    /// Starts everything.
    /// </summary>
    void Start()
    {
        InGameStoreAndCurrencyManager.instance.Initialize();
    }

    /// <summary>
    /// Updates game time, if game is running.
    /// </summary>
    void Update()
    {
        if (actualState == GameStates.Gameplay)
        {
            float newGameTime = gameTimeInSeconds + Time.deltaTime;
            if ((int)newGameTime > (int)gameTimeInSeconds)
            {
                GameplayUI.instance.UpdateGameTime((int)newGameTime);
            }
            gameTimeInSeconds = newGameTime;
        }
    }

    /// <summary>
    /// Gets all towers in the game.
    /// </summary>
    /// <returns>Returns all towers.</returns>
    public TowerManager[] GetTowers()
    {
        return gameTowers;
    }

    /// <summary>
    /// Gets specific tower from team.
    /// </summary>
    /// <param name="team">Tower's team.</param>
    /// <returns>Tower from the team.</returns>
    public TowerManager GetTower(TeamTypes team)
    {
        for (int i = 0; i < gameTowers.Length; i++)
        {
            if (gameTowers[i].myTeam == team)
            {
                return gameTowers[i];
            }
        }
        return null;
    }

    /// <summary>
    /// Gets level text reference.
    /// </summary>
    /// <returns>Level text.</returns>
    public Text GetLevelText()
    {
        return levelTxt;
    }

    /// <summary>
    /// Gets experence text reference.
    /// </summary>
    /// <returns>Experience text.</returns>
    public Text GetExpText()
    {
        return expTxt;
    }

    /// <summary>
    /// Adds player to reference list. Called after player instantiation.
    /// </summary>
    /// <param name="player">New player.</param>
    public void AddPlayer(PlayerManager player)
    {
        actualState = GameStates.Gameplay;

        if (player.myTeam == localPlayerTeam)
        {
            for (int i = 0; i < myTeamLifebars.Length; i++)
            {
                if (!myTeamLifebars[i].hasOwner)
                {
                    myTeamLifebars[i].gameObject.SetActive(true);
                    myTeamLifebars[i].Initialize(player);
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < enemyTeamLifebars.Length; i++)
            {
                if (!enemyTeamLifebars[i].hasOwner)
                {
                    enemyTeamLifebars[i].gameObject.SetActive(true);
                    enemyTeamLifebars[i].Initialize(player);
                    break;
                }
            }
        }

        System.Array.Resize(ref players, players.Length + 1);
        players[players.Length - 1] = player;

        for (int i = 0; i < gameTowers.Length; i++)
        {
            gameTowers[i].AddPlayer(player);
        }
    }

    /// <summary>
    /// Checks number of panels owned, to see if a tower's shield must be disabled/enabled.
    /// </summary>
    public void CheckPanelsOwners()
    {
        int bluePanels = 0;
        int orangePanels = 0;

        for (int i = 0; i < gamePanels.Length; i++)
        {
            if (gamePanels[i].myOwner == null) continue;

            if (gamePanels[i].myOwner.myTeam == TeamTypes.Blue)
            {
                bluePanels++;
            }
            else
            {
                orangePanels++;
            }
        }

        if (bluePanels >= panelsToDisableShield)
        {
            gameTowers[TeamTypes.Orange.GetHashCode()].DeactivateShield();
        }
        else
        {
            gameTowers[TeamTypes.Orange.GetHashCode()].ActivateShield();
        }

        if (orangePanels >= panelsToDisableShield)
        {
            gameTowers[TeamTypes.Blue.GetHashCode()].DeactivateShield();
        }
        else
        {
            gameTowers[TeamTypes.Blue.GetHashCode()].ActivateShield();
        }
    }

    /// <summary>
    /// Checks if the game has ended. Game ends if a team has no more towers and players alive.
    /// </summary>
    public void CheckEndGame()
    {
        for (int i = 0; i < gameTowers.Length; i++)
        {
            if (gameTowers[i].actualState == TowerStates.Dead)
            {
                bool anyoneAlive = false;
                for (int j = 0; j < players.Length; j++)
                {
                    if (players[j].myTeam == gameTowers[i].myTeam)
                    {
                        if (players[j].actualState != PlayerStates.Dead)
                        {
                            anyoneAlive = true;
                            break;
                        }
                    }
                }

                if (!anyoneAlive)
                {

                    int winnerTeamID = gameTowers[i].myTeam.GetHashCode() + 1;
                    if (winnerTeamID > 1) winnerTeamID = 0;

                    TeamTypes winnerTeam = (TeamTypes)winnerTeamID;

                    this.FinishGame(winnerTeam);

                    EndGameScreen.winnerTeam = winnerTeam;
                }
            }
        }
    }

    /// <summary>
    /// Finishes the game.
    /// </summary>
    public void FinishGame(TeamTypes winner)
    {
		if (actualState == GameStates.EndGame)
			return;

        blackFadeout.SetActive(true);

        actualState = GameStates.EndGame;

        Debug.Log("FinishGame");
        for (int i = 0; i < players.Length; i++)
        {
			if(players[i] == null) continue;

            players[i].ReportKills();
            players[i].GiveExpToAccount();

            if (players[i].myTeam == winner)
            {
                players[i].ReportWin();
            }
            else
            {
                players[i].LoseGame();
            }

			players[i].OnGameEnded(winner);
        }

        Invoke("DisableCamera", 1f);
    }

    void DisableCamera()
    {
        Camera.main.enabled = false;
        Invoke("LoadNextLevel", 0.5f);
    }

    void LoadNextLevel()
    {
        Application.LoadLevel("EndGame");
    }

    /// <summary>
    /// Returns all game panels.
    /// </summary>
    /// <returns>Game panels.</returns>
    public Panel[] GetGamePanels()
    {
        return gamePanels;
    }

    /// <summary>
    /// Reports a kill from a specific team.
    /// </summary>
    /// <param name="team">Team who killed a player.</param>
    public void ReportKill(TeamTypes team)
    {
        if (team == TeamTypes.Blue)
        {
            blueTeamKillCount++;
            GameplayUI.instance.UpdateKillcount(team, blueTeamKillCount);
        }
        else
        {
            orangeTeamKillCount++;
            GameplayUI.instance.UpdateKillcount(team, orangeTeamKillCount);
        }
    }

    /// <summary>
    /// Gets a specific player, based on their name.
    /// </summary>
    /// <param name="name">Player name.</param>
    /// <returns>Player found. If none found, returns null.</returns>
    public PlayerManager GetPlayer(string name)
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].name == name) return players[i];
        }
        return null;
    }

    /// <summary>
    /// Gets all players from a determined team.
    /// </summary>
    /// <param name="team">Team to get players from.</param>
    /// <returns>Players array, containing all that belong to the team.</returns>
    public PlayerManager[] GetPlayersOfTeam(TeamTypes team)
    {
        PlayerManager[] playersOfTeam = new PlayerManager[0];
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].myTeam == team)
            {
                System.Array.Resize<PlayerManager>(ref playersOfTeam, playersOfTeam.Length + 1);
                playersOfTeam[playersOfTeam.Length - 1] = players[i];
            }
        }
        return playersOfTeam;
    }

    /// <summary>
    /// Gets all players.
    /// </summary>
    /// <returns>Players array.</returns>
    public PlayerManager[] GetPlayers()
    {
        return players;
    }

    /// <summary>
    /// Gets random spawn point from team. Used to handle player respawn.
    /// </summary>
    /// <param name="team">Team to get spawn point.</param>
    /// <returns>Random spawn point position.</returns>
    public Vector3 GetRandomSpawnPoint(TeamTypes team)
    {
        if (team == TeamTypes.Blue)
        {
            return blueSpawnPoints[Random.Range(0, blueSpawnPoints.Length)].transform.position;
        }
        else
        {
            return orangeSpawnPoints[Random.Range(0, blueSpawnPoints.Length)].transform.position;
        }
    }
}