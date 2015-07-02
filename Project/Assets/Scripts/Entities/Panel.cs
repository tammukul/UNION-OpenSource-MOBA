using UnityEngine;
using System.Collections;

public class Panel : MonoBehaviour
{
    /// <summary>
    /// Audio Clip to play when the panel is dominated.
    /// </summary>
    [SerializeField]
    AudioClip dominatePanelClip;

    /// <summary>
    /// Base object of panel. Reference used to get substance material.
    /// </summary>
    [SerializeField]
    GameObject panelBase;

    /// <summary>
    /// Colors the particle system will use, depending on the panel state.
    /// </summary>
    [SerializeField]
    Color[] effectColors;

    /// <summary>
    /// Particle System reference.
    /// </summary>
    [SerializeField]
    ParticleSystem effectSystem;

    /// <summary>
    /// Substance reference. Used to change texture parameters, depending on the panel state.
    /// </summary>
    ProceduralMaterial substance;

    /// <summary>
    /// Network layer reference.
    /// </summary>
    PanelNetworkLayer networkLayer;

    /// <summary>
    /// Other panels in game reference.
    /// </summary>
    //Panel[] otherPanels;

    /// <summary>
    /// Panel owner reference.
    /// </summary>
    public PlayerManager myOwner { get; private set; }

    /// <summary>
    /// Variable controlling the time the player spent inside panel, to control it
    /// </summary>
    float timeInPanel;

    /// <summary>
    /// Reference to the player who is trying to control the panel.
    /// </summary>
    PlayerManager probableOwner;

    /// <summary>
    /// Gets all necessary references.
    /// </summary>
    void Start()
    {
        networkLayer = this.GetComponent<PanelNetworkLayer>();

        substance = panelBase.GetComponent<Renderer>().material as ProceduralMaterial;

        //otherPanels = GameController.instance.GetGamePanels();
    }

    /// <summary>
    /// Handles collision. If a player collides with panel, and both are free, they are linked.
    /// </summary>
    /// <param name="target">Player who collided.</param>
    void OnTriggerEnter(Collider target)
    {
        if (myOwner == null && probableOwner == null)
        {
            /*for (int i = 0; i < otherPanels.Length; i++)
            {
                if (otherPanels[i].myOwner != null)
                {
                    if (otherPanels[i].myOwner.name == target.name) return;
                }
            }*/

            PlayerManager owner = target.GetComponent<PlayerManager>();
            if (owner.actualState != PlayerStates.Dead)
            {
                probableOwner = owner;
                timeInPanel = 0;
            }
        }
    }

    /// <summary>
    /// Updates collision. If the player stays on trigger for a certain amount of time, the the player dominates the panel.
    /// </summary>
    /// <param name="target">Player who collided.</param>
    void OnTriggerStay(Collider target)
    {
        if (myOwner == null && probableOwner != null)
        {
            if (target.name == probableOwner.name)
            {
                timeInPanel += Time.deltaTime;
                if (timeInPanel > 1)
                {
                    networkLayer.OnPlayerChanged(target.name);
                    myOwner = probableOwner;
                }
            }
        }
    }

    /// <summary>
    /// Handles collision exit. If the player who left was the probable owner, they are unlinked.
    /// </summary>
    /// <param name="target">Player who left collision.</param>
    void OnTriggerExit(Collider target)
    {
        if (probableOwner != null)
        {
            if (target.name == probableOwner.name)
            {
                probableOwner = null;
                timeInPanel = 0;
            }
        }
    }

    /// <summary>
    /// Removes owner from panel.
    /// </summary>
    public void RemoveOwner()
    {
        networkLayer.OnPlayerChanged("");
    }

    /// <summary>
    /// Receives from network new owner.
    /// Updates materials and effects color.
    /// </summary>
    /// <param name="player">New player name.</param>
    public void OnNetworkPlayerChanged(string player)
    {
        if (myOwner != null)
        {
            if (myOwner.name == player)
            {
                return;
            }
        }

        if (myOwner != null && probableOwner != null)
        {
            if (myOwner.name == probableOwner.name)
            {
                probableOwner = null;
            }
        }

        myOwner = null;

        if (player.Length > 0)
        {
            myOwner = GameObject.Find(player).GetComponent<PlayerManager>();
        }

        if (AudioManager.instance.sfxEnabled && myOwner != null)
        {
            GetComponent<AudioSource>().clip = dominatePanelClip;
            GetComponent<AudioSource>().Play();
        }

        if (myOwner == null)
        {
            effectSystem.startColor = effectColors[0];
            substance.SetProceduralFloat("ColorSwitch", 1);
        }
        else if (myOwner.myTeam == TeamTypes.Blue)
        {
            effectSystem.startColor = effectColors[1];
            substance.SetProceduralFloat("ColorSwitch", 2);
        }
        else
        {
            effectSystem.startColor = effectColors[2];
            substance.SetProceduralFloat("ColorSwitch", 3);
        }
        substance.RebuildTextures();
        effectSystem.Stop();
        effectSystem.Simulate(15, true, true);
        effectSystem.Play();

        if (myOwner != null)
        {
            myOwner.AddExp(1000);
        }

        GameController.instance.CheckPanelsOwners();
    }
}