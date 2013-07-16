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
				GeneratePlaceHolderMissions (Random.Range (5,10));
			if (ungeneratedMissions > 0) {
				factoryMap.GeneratePlaceHolderMission ();
				ungeneratedMissions--;
			} else {
				managerMapState = ManagerMapState.Waiting;
			}
			break;
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
	}
	
	public void UnspawnCurrentMapMission () {
		
	}
}

public enum ManagerMapState {
	None,
	Waiting,
	#region Universe Map Generation
	SpawnUniverse,
	GenerateLocations,
	SpawnLocations,
	GeneratePlaceHolderMissions,
	#endregion
	
	#region Mission Map Generation
	RoomRects,
	RoomRectsToCompressed,
	HallwaysFromRoomRects,
	MissionDetailsFromMissionType
	#endregion
}