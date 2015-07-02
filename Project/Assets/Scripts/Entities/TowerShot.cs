using UnityEngine;
using System.Collections;

public class TowerShot : MonoBehaviour
{
    /// <summary>
    /// Array of effects to activate, when tower shoots.
    /// </summary>
    [SerializeField]
    GameObject[] effectsToActivate;

    /// <summary>
    /// Bool defining if will test collsisions. Only network owner tests for collisions.
    /// </summary>
    bool testCollisions;

    /// <summary>
    /// Amount to take from player's life, if hit.
    /// </summary>
    float shotStrength;

    /// <summary>
    /// Tower's team.
    /// </summary>
    TeamTypes towerTeam;

    /// <summary>
    /// Activates all effects, sets properties, and calls for deactivate after a certain amount of time.
    /// </summary>
    /// <param name="willTestCollisions">Is network owner?</param>
    /// <param name="shotStrength">Amount to take from player's life, if hit.</param>
    public void Shoot(bool willTestCollisions, float shotStrength, TeamTypes towerTeam)
    {
        this.testCollisions = willTestCollisions;
        this.shotStrength = shotStrength;
        this.towerTeam = towerTeam;

        for (int i = 0; i < effectsToActivate.Length; i++)
        {
            effectsToActivate[i].SetActive(true);
        }

        Invoke("DeactivateEffects", 1.2f);
    }

    /// <summary>
    /// Deactivates all shoot effects, and the object itself.
    /// </summary>
    public void DeactivateEffects()
    {
        for (int i = 0; i < effectsToActivate.Length; i++)
        {
            effectsToActivate[i].SetActive(false);
        }
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Event on collision, probably with player.
    /// </summary>
    /// <param name="target"></param>
    void OnTriggerEnter(Collider target)
    {
        if (!testCollisions) return;

        if (target.tag == "Player")
        {
            PlayerManager player = target.GetComponent<PlayerManager>();
            if (player.myTeam != towerTeam)
            {
                player.TakeBullet(shotStrength);
            }
        }
    }
}