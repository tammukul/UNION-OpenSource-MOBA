using UnityEngine;
using System.Collections;

public class HeProperties : PlayerProperties
{
    /// <summary>
    /// Initialization method. Creates all the upgrade options.
    /// </summary>
    void Start()
    {
        levelUpgradeOptions = new PlayerStats[,] {  { PlayerStats.Def,      PlayerStats.HP },
                                                    { PlayerStats.MovSpd,   PlayerStats.HP },
                                                    { PlayerStats.Def,      PlayerStats.HP },
                                                    { PlayerStats.Atk,      PlayerStats.HP },
                                                    { PlayerStats.Def,      PlayerStats.HP },
                                                    { PlayerStats.MovSpd,   PlayerStats.HP },
                                                    { PlayerStats.Atk,      PlayerStats.HP },
                                                    { PlayerStats.Def,      PlayerStats.HP },
                                                    { PlayerStats.Def,      PlayerStats.HP },
                                                    { PlayerStats.Def,      PlayerStats.HP }
                                                  };

        levelUpgradeBonuses = new float[,]        { { 1,                    5},
                                                    { 1,                    5},
                                                    { 1,                    5},
                                                    { 0.5f,                 5},
                                                    { 3,                    20},
                                                    { 2,                    5},
                                                    { 1,                    5},
                                                    { 2,                    5},
                                                    { 2,                    5},
                                                    { 4,                    20}
                                                  };
    }
}