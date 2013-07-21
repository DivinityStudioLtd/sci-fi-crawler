using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapMission : Map {
	public void Start () {
		managerMap.missions.Add (this);
	}
	
	public int level;
	
	public CompressedMap compressedMap;
	
	public SolarBody solarBody;
	public Mission mission;
	public List<Challenge> challenges;
	public List<Reward> rewards;
	public MissionType missionType;
	
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
}