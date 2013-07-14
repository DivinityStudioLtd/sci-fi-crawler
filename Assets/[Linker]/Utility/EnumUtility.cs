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

public enum GameManagerSyncState {
	StartingUp,
	MapGenerating,
	MapGenerated,
	MapSpawned,
	MapSpawning,
	ActorsSpawning,
	ActorsSpawned,
	PreGameCharacterSpawning,
	PreGameCharacterSpawned,
	PreGame,
	PreGameTransitionGame,
	Game,
	GameTransitionPostGame,
	PostGame
}

public enum AttributeEnum {
	Base,
	Derived,
	Pool,
	Skill
}

public enum InterfaceEnum {
	None,
	Developer,
	Dialogue,
	Equipment,
	FPS,
	Goal,
	Help,
	MainMenu,
	Spectator,
	SplashScreen
	
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
	Movement, 
	Defend, 
	Flee, 
	Avoid, 
	FleetMovement
}

public enum TileEnum {
	Room,
	Hallway,
	Closed
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

public enum EntertainerFactoryState {
	Idle,
	SetupGeneration,
	DoorsGenerating,
	DoorsGenerated,
	PropsGenerating,
	PropsGenerated
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