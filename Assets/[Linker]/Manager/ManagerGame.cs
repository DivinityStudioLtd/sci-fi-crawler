using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerGame : Manager {
	public string gameName;
	public bool developBuild;
	
	public Camera main;
	public Camera gUI3D;
	public Camera mapGUI;
	
	public override void ManagerStart () {
		managerInterface.SetInterface (interfaceLogin);
		base.ManagerStart ();
	}
	
	public override void ManagerWorking () {
		if (managerMap.managerMapState == ManagerMapState.Waiting) {
			if (!managerPlayer.setup) {
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
			
			if (managerMap.currentMission != null && managerPlayer.currentBody == null) {
				RectRoom startingRoom = managerMap.currentMission.compressedMap.StartingRoom ();
				Vector3 spawnPosition = new Vector3 (startingRoom.left, 0, startingRoom.top) * CompressedMap.COMPRESSION_RATIO * CompressedMap.TILE_SIZE;
				factoryCharacter.SpawnPlayerCharacter (spawnPosition);
				managerInterface.SetInterface (interfaceTDS);
			}
		}
	}
	
	public void UniverseToMission (MapMission mission) {
		managerMap.SpawnMapMission (mission);
		InterfaceUtility.SetCameraToTransform (null, true);
		Camera.main.farClipPlane = 0.02f;
		managerMap.universe.transform.position = new Vector3 (0, 10000, 0);
	}
	
	public void MissionToUniverse () {
		if (managerMap.currentMission.mission.Completed ()) {
			managerPlayer.playerInventory.credit += managerMap.currentMission.credits;
			//managerMap.GeneratePlaceHolderMissions (2);	
		} else {
			//managerMap.GeneratePlaceHolderMissions (1);	
		}
		managerMap.universe.shop.Shuffle ();
		managerMap.UnspawnCurrentMapMission ();
		managerMap.universe.transform.position = new Vector3 (0, 0, 0);
		managerCharacter.DestoryNonPlayer ();
		managerCharacter.PlayerCleanUp ();
		managerInterface.SetInterface (interfaceSolar);
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