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
	public Mission mission;
	public List<Challenge> challenges;
	public List<Reward> rewards;
	public MissionType missionType;
	
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
	
	public void Update () {
		warning.transform.position = solarBody.transform.position;
	}
}