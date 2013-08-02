using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerGame : Manager {
	public string gameName;
	public bool developBuild;
	
	public int level;
	
	public Camera main;
	public Camera gUI3D;
	public Camera mapGUI;
	
	public void Start () {
		level = 1;	
	}
	
	public override void ManagerStart () {
	}
	
	public override void ManagerWorking () {
		if (!managerPlayer.playerSetup) {
			factoryCharacter.SetupPlayer (level);
			managerPlayer.playerSetup = true;
		}
		
		if (managerPlayer.ship == null) {
			factoryCharacter.SpawnPlayerShip ();
		}
		
		if (managerMap.managerMapState == ManagerMapState.Waiting) {
			if (managerMap.universe == null) {
				UniverseEnterance ();
			}
			
			if (managerMap.universe != null && !managerPlayer.shipSetup) {
				managerPlayer.PositionShip ();
				managerInterface.SetInterface (interfaceSolar);
				return;
			}
			
			if (managerMap.currentMission != null && managerPlayer.currentBody == null) {
				RectRoom startingRoom = managerMap.currentMission.compressedMap.StartingRoom ();
				Vector3 spawnPosition = new Vector3 (startingRoom.left, 0, startingRoom.top) * CompressedMap.COMPRESSION_RATIO * CompressedMap.TILE_SIZE;
				factoryCharacter.SpawnPlayerCharacter (spawnPosition);
				managerInterface.SetInterface (interfaceTDS);
				return;
			}
		}
	}
	
	public void UniverseEnterance () {
		managerMap.GenerateUniverse ();
	}
	public void UniverseExit () {
		level++;
		managerMap.UngenerateUniverse ();
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
		managerCharacter.PlayerCleanUp (level);
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