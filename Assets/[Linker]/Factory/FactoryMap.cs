using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*Modjo
 */
public class FactoryMap : Factory {
	int staggerSpawnCounter = 0;
	
	#region Universe Map Generation
	public void SpawnUniverse (int level) {
		GameObject uni = Instantiate (managerPrefab.universeMap, Vector3.zero, Quaternion.identity) as GameObject;
		MapUniverse universe = uni.GetComponent<MapUniverse> ();
		universe.level = level;
		universe.numberOfSolarBodies = Random.Range (2 + level, 2 + (level * 2));
		RenderSettings.skybox = managerPrefab.skybox [Random.Range (0, managerPrefab.skybox.Count)];
		Shop s = (Instantiate (managerPrefab.shop) as GameObject).GetComponent<Shop> ();
		s.SetParent (uni.transform, true);
	}
	
	static public float PLANET_SPACING = 15.0f;
	public bool GenerateSpawnSolarBody (MapUniverse universe) {
		if (staggerSpawnCounter == 0) {
			GameObject sunGO;
			if (Random.Range (0, 3) == 0)
				sunGO = managerPrefab.suns [1];
			else
				sunGO = managerPrefab.suns [0];
			SolarBody sun = (Instantiate (sunGO) as GameObject).GetComponent<SolarBody> ();
			
			sun.transform.parent = universe.transform;
			sun.transform.localPosition = Vector3.zero;
			sun.transform.localRotation = Quaternion.identity;
			sun.solarBodyType = SolarBodyType.Sun;
		} else {
			PlanetArm pa = (Instantiate (managerPrefab.planetArm) as GameObject).GetComponent<PlanetArm> ();
			pa.SetParent (universe.transform, true);
			pa.edge.localPosition = new Vector3 (0.0f, 0.0f, Random.Range ((PLANET_SPACING * (staggerSpawnCounter+1)) - 1.0f, (PLANET_SPACING * (staggerSpawnCounter+1)) + 1.0f)); 
			
			pa.armRotate.SetRotationTime (staggerSpawnCounter * staggerSpawnCounter * staggerSpawnCounter * Random.Range (10,20)); 
			
			SolarBody planet = (Instantiate (managerPrefab.planet) as GameObject).GetComponent<SolarBody> ();
			planet.SetParent (pa.planetPosition, true);
			pa.planetPosition.GetComponent<Rotate> ().SetRotationTime (Random.Range (1.50f, 2.50f + staggerSpawnCounter));
			planet.mapTileSetName = "Default";
				
			float planetRadius = Random.Range (5.0f, 10.0f);
			planet.transform.localScale = new Vector3 (planetRadius, planetRadius, planetRadius);
			
			PlanetTexture pT = PlanetTexture.General;
			planet.renderer.materials [0].mainTexture = managerPrefab.planetTextures (pT) [Random.Range (0, managerPrefab.planetTextures (pT).Count)];
			planet.solarBodyType = SolarBodyType.Planet;
			
			foreach (Transform sbp in pa.planetSolarBodyPositions) {
				SolarBody sb;
				if (staggerSpawnCounter == universe.numberOfSolarBodies - 1 && pa.planetSolarBodyPositions.IndexOf (sbp) == 0) {
					sb = (Instantiate (managerPrefab.jumpGate) as GameObject).GetComponent<SolarBody> ();
					
					sbp.GetComponent<Rotate> ().SetRotationTime (0.0f);
					sb.solarBodyType = SolarBodyType.JumpGate;
					sb.SetParent (sbp, true);
					sb.mapTileSetName = "Default";
					continue;
				}
				
				if (Random.Range (0, 3) == 0)
					continue;
				
				if (Random.Range (0, 3) == 0) {
					sb = (Instantiate (managerPrefab.planet) as GameObject).GetComponent<SolarBody> ();
					planetRadius = Random.Range (1.5f, 2.5f);
					sb.transform.localScale = new Vector3 (planetRadius, planetRadius, planetRadius);
					pT = PlanetTexture.General;
					sb.renderer.materials [0].mainTexture = managerPrefab.planetTextures (pT) [Random.Range (0, managerPrefab.planetTextures (pT).Count)];
					
					sbp.GetComponent<Rotate> ().SetRotationTime (Random.Range (1.00f, 1.50f));
					sb.solarBodyType = SolarBodyType.Moon;
					sb.mapTileSetName = "Default";
				} else {
					sb = (Instantiate (managerPrefab.stations [Random.Range (0, managerPrefab.stations.Count)]) as GameObject).GetComponent<SolarBody> ();
					
					sbp.GetComponent<Rotate> ().SetRotationTime (0.0f);
					sb.solarBodyType = SolarBodyType.Station;
					sb.mapTileSetName = "Default";
				}
				sb.SetParent (sbp, true);
			}
		}
		staggerSpawnCounter++;
		
		if (staggerSpawnCounter < universe.numberOfSolarBodies)
			return false;
		staggerSpawnCounter = 0;
		return true;
	}
	
	public void GeneratePlaceHolderMission (MapUniverse universe) {
		MapMission mm = (Instantiate (managerPrefab.missionMap, Vector3.zero, Quaternion.identity) as GameObject).GetComponent<MapMission> ();
		mm.missionType = (MissionType) Random.Range (0, 4);
		mm.generated = false;
		mm.level = Random.Range (1, 3);
		
		mm.credits = 0;
		for (int i = 0; i < mm.level; i++)
			mm.credits += Random.Range (1000,2000);
		
		int numberOfTries = 10;
		while (numberOfTries > 0) {
			SolarBody sb = universe.solarbodies [Random.Range (0, universe.solarbodies.Count)];
			if (sb.solarBodyType == SolarBodyType.Sun || sb.solarBodyType == SolarBodyType.JumpGate || sb.mapMission != null) {
				numberOfTries--;
				continue;
			}
			sb.mapMission = mm;
			mm.solarBody = sb;
			break;
		}
		
		if (numberOfTries == 0)
			Destroy (mm.gameObject);
	}
	
	public void GenerateArtifactMissions (MapUniverse universe) {
		MapMission artifact;
		MapMission boss;
		
		artifact = managerMap.missions [Random.Range (0, managerMap.missions.Count)];
		do {
			boss = managerMap.missions [Random.Range (0, managerMap.missions.Count)];
		} while (artifact == boss);
		
		artifact.missionType = MissionType.Artifact;
		boss.missionType = MissionType.Boss;
	}
	#endregion
	
	#region Mission Map Generation
	static int ROOM_BORDER = 1;
	public void GenerateRectRooms (MapMission mission) {
		if (mission.missionType == MissionType.Boss) {
			GenerateBossRectRooms (mission);
		} else {
			GenerateStationRectRooms (mission);	
		}
	} 	
	
	void GenerateSurfaceRectRooms (MapMission mission) {
		
	}
	
	void GenerateStationRectRooms (MapMission mission) {
		int xMap;
		int yMap;
		int spread = Random.Range (0, (mission.level + 1));
		xMap = 1 + spread;
		yMap = 1 + (mission.level - spread);
		mission.compressedMap.compressedX = 2 + (xMap * 4);
		mission.compressedMap.compressedY = 2 + (yMap * 4);
		mission.compressedMap.compressedTiles = new TileType[mission.compressedMap.compressedX, mission.compressedMap.compressedY];
		mission.mapTiles = new Tile [mission.width, mission.height];
		int rectGenerationUnits = ((mission.compressedMap.compressedX - 2) / 2) * ((mission.compressedMap.compressedY - 2) / 2) / 4;
		
		int tier1 = 0;
		int tier2 = 0;
		int tier3 = 0;
		tier2++;
		tier1++;
		tier1++;
		
		for (int i = 1; i < rectGenerationUnits; i++) {
			switch (Random.Range (0,3)) {
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
		
		while (true) {
			if (tier3 > 0) {
				if (AddRectRoom (new RectRoom (Random.Range (1, mission.compressedMap.compressedX - 2), Random.Range (1, mission.compressedMap.compressedY - 2), 2, 2), mission))
					tier3--;
			} else if (tier2 > 0) {
				int ran = Random.Range (0, 2);
				int x = ran == 0? 1:2;
				int y = ran == 0? 2:1;
				if (AddRectRoom (new RectRoom (Random.Range (1, mission.compressedMap.compressedX - x), Random.Range (1, mission.compressedMap.compressedY - y), x, y), mission))
					tier2--;
			} else if (tier1 > 0) {
				if (AddRectRoom (new RectRoom (Random.Range (1, mission.compressedMap.compressedX - 1), Random.Range (1, mission.compressedMap.compressedY - 1), 1, 1), mission))
					tier1--;
			} else {
				break;	
			}
		}
		
		mission.compressedMap.rectRooms [mission.compressedMap.rectRooms.Count - 1].startingRoom = true;
	}
	
	void GenerateBossRectRooms (MapMission mission) {
		mission.compressedMap.compressedX = 6;
		mission.compressedMap.compressedY = 6;
		mission.compressedMap.compressedTiles = new TileType[mission.compressedMap.compressedX, mission.compressedMap.compressedY];
		mission.mapTiles = new Tile [mission.width, mission.height];
		//int rectGenerationUnits = ((mission.compressedMap.compressedX - 2) / 2) * ((mission.compressedMap.compressedY - 2) / 2) / 4;
		
		AddRectRoom (new RectRoom (1, 1, mission.compressedMap.compressedX - 3, mission.compressedMap.compressedY - 3), mission);
		
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
		if (mission.compressedMap.rectRooms.Count > 1)
			foreach (RectRoom rr in mission.compressedMap.rectRooms) {
				//get a unique other room
				RectRoom o_rr;
				do {
					o_rr = mission.compressedMap.rectRooms [Random.Range (0, mission.compressedMap.rectRooms.Count)];
				} while (rr == o_rr);
				
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
							if (startX < endX)
								startX++;
							else
								startX--;
						} else {
							if (startY < endY)
								startY++;
							else
								startY--;
						}
						
					} else if (startX != endX) {
						if (startX < endX)
							startX++;
						else
							startX--;
					} else if (startY != endY) {
						if (startY < endY)
							startY++;
						else
							startY--;
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
	
	public void GenerateChallenges (MapMission mission) {
		foreach (RectRoom rr in mission.compressedMap.rectRooms) {
			if (rr.startingRoom)
				continue;
			Challenge c = GenerateChallenge ((rr.width * mission.level) + (mission.level * rr.height) );
			c.room = rr;
			mission.challenges.Add (c);
		}
	}
	public Challenge GenerateChallenge (int difficulty) {
		Challenge c = new Challenge ();
		
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
			Reward r = GenerateReward (rr.width + rr.height + mission.level);
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
		
		for (int i = 0; i < bodiesToAdd; i++)
			ib.bodies.Add (managerPrefab.bodies[Random.Range (0,managerPrefab.bodies.Count)]);
		for (int i = 0; i < firearmsToAdd; i++)
			ib.firearms.Add (managerPrefab.firearms[Random.Range (0,managerPrefab.firearms.Count)]);
		for (int i = 0; i < powersToAdd; i++)
			ib.powers.Add (managerPrefab.powers[Random.Range (0,managerPrefab.powers.Count)]);
		for (int i = 0; i < creditsToAdd; i++)
			ib.credit += Random.Range (20,40);
		
		Reward r = new Reward ();
		r.reward = ib;
		return r;
	}

	public void GenerateRoaming (MapMission mission) {
		
	}
	
	int currentCompressedX;
	int currentCompressedY;
	public bool SpawnTiles (MapMission mission) {
		if (mission.generated == false) {
			currentCompressedX = 0;
			currentCompressedY = 0;
			mission.generated = true;
			mission.mapTileSet = managerPrefab.mapTileSet (mission.solarBody.mapTileSetName);
		} else {
			SpawnTile (mission.compressedMap, currentCompressedX, currentCompressedY, mission.mapTileSet);
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
	void SpawnTile (CompressedMap cm, int x, int y, TileSet mapTileSet) {
		MapTileSurrounding mts = GetSurrounding (cm, currentCompressedX, currentCompressedY);
		int worldX = x * CompressedMap.TILE_SIZE * CompressedMap.COMPRESSION_RATIO;
		int worldY = y * CompressedMap.TILE_SIZE * CompressedMap.COMPRESSION_RATIO;
		List<Tile> graphicsPainter = new List<Tile> ();
		switch (cm.compressedTiles [x,y]) {
			case TileType.Room :
				for (int i = -1; i <= 1; i++)
					for (int j = -1; j <= 1; j++) 
						graphicsPainter.Add ((Instantiate (mapTileSet.RandomOpen (), new Vector3 (worldX + (i * CompressedMap.TILE_SIZE), 0.0f, worldY + (j * CompressedMap.TILE_SIZE)), Quaternion.identity) as GameObject).GetComponent<Tile> ());
				break;
			case TileType.Hallway :
				graphicsPainter.Add ((Instantiate (mapTileSet.RandomOpen (), new Vector3 (worldX, 0.0f, worldY), Quaternion.identity) as GameObject).GetComponent<Tile> ());
				if (mts.above == TileType.Hallway)
					graphicsPainter.Add ((Instantiate (mapTileSet.RandomOpen (), new Vector3 (worldX, 0.0f, worldY - CompressedMap.TILE_SIZE), Quaternion.identity) as GameObject).GetComponent<Tile> ());
				else if (mts.above == TileType.Room)
					graphicsPainter.Add ((Instantiate (mapTileSet.RandomDoor (), new Vector3 (worldX, 0.0f, worldY - CompressedMap.TILE_SIZE), Quaternion.identity) as GameObject).GetComponent<Tile> ());
				else 
					graphicsPainter.Add ((Instantiate (mapTileSet.RandomWall (), new Vector3 (worldX, 0.0f, worldY - CompressedMap.TILE_SIZE), Quaternion.identity) as GameObject).GetComponent<Tile> ());
			
				if (mts.below == TileType.Hallway)
					graphicsPainter.Add ((Instantiate (mapTileSet.RandomOpen (), new Vector3 (worldX, 0.0f, worldY + CompressedMap.TILE_SIZE), Quaternion.identity) as GameObject).GetComponent<Tile> ());
				else if (mts.below == TileType.Room)
					graphicsPainter.Add ((Instantiate (mapTileSet.RandomDoor (), new Vector3 (worldX, 0.0f, worldY + CompressedMap.TILE_SIZE), Quaternion.identity) as GameObject).GetComponent<Tile> ());
				else 
					graphicsPainter.Add ((Instantiate (mapTileSet.RandomWall (), new Vector3 (worldX, 0.0f, worldY + CompressedMap.TILE_SIZE), Quaternion.identity) as GameObject).GetComponent<Tile> ());
			
				if (mts.left == TileType.Hallway)
					graphicsPainter.Add ((Instantiate (mapTileSet.RandomOpen (), new Vector3 (worldX - CompressedMap.TILE_SIZE, 0.0f, worldY), Quaternion.identity) as GameObject).GetComponent<Tile> ());
				else if (mts.left == TileType.Room) {
					Tile temp = (Instantiate (mapTileSet.RandomDoor (), new Vector3 (worldX - CompressedMap.TILE_SIZE, 0.0f, worldY), Quaternion.identity) as GameObject).GetComponent<Tile> ();
				
					temp.door.transform.rotation = Quaternion.Euler (new Vector3 (0.0f, 90.0f, 0.0f));
					graphicsPainter.Add (temp);
				}
				else 
					graphicsPainter.Add ((Instantiate (mapTileSet.RandomWall (), new Vector3 (worldX - CompressedMap.TILE_SIZE, 0.0f, worldY), Quaternion.identity) as GameObject).GetComponent<Tile> ());
			
				if (mts.right == TileType.Hallway)
					graphicsPainter.Add ((Instantiate (mapTileSet.RandomOpen (), new Vector3 (worldX + CompressedMap.TILE_SIZE, 0.0f, worldY), Quaternion.identity) as GameObject).GetComponent<Tile> ());
				else if (mts.right == TileType.Room) {
					Tile temp = (Instantiate (mapTileSet.RandomDoor (), new Vector3 (worldX + CompressedMap.TILE_SIZE, 0.0f, worldY), Quaternion.identity) as GameObject).GetComponent<Tile> ();
				
					temp.door.transform.rotation = Quaternion.Euler (new Vector3 (0.0f, 90.0f, 0.0f));
					graphicsPainter.Add (temp);
				}
				else 
					graphicsPainter.Add ((Instantiate (mapTileSet.RandomWall (), new Vector3 (worldX + CompressedMap.TILE_SIZE, 0.0f, worldY), Quaternion.identity) as GameObject).GetComponent<Tile> ());
				
				graphicsPainter.Add ((Instantiate (mapTileSet.RandomWall (), new Vector3 (worldX - CompressedMap.TILE_SIZE, 0.0f, worldY - CompressedMap.TILE_SIZE), Quaternion.identity) as GameObject).GetComponent<Tile> ());
				graphicsPainter.Add ((Instantiate (mapTileSet.RandomWall (), new Vector3 (worldX - CompressedMap.TILE_SIZE, 0.0f, worldY + CompressedMap.TILE_SIZE), Quaternion.identity) as GameObject).GetComponent<Tile> ());
				graphicsPainter.Add ((Instantiate (mapTileSet.RandomWall (), new Vector3 (worldX + CompressedMap.TILE_SIZE, 0.0f, worldY - CompressedMap.TILE_SIZE), Quaternion.identity) as GameObject).GetComponent<Tile> ());
				graphicsPainter.Add ((Instantiate (mapTileSet.RandomWall (), new Vector3 (worldX + CompressedMap.TILE_SIZE, 0.0f, worldY + CompressedMap.TILE_SIZE), Quaternion.identity) as GameObject).GetComponent<Tile> ());
			
				break;
			
			case TileType.None :
				if (mts.left == TileType.Room || mts.above == TileType.Room || mts.leftAbove == TileType.Room)
					graphicsPainter.Add ((Instantiate (mapTileSet.RandomWall (), new Vector3 (worldX - CompressedMap.TILE_SIZE, 0.0f, worldY - CompressedMap.TILE_SIZE), Quaternion.identity) as GameObject).GetComponent<Tile> ());
				if (mts.left == TileType.Room || mts.below == TileType.Room || mts.leftBelow == TileType.Room)
					graphicsPainter.Add ((Instantiate (mapTileSet.RandomWall (), new Vector3 (worldX - CompressedMap.TILE_SIZE, 0.0f, worldY + CompressedMap.TILE_SIZE), Quaternion.identity) as GameObject).GetComponent<Tile> ());
				if (mts.right == TileType.Room || mts.above == TileType.Room || mts.rightAbove == TileType.Room)
					graphicsPainter.Add ((Instantiate (mapTileSet.RandomWall (), new Vector3 (worldX + CompressedMap.TILE_SIZE, 0.0f, worldY - CompressedMap.TILE_SIZE), Quaternion.identity) as GameObject).GetComponent<Tile> ());
				if (mts.right == TileType.Room || mts.below == TileType.Room || mts.rightBelow == TileType.Room)
					graphicsPainter.Add ((Instantiate (mapTileSet.RandomWall (), new Vector3 (worldX + CompressedMap.TILE_SIZE, 0.0f, worldY + CompressedMap.TILE_SIZE), Quaternion.identity) as GameObject).GetComponent<Tile> ());
				
			
				if (mts.left == TileType.Room)
					graphicsPainter.Add ((Instantiate (mapTileSet.RandomWall (), new Vector3 (worldX - CompressedMap.TILE_SIZE, 0.0f, worldY), Quaternion.identity) as GameObject).GetComponent<Tile> ());
				if (mts.right == TileType.Room)
					graphicsPainter.Add ((Instantiate (mapTileSet.RandomWall (), new Vector3 (worldX + CompressedMap.TILE_SIZE, 0.0f, worldY), Quaternion.identity) as GameObject).GetComponent<Tile> ());
				if (mts.above == TileType.Room)
					graphicsPainter.Add ((Instantiate (mapTileSet.RandomWall (), new Vector3 (worldX, 0.0f, worldY - CompressedMap.TILE_SIZE), Quaternion.identity) as GameObject).GetComponent<Tile> ());
				if (mts.below == TileType.Room)
					graphicsPainter.Add ((Instantiate (mapTileSet.RandomWall (), new Vector3 (worldX, 0.0f, worldY + CompressedMap.TILE_SIZE), Quaternion.identity) as GameObject).GetComponent<Tile> ());
				break;
			
			}
			foreach (Tile t in graphicsPainter) {
				switch (t.mapTileType) {
				case MapTileType.Door:
				case MapTileType.Floor:
					t.graphic.renderer.materials [0].mainTexture = mapTileSet.RandomOpenTexture ();
					break;	
			case MapTileType.Wall:
					t.graphic.renderer.materials [0].mainTexture = mapTileSet.RandomWallTexture ();
					break;	
				}
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
	
	RectRoom NotSpawnRoom (MapMission mission) {
		while (true) {
			RectRoom rr = mission.compressedMap.rectRooms [Random.Range (0, mission.compressedMap.rectRooms.Count)];	
			
			if (!rr.startingRoom)
				return rr;
		}
	}
	public void GenerateSpawnMission (MapMission mission) {
		switch (mission.missionType) {
		case MissionType.Assualt:
			GenerateSpawnMissionAssualt (mission);
			break;
		case MissionType.Capture:
			GenerateSpawnMissionCapture (mission);
			break;
		case MissionType.Destroy:
			GenerateSpawnMissionDestroy (mission);
			break;
		case MissionType.Recover:
			GenerateSpawnMissionRecover (mission);
			break;
		case MissionType.Steal:
			GenerateSpawnMissionSteal (mission);
			break;
		case MissionType.Artifact:
			GenerateSpawnMissionArtifact (mission);
			break;
		case MissionType.Boss:
			GenerateSpawnMissionBoss (mission);
			break;
		}
	}
	
	void GenerateSpawnMissionAssualt (MapMission mission) {
		MissionAssualt miss = (Instantiate (managerPrefab.mission (mission.missionType)) as GameObject).GetComponent<MissionAssualt> ();
		Challenge c = GenerateChallenge (mission.level);
		
		foreach (GameObject go in c.enemies) {
			Controller e = factoryCharacter.SpawnCharacter (go, MapMission.RandomPositionInRoom (NotSpawnRoom (mission)));
			e.team = 1;
			miss.enemies.Add (e);
		}
		
		Target t = factoryCharacter.SpawnTarget (managerPrefab.targets [Random.Range (0, managerPrefab.targets.Count)], MapMission.RandomPositionInRoom (NotSpawnRoom (mission)));
		t.team = 1;
		miss.target = t;
	}
	
	void GenerateSpawnMissionCapture (MapMission mission) {
		
	}
	
	void GenerateSpawnMissionArtifact (MapMission mission) {
		MissionArtifact miss = (Instantiate (managerPrefab.mission (mission.missionType)) as GameObject).GetComponent<MissionArtifact> ();
		RectRoom artifactRoom = NotSpawnRoom (mission);
	
		Artifact a = factoryCharacter.SpawnIntel (managerPrefab.artifacts [Random.Range (0, managerPrefab.artifacts.Count)], MapMission.RandomPositionInRoom (artifactRoom)) as Artifact;
		
		miss.artifact = a;
		
		Challenge c = GenerateChallenge (mission.level * managerMap.universe.level);
		c.room = artifactRoom;
		mission.challenges.Add (c);
	}
	
	void GenerateSpawnMissionBoss (MapMission mission) {
		MissionBoss miss = (Instantiate (managerPrefab.mission (mission.missionType)) as GameObject).GetComponent<MissionBoss> ();
		
		Controller boss = (Instantiate (managerPrefab.bosses (Mathf.CeilToInt (managerMap.universe.level / 3.0f)) [Random.Range (0, managerPrefab.bosses (Mathf.CeilToInt (managerMap.universe.level / 3)).Count)], MapMission.CenterPositionInRoom (mission.compressedMap.rectRooms [0]), Quaternion.identity) as GameObject).GetComponent<Controller> ();
		
		boss.team = 1;
		
		miss.boss = boss;
	}
	
	void GenerateSpawnMissionDestroy (MapMission mission) {
		MissionDestroy miss = (Instantiate (managerPrefab.mission (mission.missionType)) as GameObject).GetComponent<MissionDestroy> ();
		
		for (int j = 0; j < mission.level + 1; j++) {
			Target t = factoryCharacter.SpawnTarget (managerPrefab.targets [Random.Range (0, managerPrefab.targets.Count)], MapMission.RandomPositionInRoom (NotSpawnRoom (mission)));
			t.team = 1;
			miss.targets.Add (t);
		}
	}
	
	void GenerateSpawnMissionRecover (MapMission mission) {
		MissionRecover miss = (Instantiate (managerPrefab.mission (mission.missionType)) as GameObject).GetComponent<MissionRecover> ();
		
		for (int j = 0; j < mission.level + 1; j++) {
			Intel i = (Instantiate (managerPrefab.intels [Random.Range (0, managerPrefab.intels.Count)], MapMission.RandomPositionInRoom (NotSpawnRoom (mission)), Quaternion.identity) as GameObject).GetComponent<Intel> ();
			miss.intel.Add (i);
		}
	}
	
	void GenerateSpawnMissionSteal (MapMission mission) {
		MissionSteal miss = (Instantiate (managerPrefab.mission (mission.missionType)) as GameObject).GetComponent<MissionSteal> ();
		//RectRoom rr = NotSpawnRoom (mission);
		Intel i = (Instantiate (managerPrefab.intels [Random.Range (0, managerPrefab.intels.Count)], MapMission.RandomPositionInRoom (NotSpawnRoom (mission)), Quaternion.identity) as GameObject).GetComponent<Intel> ();
		miss.intel = i;
		
		Controller kh = factoryCharacter.SpawnCharacter (managerPrefab.enemies (mission.level) [Random.Range (0, managerPrefab.enemies (mission.level).Count)], MapMission.RandomPositionInRoom (NotSpawnRoom (mission)));
		kh.team = 1;
		miss.keyHolder = kh;
	}
	
	public bool SpawnRewards (MapMission mission) {
		if (!(staggerSpawnCounter < mission.rewards.Count)) {
			staggerSpawnCounter = 0;
			return true;
		}
			
		GameObject go = Instantiate (managerPrefab.containers [Random.Range (0, managerPrefab.containers.Count)], MapMission.RandomPositionInRoom (mission.rewards [staggerSpawnCounter].room), Quaternion.identity) as GameObject;
		
		go.GetComponent<Container> ().reward = mission.rewards [staggerSpawnCounter].reward;
		
		staggerSpawnCounter++;
			return false;
	}
	
	public bool SpawnChallenges (MapMission mission) {
		if (!(staggerSpawnCounter < mission.challenges.Count)) {
			staggerSpawnCounter = 0;
			return true;
		}
			
		foreach (GameObject go in mission.challenges [staggerSpawnCounter].enemies) {
			Controller c = factoryCharacter.SpawnCharacter (go, MapMission.RandomPositionInRoom (mission.challenges [staggerSpawnCounter].room));
			c.team = 1;
		}
		staggerSpawnCounter++;
		return false;
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
	
	public override string ToString () {
		return "left|"+left+" top|"+top+" width|"+width+" height|"+height;
	}
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

[System.Serializable]
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