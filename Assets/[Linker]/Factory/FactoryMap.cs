using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*Modjo
 */
public class FactoryMap : Factory {
	#region Universe Map Generation
	public void SpawnUniverse () {
		Instantiate (Resources.Load ("Map/Universe") as GameObject, Vector3.zero, Quaternion.identity);
	}
	
	public void GeneratePlaceHolderMission () {
		MapMission mm = (Instantiate (Resources.Load ("Map/Mission") as GameObject, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<MapMission> ();
		mm.missionType = (MissionType) Random.Range (0, (int) MissionType.Capture);
	}
	#endregion
	
	#region Mission Map Generation
	
	public void RoomRects (MapMission mission) {
		mission.compressedMap.rectRooms.Add (new RectRoom (1,1,1,1));
		if (mission.compressedMap.rectRooms [0].Collides (new RectRoom (-1,-1,1,1), 1))
			print ("WIN");
	} 
	
	public void RoomRectsToCompressed (MapMission mission) {
		
	}
	
	public void HallwaysFromRoomRects (MapMission mission) {
		
	}
	
	public void GenerateMissionFromMissionType (MapMission mission) {
		
	}
	
	public void GenerateRewards (MapMission mission) {
		
	}
	
	public void GenerateChallenges (MapMission mission) {
		
	}
	
	public void GenerateRoaming (MapMission mission) {
		
	}
	
	public void SpawnTiles (MapMission mission) {
		
	}
	
	public void SpawnMission (MapMission mission) {
		
	}
	
	public void SpawnRewards (MapMission mission) {
		
	}
	
	public void SpawnChallenges (MapMission mission) {
		
	}
	
	public void SpawnRoaming (MapMission mission) {
		
	}
	#endregion
}

[System.Serializable]
public class CompressedMap {
	public List<RectRoom> rectRooms;
	public TileTpe[,] compressedMap;
	public int compressedX;
	public int compressedY;
	public int ratio;
}

[System.Serializable]
public class RectRoom {
	public int left;
	public int top;
	public int width;
	public int height;
	public int right {
		get {
			return left + width;
		}
		set {
			left = value - width;	
		}
	}
	public int bottom {
		get {
			return top + height;
		}
		set {
			top = value - height;	
		}
	}
	
	public bool Collides (RectRoom otherRoom, int border = 0) {
		int newLeft = left - border;
		int newRight = right + border;
		int newTop = top - border;
		int newBottom = bottom - border;
		if ((otherRoom.left <= newLeft && otherRoom.right <= newLeft) || (otherRoom.left >= newRight && otherRoom.right >= newRight))
			if ((otherRoom.top <= newTop && otherRoom.bottom <= newTop) || (otherRoom.top >= newBottom && otherRoom.bottom >= newBottom))
				return true;
		return false;
	}
	
	public RectRoom (int newLeft, int newTop, int newWidth, int newHeight) {
		left = newLeft;
		top = newTop;
		width = newWidth;
		height = newHeight;	
	}
}

[System.Serializable]
public class Mission {
	public List<GameObject> enemies;
	public int room;
}

[System.Serializable]
public class Challenge {
	public List<GameObject> enemies;
	public List<GameObject> environment;
	public int room;
}

[System.Serializable]
public class Reward {
	public List<Power> powers;
	public List<Firearm> firearms;
	public int credits;
	public int room;
}

public enum MissionType {
	Destroy,
	Steal,
	Recover,
	Assualt,
	Capture
}