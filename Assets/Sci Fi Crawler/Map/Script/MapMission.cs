using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapMission : Map {
	public void Start () {
		managerMap.missions.Add (this);
		SetParent (managerMap.transform, false);
	}
	
	public int level;
	
	public CompressedMap compressedMap;
	
	public SolarBody solarBody;
	public List<Challenge> challenges;
	public List<Reward> rewards;
	public MissionType missionType;
	public Mission mission;
	
	public int credits;
	
	public GameObject warning;
	
	public int width {
		get {
			return CompressedMap.COMPRESSION_RATIO * compressedMap.compressedX; 	
		}
	}
	
	public int height {
		get {
			return CompressedMap.COMPRESSION_RATIO * compressedMap.compressedY;	
		}
	}
	
	public static Vector3 RandomPositionInRoom (RectRoom rr) {
		float scale = CompressedMap.COMPRESSION_RATIO * CompressedMap.TILE_SIZE;
		float tileOffset = CompressedMap.TILE_SIZE;
		Vector3 spawnPosition = new Vector3 (Random.Range ((rr.left * scale) - tileOffset, ((rr.right - 1) * scale) + tileOffset), 0, Random.Range ((rr.top * scale) - tileOffset, ((rr.bottom - 1) * scale) + tileOffset));
		return spawnPosition;
	}
	
	public void Update () {
		if (solarBody != null)
			warning.transform.position = solarBody.transform.position;
	}
}