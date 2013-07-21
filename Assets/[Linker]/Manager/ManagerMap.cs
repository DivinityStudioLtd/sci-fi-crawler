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
		case ManagerMapState.SpawnUniverse :
			factoryMap.SpawnUniverse ();
			if (universe != null)
				managerMapState = ManagerMapState.GenerateSpawnSolarBody ;
			break;
		case ManagerMapState.GenerateSpawnSolarBody :
			if (factoryMap.GenerateSpawnSolarBody (universe))
				managerMapState = ManagerMapState.GeneratePlaceHolderMissions;
			break;
		case ManagerMapState.GeneratePlaceHolderMissions :
			if (missions.Count == 0)
				GeneratePlaceHolderMissions (1);//(Random.Range (5,10));
			if (ungeneratedMissions > 0) {
				factoryMap.GeneratePlaceHolderMission (universe);
				ungeneratedMissions--;
			} else {
				managerMapState = ManagerMapState.Waiting;
			}
			break;
		#endregion
		
		#region ManagerWorking Mission Map Generation
		case ManagerMapState.RoomRects :
			factoryMap.GenerateRectRooms (currentMission);
			managerMapState = ManagerMapState.HallwaysFromRectRooms;
			break;
			
		case ManagerMapState.HallwaysFromRectRooms :
			factoryMap.HallwaysFromRectRooms (currentMission); 
			managerMapState = ManagerMapState.RectRoomsToCompressed;
			break;
			
		case ManagerMapState.RectRoomsToCompressed :
			factoryMap.RectRoomsToCompressed (currentMission);
			managerMapState = ManagerMapState.GenerateMissionFromMissionType;
			break;
			
		case ManagerMapState.GenerateMissionFromMissionType :
			factoryMap.GenerateMissionFromMissionType (currentMission);
			managerMapState = ManagerMapState.GenerateChallenges;
			break;
			
		case ManagerMapState.GenerateChallenges :
			factoryMap.GenerateChallenges (currentMission);
			managerMapState = ManagerMapState.GenerateRewards;
			break;
			
		case ManagerMapState.GenerateRewards :
			factoryMap.GenerateRewards (currentMission);
			managerMapState = ManagerMapState.GenerateRoaming;
			break;
		case ManagerMapState.GenerateRoaming :
			managerMapState = ManagerMapState.SpawnTiles;
			break;
			
		case ManagerMapState.SpawnTiles :
			if (factoryMap.SpawnTiles (currentMission))
				managerMapState = ManagerMapState.SpawnMission;
			break;
		case ManagerMapState.SpawnMission :
			managerMapState = ManagerMapState.SpawnRewards;
			break;
		case ManagerMapState.SpawnRewards :
			if (factoryMap.SpawnRewards (currentMission))
				managerMapState = ManagerMapState.SpawnChallenges;
			break;
		case ManagerMapState.SpawnChallenges :
			if (factoryMap.SpawnChallenges (currentMission))
				managerMapState = ManagerMapState.SpawnRoaming;
			break;
		case ManagerMapState.SpawnRoaming :
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
	GenerateSpawnSolarBody,
	GeneratePlaceHolderMissions,
	#endregion
	
	#region ManagerMapState Mission Map Generation
	RoomRects,
	HallwaysFromRectRooms,
	RectRoomsToCompressed,
	
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