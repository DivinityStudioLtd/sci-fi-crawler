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
				GeneratePlaceHolderMissions (10);//(Random.Range (5,10));
			if (ungeneratedMissions > 0) {
				ungeneratedMissions--;
				factoryMap.GeneratePlaceHolderMission (universe);
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
				managerMapState = ManagerMapState.GenerateSpawnMission;
			break;
		case ManagerMapState.GenerateSpawnMission :
			factoryMap.GenerateSpawnMission (currentMission);
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
		mission.solarBody.mapMission = null;
		mission.solarBody = null;
		managerMapState = ManagerMapState.RoomRects;
		Destroy (currentMission.warning3D);
		Destroy (currentMission.warningMap);
	}
	
	public void UnspawnCurrentMapMission () {
		Destroy (currentMission.gameObject);
	}

	public List<Tile> Path (Tile startTile, Tile endTile) {
		for (int i = 0; i < currentMission.width; i++)
			for (int j = 0; j < currentMission.height; j++)
				currentMission.mapTiles [i,j].parent = null;
		
		List<Tile> possibleTiles;
		startTile.pathCost = 0;
		possibleTiles.Add (startTile);
		Tile cheapestTile;
		List<Tile> surroundingTiles;
		while (endTile.parent == null && possibleTiles.Count < 0) {
			cheapestTile = possibleTiles [0];
			for (int i = 1; i < possibleTiles.Count; i++)
				if (possibleTiles [i].pathCost < cheapestTile.pathCost)
					cheapestTile = possibleTiles [i];
			possibleTiles.Remove (cheapestTile);
			
			surroundingTiles.Clear ();
			if (cheapestTile.x < 0)
				if (currentMission.mapTiles [cheapestTile.x - 1, cheapestTile.y] != null)
					surroundingTiles.Add (currentMission.mapTiles [cheapestTile.x - 1, cheapestTile.y]);
			if (cheapestTile.x > currentMission.width - 1)
				if (currentMission.mapTiles [cheapestTile.x + 1, cheapestTile.y] != null)
					surroundingTiles.Add (currentMission.mapTiles [cheapestTile.x + 1, cheapestTile.y]);
			if (cheapestTile.y < 0)
				if (currentMission.mapTiles [cheapestTile.x, cheapestTile.y - 1] != null)
					surroundingTiles.Add (currentMission.mapTiles [cheapestTile.x, cheapestTile.y - 1]);
			if (cheapestTile.y > currentMission.height - 1)
				if (currentMission.mapTiles [cheapestTile.x, cheapestTile.y + 1] != null)
					surroundingTiles.Add (currentMission.mapTiles [cheapestTile.x, cheapestTile.y + 1]);
			
			foreach (Tile t in surroundingTiles)
				if (t.parent == null) {
					t.parent = cheapestTile;
					t.pathCost = cheapestTile.pathCost + 1;
					possibleTiles.Add (t);
				}
		}
		
		List<Tile> path = new List<Tile> ();
		Tile backTrackTile = endTile;
		while (backTrackTile != null) {
			path.Add (backTrackTile);
			backTrackTile = backTrackTile.parent;
		}
		return path;
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
	
	GenerateRewards,
	GenerateChallenges,
	GenerateRoaming,
	
	SpawnTiles,
	GenerateSpawnMission,
	SpawnRewards,
	SpawnChallenges,
	SpawnRoaming
	#endregion
}