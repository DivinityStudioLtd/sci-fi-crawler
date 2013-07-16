using UnityEngine;
using System.Collections;

public class ManagerGame : Manager {
	public string gameName;
	
	public override void ManagerStart () {
		managerInterface.SetInterface (interfaceLogin);
		base.ManagerStart ();
	}
	
	public override void ManagerWorking () {
		if (managerMap.managerMapState == ManagerMapState.Waiting && managerMap.universe == null) {
			managerMap.GenerateUniverse ();
		}
		if (managerMap.managerMapState == ManagerMapState.Waiting && managerMap.universe != null) {
			managerMap.SpawnMapMission (managerMap.missions [0]);
			base.ManagerWorking ();
		}
		/*
		if (managerPlayer.controller == null) {
			factoryCharacter.SpawnPlayerCharacter ();
			managerInterface.SetInterface (interfaceTDS);
		}*/
	}
}
public enum ManagerGameState {
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