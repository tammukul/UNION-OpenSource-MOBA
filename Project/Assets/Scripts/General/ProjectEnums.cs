using UnityEngine;
using System.Collections;

/// <summary>
/// Enum containing all the game states.
/// </summary>
public enum GameStates { None, PreGame, Gameplay, Pause, EndGame }

/// <summary>
/// Enum containing all the player states.
/// </summary>
public enum PlayerStates { None, Idle, Moving, Dead }

/// <summary>
/// Enum containing all the characters you can be.
/// </summary>
public enum CharacterTypes { She = 0, He = 1 }

/// <summary>
/// Enum containing all teams.
/// </summary>
public enum TeamTypes { Blue = 0, Orange = 1 }

/// <summary>
/// Enum containing all tower states.
/// </summary>
public enum TowerStates { Idle, Attacking, Dead }

/// <summary>
/// Enum containing all player stats.
/// </summary>
public enum PlayerStats { Atk, MovSpd, HP, Def, FireRate }

/// <summary>
/// Enum containing screen touch sides.
/// </summary>
public enum TouchSides { Left = 0, Right = 1 }

/// <summary>
/// Enum contaning all scenes names.
/// </summary>
public enum Scenes { MainMenu, ModeSelect, Loading, MatchRoom, Profile, Landing, Store, Tutorial }

/// <summary>
/// Enum containing all gun types.
/// </summary>
public enum GunTypes { None, Default, Alt1, Alt2, Alt3, Alt4 }

/// <summary>
/// Enum containing all states from characters AI.
/// </summary>
public enum PlayerAIStates { None, Moving, MovingToPanel, OcuppyingPanel, AttackingPlayer, AttackingTower, Fleeing }