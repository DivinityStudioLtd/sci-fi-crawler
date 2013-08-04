using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapMission : Map {
	public void Start () {
		managerMap.missions.Add (this);
		SetParent (managerMap.transform, false);
	}
	
	public TileSet mapTileSet;
	
	public int level;
	
	public CompressedMap compressedMap;
	
	public SolarBody solarBody;
	public List<Challenge> challenges;
	public List<Reward> rewards;
	public MissionType missionType;
	public Mission mission;
	
	public Tile[,] mapTiles;
	
	public int credits;
	
	public GameObject warning3D;
	public GameObject warningMap;
	
	public int width {
		get {
			return CompressedMap.COMPRESSION_RATIO * compressedMap.compressedX + 1; 	
		}
	}
	
	public int height {
		get {
			return CompressedMap.COMPRESSION_RATIO * compressedMap.compressedY + 1;	
		}
	}
	
	public static Vector3 RandomPositionInRoom (RectRoom rr) {
		float scale = CompressedMap.COMPRESSION_RATIO * CompressedMap.TILE_SIZE;
		float tileOffset = CompressedMap.TILE_SIZE;
		Vector3 spawnPosition = new Vector3 (Random.Range ((rr.left * scale) - tileOffset, ((rr.right - 1) * scale) + tileOffset), 0, Random.Range ((rr.top * scale) - tileOffset, ((rr.bottom - 1) * scale) + tileOffset));
		return spawnPosition;
	}
	
	public void Update () {
		if (missionType == MissionType.Boss) {
			warning3D.SetActive (managerPlayer.hasArtifact);
			warningMap.SetActive (managerPlayer.hasArtifact);
		}
		
		if (warning3D != null)
			warning3D.transform.position = solarBody.transform.position;
		if (warningMap != null)
			warningMap.transform.position = solarBody.transform.position;
	}
	
	public string MapSting () {
		string returnString = "";
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				if (mapTiles [i,j] == null)
					returnString += "n";
				else 
					switch (mapTiles [i,j].mapTileType) {
					case MapTileType.Wall:
					returnString += "W";
						break;
					case MapTileType.Floor:
					returnString += "F";
						break;
					case MapTileType.Door:
					returnString += "D";
						break;
					}
			}
			returnString+="\n";
		}
		return returnString;
	}
}

[System.Serializable]
public class TileSet {
	public List<GameObject> walls;
	public List<GameObject> opens;
	public List<GameObject> doors;
	
	public List<Texture2D> openTextures;
	public List<Texture2D> wallTextures;
	
	public GameObject RandomWall () {
		return walls [Random.Range (0, walls.Count)];	
	}
	public GameObject RandomDoor () {
		return doors [Random.Range (0, doors.Count)];	
	}
	public GameObject RandomOpen () {
		return opens [Random.Range (0, opens.Count)];	
	}
	public Texture2D RandomOpenTexture () {
		return openTextures [Random.Range (0, openTextures.Count)];	
	}
	public Texture2D RandomWallTexture () {
		return wallTextures [Random.Range (0, wallTextures.Count)];	
	}
}