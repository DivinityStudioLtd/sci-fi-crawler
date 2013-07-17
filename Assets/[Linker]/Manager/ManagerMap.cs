using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerMap : Manager {
	public MapUniverse universe;
	public MapMission currentMission;
	public List<MapMission> missions;
	public ManagerMapState managerMapState;
	
	public int ungeneratedMissions;
	
	public override void ManagerStart () {
		managerMapState = ManagerMapState.Waiting;
		base.ManagerStart ();	
	}
	
	public override void ManagerWorking () {
		switch (managerMapState) {
		#region ManagerWorking Universe Map Generation
		case ManagerMapState.SpawnUniverse:
			factoryMap.SpawnUniverse ();
			if (universe != null)
				managerMapState = ManagerMapState.GenerateLocations;
			break;
		case ManagerMapState.GenerateLocations:
			managerMapState = ManagerMapState.SpawnLocations;
			break;
		case ManagerMapState.SpawnLocations:
			managerMapState = ManagerMapState.GeneratePlaceHolderMissions;
			break;
		case ManagerMapState.GeneratePlaceHolderMissions:
			if (missions.Count == 0)
				GeneratePlaceHolderMissions (1);//(Random.Range (5,10));
			if (ungeneratedMissions > 0) {
				factoryMap.GeneratePlaceHolderMission ();
				ungeneratedMissions--;
			} else {
				managerMapState = ManagerMapState.Waiting;
			}
			break;
		#endregion
		
		#region ManagerWorking Mission Map Generation
		case ManagerMapState.RoomRects:
			factoryMap.RoomRects (currentMission);
			managerMapState = ManagerMapState.RoomRectsToCompressed;
			break;
		case ManagerMapState.RoomRectsToCompressed:
			managerMapState = ManagerMapState.HallwaysFromRoomRects;
			break;
		case ManagerMapState.HallwaysFromRoomRects:
			managerMapState = ManagerMapState.GenerateMissionFromMissionType;
			break;
		case ManagerMapState.GenerateMissionFromMissionType:
			managerMapState = ManagerMapState.GenerateRewards;
			break;
		case ManagerMapState.GenerateRewards:
			managerMapState = ManagerMapState.GenerateChallenges;
			break;
		case ManagerMapState.GenerateChallenges:
			managerMapState = ManagerMapState.GenerateRoaming;
			break;
		case ManagerMapState.GenerateRoaming:
			managerMapState = ManagerMapState.SpawnTiles;
			break;
		case ManagerMapState.SpawnTiles:
			managerMapState = ManagerMapState.SpawnMission;
			break;
		case ManagerMapState.SpawnMission:
			managerMapState = ManagerMapState.SpawnRewards;
			break;
		case ManagerMapState.SpawnRewards:
			managerMapState = ManagerMapState.SpawnChallenges;
			break;
		case ManagerMapState.SpawnChallenges:
			managerMapState = ManagerMapState.SpawnRoaming;
			break;
		case ManagerMapState.SpawnRoaming:
			managerMapState = ManagerMapState.Waiting;
			break;	
		#endregion
		}
	}
	
	public void GenerateUniverse () {
		managerMapState = ManagerMapState.SpawnUniverse;
	}
	
	public void GeneratePlaceHolderMissions (int missionToBeGenerated) {
		ungeneratedMissions = missionToBeGenerated;
		managerMapState = ManagerMapState.GeneratePlaceHolderMissions;
	}
	
	public void SpawnMapMission (MapMission mission) {
		currentMission = mission;
		missions.Remove (currentMission);
		managerMapState = ManagerMapState.RoomRects;
	}
	
	public void UnspawnCurrentMapMission () {
		
	}
}

public enum ManagerMapState {
	None,
	Waiting,
	#region ManagerMapState Universe Map Generation
	SpawnUniverse,
	GenerateLocations,
	SpawnLocations,
	GeneratePlaceHolderMissions,
	#endregion
	
	#region ManagerMapState Mission Map Generation
	RoomRects,
	RoomRectsToCompressed,
	HallwaysFromRoomRects,
	GenerateMissionFromMissionType,
	GenerateRewards,
	GenerateChallenges,
	GenerateRoaming,
	SpawnTiles,
	SpawnMission,
	SpawnRewards,
	SpawnChallenges,
	SpawnRoaming
	#endregion
}