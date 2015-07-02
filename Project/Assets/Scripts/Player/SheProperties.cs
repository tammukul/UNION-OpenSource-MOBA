using UnityEngine;
using System.Collections;

public class SheProperties : PlayerProperties
{
    /// <summary>
    /// Initialization method. Creates all the upgrade options.
    /// </summary>
    void Start()
    {
        levelUpgradeOptions = new PlayerStats[,] {  { PlayerStats.MovSpd,   PlayerStats.Atk },
                                                    { PlayerStats.MovSpd,   PlayerStats.Atk },
                                                    { PlayerStats.Def,      PlayerStats.Atk },
                                                    { PlayerStats.HP,       PlayerStats.Atk },
                                                    { PlayerStats.MovSpd,   PlayerStats.Atk },
                                                    { PlayerStats.Def,      PlayerStats.Atk },
                                                    { PlayerStats.HP,       PlayerStats.Atk },
                                                    { PlayerStats.MovSpd,   PlayerStats.Atk },
                                                    { PlayerStats.Def,      PlayerStats.Atk },
                                                    { PlayerStats.MovSpd,   PlayerStats.Atk }
                                                  };

        levelUpgradeBonuses = new float[,]        { { 1,                    0.5f},
                                                    { 1,                    0.5f},
                                                    { 1,                    0.5f},
                                                    { 20,                   0.5f},
                                                    { 3,                    1.5f},
                                                    { 1,                    1.0f},
                                                    { 20,                   1.0f},
                                                    { 2,                    1.0f},
                                                    { 1,                    1.0f},
                                                    { 3,                    3.0f}
                                                  };
    }
}