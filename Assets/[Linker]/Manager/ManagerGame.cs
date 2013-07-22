using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerGame : Manager {
	public string gameName;
	
	public override void ManagerStart () {
		managerInterface.SetInterface (interfaceLogin);
		base.ManagerStart ();
	}
	
	public override void ManagerWorking () {
		if (managerMap.managerMapState == ManagerMapState.Waiting && !managerPlayer.setup) {
			if (managerMap.universe == null) 
				managerMap.GenerateUniverse ();
			
			if (managerMap.universe != null && managerPlayer.ship == null) {
				factoryCharacter.SpawnPlayerShip ();
				managerInterface.SetInterface (interfaceSolar);
			}
			if (managerPlayer.ship != null && !managerPlayer.setup) {
				factoryCharacter.SetupPlayer ();
				managerPlayer.setup = true;
			}
		}
	}
	
	public void UniverseToMission () {
		/*
		RectRoom startingRoom = managerMap.currentMission.compressedMap.StartingRoom ();
		Vector3 spawnPosition = new Vector3 (startingRoom.left, 0, startingRoom.top) * CompressedMap.COMPRESSION_RATIO * CompressedMap.TILE_SIZE;
		factoryCharacter.SpawnPlayerCharacter (spawnPosition);
		managerInterface.SetInterface (interfaceTDS);*/
		//factoryCharacter.SpawnCharacter (
		//	managerPrefab.enemies (1) [Random.Range (0, managerPrefab.enemies (1).Count)], 
		//	spawnPosition + new Vector3 (CompressedMap.TILE_SIZE, 0.0f, 0.0f)
		//	);
	}
	
	public void MissionToUniverse () {
		
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