using UnityEngine;
using System.Collections;

public class PlayerProperties : MonoBehaviour
{
    // Base player stats.

    /// <summary>
    /// Default attack value
    /// </summary>
    public float baseAtk = 5;

    /// <summary>
    /// Default movement speed
    /// </summary>
    public float baseMovSpd = 16;

    /// <summary>
    /// Default HP
    /// </summary>
    public float baseHP = 100;

    /// <summary>
    /// Default defense 
    /// </summary>
    public float baseDef;

    /// <summary>
    /// Default fire rate
    /// </summary>
    public float baseFireRate = 0.2f;

    //Stats modifiers



    /// <summary>
    /// Value to add to attack
    /// </summary>
    public float atkModifier { get; private set; }
    /// <summary>
    /// Value to add to the speed
    /// </summary>
    public float movSpdModifier { get; private set; }
    /// <summary>
    /// Value to add to HP
    /// </summary>
    public float hpModifier { get; private set; }

    /// <summary>
    /// Value to add to defense
    /// </summary>
    public float defModifier { get; private set; }

    //Value to add to Fire rate
    public float fireRateModifier { get; private set; }

    // Actual player stats.
    public float actualAtk { get { return baseAtk + atkModifier; } }
    public float actualMovSpd { get { return baseMovSpd + movSpdModifier; } }
    public float actualHP;
    public float actualDef { get { return baseDef + defModifier; } }
    public float actualFireRate { get { return baseFireRate + fireRateModifier; } }

    protected PlayerStats[,] levelUpgradeOptions;

    protected float[,] levelUpgradeBonuses;

    /// <summary>
    /// Initializes every stat value.
    /// </summary>
    public void Initialize()
    {
        actualHP = baseHP;
    }

    /// <summary>
    /// Increases attack amount based on multiplier.
    /// </summary>
    /// <param name="multiplier">Amount to increase.</param>
    public void IncreaseAtk(float multiplier)
    {
        atkModifier += (baseAtk * (multiplier / 100f));
        Debug.Log("bought attack item. new attack " + actualAtk+atkModifier);
    }

    /// <summary>
    /// Increases movement speed amount based on multiplier.
    /// </summary>
    /// <param name="multiplier">Amount to increase.</param>
    public void IncreaseMovSpd(float multiplier)
    {
        movSpdModifier += (baseMovSpd * (multiplier / 100f));
        Debug.Log("bought movspd item. new movspd " + actualMovSpd+movSpdModifier);
    }

    /// <summary>
    /// Increases health points amount based on multiplier.
    /// </summary>
    /// <param name="multiplier">Amount to increase.</param>
    public void IncreaseHP(float multiplier)
    {
        hpModifier += (baseHP * (multiplier / 100f));
        actualHP += hpModifier;
        Debug.Log("bought hp item. new hp " + actualHP);
    }

    /// <summary>
    /// Updates HP, tells if the player died.
    /// </summary>
    /// <param name="amount">Amount to give or take from the player.</param>
    /// <returns>Returns if the player is dead.</returns>
    public bool UpdateHP(float amount)
    {
        actualHP = Mathf.Clamp(actualHP + amount, 0, baseHP+hpModifier);

        return actualHP == 0;
    }

    /// <summary>
    /// Resets player HP.
    /// </summary>
    public void ResetHP()
    {
        actualHP = baseHP;
        GameplayUI.instance.UpdatePlayerLife(actualHP, baseHP+hpModifier);
    }

    /// <summary>
    /// Updates player stats, based on level up.
    /// </summary>
    /// <param name="level">New Level.</param>
    public void LevelUpStats(int level)
    {
        atkModifier += (1 + ((level - 1) / 10f)) * 2;
        //baseAtk += atkModifier;
        //actualAtk += atkModifier;

        movSpdModifier += (((1 + ((level - 1) / 10f))) / 2f) * 2;
        //baseMovSpd += movSpdModifier;
        //actualMovSpd += movSpdModifier;

        defModifier += (1 + ((level - 1) / 10f)) * 2;
        //baseDef += defModifier;
        //actualDef += defModifier;

        hpModifier += (1 + ((level - 1) / 10f)) * 2;
        //baseHP += HPModifier;
        actualHP += hpModifier;
    }

    /// <summary>
    /// Gives stat bonuses, depending on user's choice.
    /// </summary>
    /// <param name="stat">Stat to update.</param>
    /// <param name="level">Player's new level.</param>
    public void GiveBonus(PlayerStats stat, int level)
    {
        float amount = 0;

        if (stat == levelUpgradeOptions[level, 0])
        {
            amount = levelUpgradeBonuses[level, 0];
        }
        else
        {
            amount = levelUpgradeBonuses[level, 1];
        }

        switch (stat)
        {
            case PlayerStats.Atk:
                atkModifier += amount;
                break;
            case PlayerStats.MovSpd:
                movSpdModifier += amount;
                break;
            case PlayerStats.HP:
                hpModifier += amount;
                break;
            case PlayerStats.Def:
                defModifier += amount;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Gets bonuses options for new level, when levelled up.
    /// </summary>
    /// <param name="level">New level.</param>
    /// <returns>Array of stats to show.</returns>
    public PlayerStats[] GetBonusesOptions(int level)
    {
        return new PlayerStats[] { levelUpgradeOptions[level, 0], levelUpgradeOptions[level, 1] };
    }
}