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
		mm.generated = false;
		mm.level = 3;
	}
	#endregion
	
	#region Mission Map Generation
	static int ROOM_BORDER = 1;
	public void GenerateRectRooms (MapMission mission) { // completed
		mission.compressedMap.compressedX = 10;
		mission.compressedMap.compressedY = 10;
		mission.compressedMap.compressedTiles = new TileType[mission.compressedMap.compressedX, mission.compressedMap.compressedY];
		
		int rectGenerationUnits = ((mission.compressedMap.compressedX - 2) / 2) * ((mission.compressedMap.compressedY - 2) / 2) / 4;
		
		int tier1 = 0;
		int tier2 = 0;
		int tier3 = 0;
		for (int i = 0; i < rectGenerationUnits; i++) {
			switch (Random.Range (0,2)) {
			case 0:
				tier3++;
				break;
			case 1:
				tier2++;
				tier1++;
				tier1++;
				break;
			}
		}
		
		int totalTries = 100;
		while (totalTries > 0) {
			if (tier3 > 0) {
				if (AddRectRoom (new RectRoom (Random.Range (1, mission.compressedMap.compressedX - 2), Random.Range (1, mission.compressedMap.compressedY - 2), 2, 2), mission))
					tier3--;
				totalTries--;
			} else if (tier2 > 0) {
				int ran = Random.Range (0, 2);
				int x = ran == 0? 1:2;
				int y = ran == 0? 2:1;
				if (AddRectRoom (new RectRoom (Random.Range (1, mission.compressedMap.compressedX - x), Random.Range (1, mission.compressedMap.compressedY - y), x, y), mission))
					tier2--;
				totalTries--;
			} else if (tier1 > 0) {
				if (AddRectRoom (new RectRoom (Random.Range (1, mission.compressedMap.compressedX - 1), Random.Range (1, mission.compressedMap.compressedY - 1), 1, 1), mission))
					tier1--;
				totalTries--;
			} else {
				break;	
			}
		}
		
		mission.compressedMap.rectRooms [mission.compressedMap.rectRooms.Count - 1].startingRoom = true;
	} 
			
	bool AddRectRoom (RectRoom n_rr, MapMission mm) {
		foreach (RectRoom rr in mm.compressedMap.rectRooms)
			if (rr.Collides (n_rr, ROOM_BORDER))
				return false;
		mm.compressedMap.rectRooms.Add (n_rr);
		return true;
	}
	
	public void HallwaysFromRectRooms (MapMission mission) {
		if (mission.compressedMap.rectRooms.Count > 2)
			foreach (RectRoom rr in mission.compressedMap.rectRooms) {
				//get a unique other room
				RectRoom o_rr;
				do {
					o_rr = mission.compressedMap.rectRooms [Random.Range (0, mission.compressedMap.rectRooms.Count)];
				} while (rr == o_rr);
				
				RectRoom startEndPoints;
				
				int startX;
				int startY;
				int endX;
				int endY;
				
				if (rr.left <= o_rr.left) {
					if (rr.top <= o_rr.top) {
						startX = rr.right-1;
						startY = rr.bottom-1;
						endX = o_rr.left;
						endY = o_rr.top;
					} else {
						startX = rr.right-1;
						startY = rr.top;
						endX = o_rr.left;
						endY = o_rr.bottom-1;
					}
				} else {
					if (rr.top <= o_rr.top) {
						startX = rr.left;
						startY = rr.bottom-1;
						endX = o_rr.right-1;
						endY = o_rr.top;
					} else {
						startX = rr.left;
						startY = rr.top;
						endX = o_rr.right-1;
						endY = o_rr.bottom-1;
					}
				}
				mission.compressedMap.compressedTiles [startX,startY] = TileType.Hallway;
				
				while (!(startX == endX && startY == endY)) {
					if (startX != endX && startY != endY) {
						if (Random.Range (0, 2) ==0) {
						if (startX > endX)
							startX--;
						else
							startX++;
							
						} else {
						if (startY > endY)
							startY--;
						else
							startY++;
							
						}
						
					} else if (startX != endX) {
						if (startX > endX)
							startX--;
						else
							startX++;
					} else if (startY != endY) {
						if (startY > endY)
							startY--;
						else
							startY++;
					}
				mission.compressedMap.compressedTiles [startX,startY] = TileType.Hallway;
				}
			}
	}
	
	public void RectRoomsToCompressed (MapMission mission) {
		foreach (RectRoom rr in mission.compressedMap.rectRooms)
			for (int i = rr.left; i < rr.right; i++)
				for (int j = rr.top; j < rr.bottom; j++)
					mission.compressedMap.compressedTiles [i,j] = TileType.Room;
	}
	
	public void GenerateMissionFromMissionType (MapMission mission) {
		
	}
	
	public void GenerateChallenges (MapMission mission) {
		foreach (RectRoom rr in mission.compressedMap.rectRooms) {
			if (rr.startingRoom)
				continue;
			Challenge c = GenerateChallenge (rr.width * rr.height * mission.level);
			c.room = rr;
			mission.challenges.Add (c);
		}
	}
	
	public Challenge GenerateChallenge (int difficulty) {
		Challenge c = new Challenge ();
		//c.difficulty = difficulty;
		
		int level1 = 0;
		int level2 = 0;
		int level3 = 0;
		
		while (difficulty > 0) {
			int ran = (int) (100.0f * Random.Range (0.0f,1.0f));
			
			if (difficulty >= 4) {
				if (ran == Mathf.Clamp (ran, 63, 100)) {
					level3++;
					difficulty -= 4;
				} else if (ran == Mathf.Clamp (ran, 43, 62)) {
					level2++;
					difficulty -= 2;
				} else {
					level1++;
					difficulty -= 1;
				}
			} else if (difficulty >= 2) {
				if (ran == Mathf.Clamp (ran, 65, 100)) {
					level2++;
					difficulty -= 2;
				} else {
					level1++;
					difficulty -= 1;
				}
			} else {
				level1++;
				difficulty -= 1;
			}
		}
		
		for (int i = 0; i < level3; i++)
			c.enemies.Add (managerPrefab.enemies (3) [Random.Range (0, managerPrefab.enemies (3).Count)]);
		for (int i = 0; i < level2; i++)
			c.enemies.Add (managerPrefab.enemies (2) [Random.Range (0, managerPrefab.enemies (2).Count)]);
		for (int i = 0; i < level1; i++)
			c.enemies.Add (managerPrefab.enemies (1) [Random.Range (0, managerPrefab.enemies (1).Count)]);
		return c;
	}
	
	public void GenerateRewards (MapMission mission) {
		foreach (RectRoom rr in mission.compressedMap.rectRooms) {
			if (rr.startingRoom)
				continue;
			Reward r = GenerateReward (rr.width * rr.height + mission.level);
			r.room = rr;
			mission.rewards.Add (r);
		}
	}
	
	public Reward GenerateReward (int points) {
		ItemBucket ib = new ItemBucket ();
		int bodiesToAdd = 0;
		int firearmsToAdd = 0;
		int powersToAdd = 0;
		int creditsToAdd = 0;
		
		while (points > 0) {
			int ran = (int) (100.0f * Random.Range (0.0f,1.0f));
			//print (ran + " " + points);
			if (points >= 8) {
				if (ran == Mathf.Clamp (ran, 90, 100)) {
					bodiesToAdd++;
					points -= 8;
				} else if (ran == Mathf.Clamp (ran, 70, 89)) {
					firearmsToAdd++;
					points -= 4;
				} else if (ran == Mathf.Clamp (ran, 40, 69)) {
					powersToAdd++;
					points -= 2;
				} else {
					creditsToAdd++;
					points -= 1;
				}
			} else if (points >= 4) {
				if (ran == Mathf.Clamp (ran, 73, 100)) {
					firearmsToAdd++;
					points -= 4;
				} else if (ran == Mathf.Clamp (ran, 43, 72)) {
					powersToAdd++;
					points -= 2;
				} else {
					creditsToAdd++;
					points -= 1;
				}
			} else if (points >= 2) {
				if (ran == Mathf.Clamp (ran, 55, 100)) {
					powersToAdd++;
					points -= 2;
				} else {
					creditsToAdd++;
					points -= 1;
				}
			} else {
				creditsToAdd++;
				points -= 1;
			}
		}
		//print (bodiesToAdd + " " + firearmsToAdd + " " + powersToAdd + " " + creditsToAdd);
		for (int i = 0; i < bodiesToAdd; i++)
			ib.bodies.Add (managerPrefab.bodies[Random.Range (0,managerPrefab.bodies.Count)]);
		for (int i = 0; i < firearmsToAdd; i++)
			ib.firearms.Add (managerPrefab.firearms[Random.Range (0,managerPrefab.firearms.Count)]);
		for (int i = 0; i < powersToAdd; i++)
			ib.powers.Add (managerPrefab.powers[Random.Range (0,managerPrefab.powers.Count)]);
		for (int i = 0; i < creditsToAdd; i++)
			ib.credit += Random.Range (2000,4000);
		
		Reward r = new Reward ();
		r.reward = ib;
		return r;
	}

	public void GenerateRoaming (MapMission mission) {
		
	}
	
	int currentCompressedX;
	int currentCompressedY;
	public GameObject wall;
	public GameObject open;
	public GameObject door;
	public bool SpawnTiles (MapMission mission) {
		if (mission.generated == false) {
			currentCompressedX = 0;
			currentCompressedY = 0;
			mission.generated = true;
			wall = Resources.Load ("Tile/Temp Wall") as GameObject;
			open = Resources.Load ("Tile/Temp Open") as GameObject;
			door = Resources.Load ("Tile/Temp Door") as GameObject;
		} else {
			SpawnTile (mission.compressedMap, currentCompressedX, currentCompressedY);
			currentCompressedX++;
			if (currentCompressedX >= mission.compressedMap.compressedX) {
				currentCompressedX = 0;
				currentCompressedY++;
			}
			if (currentCompressedY >= mission.compressedMap.compressedY)
				return true;
		}
		return false;
	}
	public GameObject prefab;
	void SpawnTile (CompressedMap cm, int x, int y) {
		MapTileSurrounding mts = GetSurrounding (cm, currentCompressedX, currentCompressedY);
		int worldX = x * CompressedMap.TILE_SIZE * CompressedMap.COMPRESSION_RATIO;
		int worldY = y * CompressedMap.TILE_SIZE * CompressedMap.COMPRESSION_RATIO;
		
		switch (cm.compressedTiles [x,y]) {
			case TileType.Room :
				for (int i = -1; i <= 1; i++)
					for (int j = -1; j <= 1; j++) 
						Instantiate (open, new Vector3 (worldX + (i * CompressedMap.TILE_SIZE), 0.0f, worldY + (j * CompressedMap.TILE_SIZE)), Quaternion.identity);
				break;
			case TileType.Hallway :
				Instantiate (open, new Vector3 (worldX, 0.0f, worldY), Quaternion.identity);
				GameObject temp;
				if (mts.above == TileType.Hallway)
					Instantiate (open, new Vector3 (worldX, 0.0f, worldY - CompressedMap.TILE_SIZE), Quaternion.identity);
				else if (mts.above == TileType.Room)
					Instantiate (door, new Vector3 (worldX, 0.0f, worldY - CompressedMap.TILE_SIZE), Quaternion.identity);
				else 
					Instantiate (wall, new Vector3 (worldX, 0.0f, worldY - CompressedMap.TILE_SIZE), Quaternion.identity);
			
				if (mts.below == TileType.Hallway)
					Instantiate (open, new Vector3 (worldX, 0.0f, worldY + CompressedMap.TILE_SIZE), Quaternion.identity);
				else if (mts.below == TileType.Room)
					Instantiate (door, new Vector3 (worldX, 0.0f, worldY + CompressedMap.TILE_SIZE), Quaternion.identity);
				else 
					Instantiate (wall, new Vector3 (worldX, 0.0f, worldY + CompressedMap.TILE_SIZE), Quaternion.identity);
			
				if (mts.left == TileType.Hallway)
					Instantiate (open, new Vector3 (worldX - CompressedMap.TILE_SIZE, 0.0f, worldY), Quaternion.identity);
				else if (mts.left == TileType.Room)
					Instantiate (door, new Vector3 (worldX - CompressedMap.TILE_SIZE, 0.0f, worldY), Quaternion.Euler (new Vector3 (0.0f, 90.0f, 0.0f)));
				else 
					Instantiate (wall, new Vector3 (worldX - CompressedMap.TILE_SIZE, 0.0f, worldY), Quaternion.identity);
			
				if (mts.right == TileType.Hallway)
					Instantiate (open, new Vector3 (worldX + CompressedMap.TILE_SIZE, 0.0f, worldY), Quaternion.identity);
				else if (mts.right == TileType.Room)
					Instantiate (door, new Vector3 (worldX + CompressedMap.TILE_SIZE, 0.0f, worldY), Quaternion.Euler (new Vector3 (0.0f, 90.0f, 0.0f)));
				else 
					Instantiate (wall, new Vector3 (worldX + CompressedMap.TILE_SIZE, 0.0f, worldY), Quaternion.identity);
				
				Instantiate (wall, new Vector3 (worldX - CompressedMap.TILE_SIZE, 0.0f, worldY - CompressedMap.TILE_SIZE), Quaternion.identity);
				Instantiate (wall, new Vector3 (worldX - CompressedMap.TILE_SIZE, 0.0f, worldY + CompressedMap.TILE_SIZE), Quaternion.identity);
				Instantiate (wall, new Vector3 (worldX + CompressedMap.TILE_SIZE, 0.0f, worldY - CompressedMap.TILE_SIZE), Quaternion.identity);
				Instantiate (wall, new Vector3 (worldX + CompressedMap.TILE_SIZE, 0.0f, worldY + CompressedMap.TILE_SIZE), Quaternion.identity);
			
				break;
			
			case TileType.None :
				if (mts.left == TileType.Room || mts.above == TileType.Room || mts.leftAbove == TileType.Room)
					Instantiate (wall, new Vector3 (worldX - CompressedMap.TILE_SIZE, 0.0f, worldY - CompressedMap.TILE_SIZE), Quaternion.identity);
				if (mts.left == TileType.Room || mts.below == TileType.Room || mts.leftBelow == TileType.Room)
					Instantiate (wall, new Vector3 (worldX - CompressedMap.TILE_SIZE, 0.0f, worldY + CompressedMap.TILE_SIZE), Quaternion.identity);
				if (mts.right == TileType.Room || mts.above == TileType.Room || mts.rightAbove == TileType.Room)
					Instantiate (wall, new Vector3 (worldX + CompressedMap.TILE_SIZE, 0.0f, worldY - CompressedMap.TILE_SIZE), Quaternion.identity);
				if (mts.right == TileType.Room || mts.below == TileType.Room || mts.rightBelow == TileType.Room)
					Instantiate (wall, new Vector3 (worldX + CompressedMap.TILE_SIZE, 0.0f, worldY + CompressedMap.TILE_SIZE), Quaternion.identity);
				
			
				if (mts.left == TileType.Room)
					Instantiate (wall, new Vector3 (worldX - CompressedMap.TILE_SIZE, 0.0f, worldY), Quaternion.identity);
				if (mts.right == TileType.Room)
					Instantiate (wall, new Vector3 (worldX + CompressedMap.TILE_SIZE, 0.0f, worldY), Quaternion.identity);
				if (mts.above == TileType.Room)
					Instantiate (wall, new Vector3 (worldX, 0.0f, worldY - CompressedMap.TILE_SIZE), Quaternion.identity);
				if (mts.below == TileType.Room)
					Instantiate (wall, new Vector3 (worldX, 0.0f, worldY + CompressedMap.TILE_SIZE), Quaternion.identity);
				break;
		}
	}
	MapTileSurrounding GetSurrounding (CompressedMap cm, int x, int y) {
		MapTileSurrounding returnMTS = new MapTileSurrounding ();
		returnMTS.left = x > 0 ? cm.compressedTiles [x-1,y] : TileType.None;
		returnMTS.right = x < cm.compressedX - 1 ? cm.compressedTiles [x+1,y] : TileType.None;
		returnMTS.above = y > 0 ? cm.compressedTiles [x,y-1] : TileType.None;
		returnMTS.below = y < cm.compressedY - 1 ? cm.compressedTiles [x,y+1] : TileType.None;
		
		returnMTS.leftAbove = x > 0 && y > 0 ? cm.compressedTiles [x-1,y-1] : TileType.None;
		returnMTS.rightAbove = x < cm.compressedX - 1 && y > 0 ? cm.compressedTiles [x+1,y-1] : TileType.None;
		returnMTS.leftBelow = x > 0 && y < cm.compressedY - 1 ? cm.compressedTiles [x-1,y+1] : TileType.None;
		returnMTS.rightBelow = x < cm.compressedX - 1 && y < cm.compressedY - 1 ? cm.compressedTiles [x+1,y+1] : TileType.None;
		
		return returnMTS;
	}
	
	
	public void SpawnMission (MapMission mission) {
	}
	
	int staggerSpawnCounter = 0;
	public bool SpawnRewards (MapMission mission) {
		RectRoom rr = mission.rewards [staggerSpawnCounter].room;
		
		float scale = CompressedMap.COMPRESSION_RATIO * CompressedMap.TILE_SIZE;
		float tileOffset = CompressedMap.TILE_SIZE;
		Vector3 spawnPosition = new Vector3 (Random.Range ((rr.left * scale) - tileOffset, ((rr.right - 1) * scale) + tileOffset), 0, Random.Range ((rr.top * scale) - tileOffset, ((rr.bottom - 1) * scale) + tileOffset));
		
		GameObject go = Instantiate (managerPrefab.containers [Random.Range (0, managerPrefab.containers.Count)], spawnPosition, Quaternion.identity) as GameObject;
		
		go.GetComponent<Container> ().reward = mission.rewards [staggerSpawnCounter].reward;
		
		staggerSpawnCounter++;
		if (staggerSpawnCounter < mission.rewards.Count)
			return false;
		staggerSpawnCounter = 0;
		return true;
	}
	
	public bool SpawnChallenges (MapMission mission) {
		RectRoom rr = mission.challenges [staggerSpawnCounter].room;
		foreach (GameObject go in mission.challenges [staggerSpawnCounter].enemies) {
			float scale = CompressedMap.COMPRESSION_RATIO * CompressedMap.TILE_SIZE;
			float tileOffset = CompressedMap.TILE_SIZE;
			Vector3 spawnPosition = new Vector3 (Random.Range ((rr.left * scale) - tileOffset, ((rr.right - 1) * scale) + tileOffset), 0, Random.Range ((rr.top * scale) - tileOffset, ((rr.bottom - 1) * scale) + tileOffset));
			
			factoryCharacter.SpawnCharacter (go, spawnPosition);
		}
		staggerSpawnCounter++;
		if (staggerSpawnCounter < mission.challenges.Count)
			return false;
		staggerSpawnCounter = 0;
		return true;
	}
	
	public void SpawnRoaming (MapMission mission) {
		
	}
	#endregion
}

[System.Serializable]
public class CompressedMap {
	public List<RectRoom> rectRooms;
	public TileType[,] compressedTiles;
	public int compressedX;
	public int compressedY;
	static public int TILE_SIZE = 5;
	static public int COMPRESSION_RATIO = 3;
	
	public RectRoom StartingRoom () {
		foreach (RectRoom rr in rectRooms)
			if (rr.startingRoom)
				return rr;
		return null;
	}
}

[System.Serializable]
public class RectRoom {
	public bool startingRoom;
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
	
	public int BorderLeft (int border) {
		return left - border;
	}
	public int BorderRight (int border) {
		return right + border;
	}
	public int BorderTop (int border) {
		return top - border;
	}
	public int BorderBottom (int border) {
		return bottom + border;
	}
	
	public bool Collides (RectRoom otherRoom, int border = 0) {
		if (otherRoom.right <= BorderLeft (border) || 
			otherRoom.left >= BorderRight (border) || 
			otherRoom.bottom <= BorderTop (border) || 
			otherRoom.top >= BorderBottom (border))
				return false;
		return true;
	}
	
	public RectRoom (int newLeft, int newTop, int newWidth, int newHeight) {
		left = newLeft;
		top = newTop;
		width = newWidth;
		height = newHeight;	
		startingRoom = false;
	}
	
	public string ToString () {
		return "left|"+left+" top|"+top+" width|"+width+" height|"+height;
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
	public RectRoom room;
	public int difficulty;
	
	public Challenge () {
		enemies = new List<GameObject> ();
		environment = new List<GameObject> ();
		room = new RectRoom (0,0,1,1);
		difficulty = 0;
	}
}

[System.Serializable]
public class Reward {
	public ItemBucket reward;
	public RectRoom room;
	
	public Reward () {
		reward = new ItemBucket ();
		room = new RectRoom (0,0,1,1);
	}
}

public enum MissionType {
	Destroy,
	Steal,
	Recover,
	Assualt,
	Capture
}

public class MapTileSurrounding {
	public TileType	above;
	public TileType below;
	public TileType left;
	public TileType right;
	public TileType leftAbove;
	public TileType rightAbove;
	public TileType leftBelow;
	public TileType rightBelow;
}