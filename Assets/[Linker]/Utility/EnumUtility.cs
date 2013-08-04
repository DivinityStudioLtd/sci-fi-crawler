using UnityEngine;
using System.Collections;

[AddComponentMenu("Utility/EnumUtility")]
/// <summary>
/// Enum Utility.
/// A static class that holds all the enums for the game.
/// </summary>
public class EnumUtility : Utility {
	// look ma, no code
}

public enum ManagerStateEnum {
	None,
	Start,
	Working, 
	Finish
}

public enum MainMenuEnum {
	MainMenu,
	Gameplay,
	MatchMaking,
	Graphic,
	Audio,
	Credit
}

public enum DispositionState {
	Helpful,
	Friendly,
	Indifferent,
	Unfriendly,
	Hostile,
	None
}

public enum GoalStatus {
	InProgress,
	Failure,
	Success,
	Unstarted
}

public enum JukeBoxTrackType {
	None,
	MainMenu,
	Flight,
	Combat,
	GameOver
}

public enum AIState {
	None,
	Idle,
	Wander,
	Attack,
	Chase, 
	Move, 
	Defend, 
	Flee, 
	Avoid, 
	FleetMovement
}

public enum PickupType {
	Health,
	Ammo
}

public enum RotationAxes { 
	MouseXAndY = 0, 
	MouseX = 1, 
	MouseY = 2 
}

public enum MovementState {
	Crouch,
	Run,
	Walk,
	Jog
}
	
public enum AnimationType {
	Animate,
	Blend
}

public enum SlidingAreaState {
	Opening,
	Opened,
	Closing,
	Closed,
	Lowered,
	Raised
}

public enum DamageType {
	Pierce,
	Energy,
	Fire
}

public enum TileType {
	None,
	Hallway,
	Room,
	Door
}

public enum MissionType {
	Destroy,
	Steal,
	Recover,
	Assualt,
	Capture,
	Artifact,
	Boss
}