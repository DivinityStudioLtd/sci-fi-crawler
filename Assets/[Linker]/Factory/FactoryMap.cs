using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*Modjo
 */
public class FactoryMap : Factory {
	public void SpawnUniverse () {
		Instantiate (Resources.Load ("Map/Universe") as GameObject, Vector3.zero, Quaternion.identity);
	}
	
	public void GeneratePlaceHolderMission () {
		MapMission mm = (Instantiate (Resources.Load ("Map/Mission") as GameObject, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<MapMission> ();
		mm.missionType = (MissionType) Random.Range (0, (int) MissionType.Capture);
	}
	
	public void GenerateMapMission (MapMission mission) {
		
	} 
	
	public void SpawnMapMission (MapMission mission) {
		
	}
	
	public void UnspawnMapMission (MapMission mission) {
		
	}
}

[System.Serializable]
public class CompressedMap {
	public List<Rect> rectRooms;
	public TileTpe[,] compressedMap;
	public int compressedX;
	public int compressedY;
	public int ratio;
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