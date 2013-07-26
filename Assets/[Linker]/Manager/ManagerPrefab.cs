using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerPrefab : Manager {
	#region Container
	#endregion
	
	#region Enemy
	public List<GameObject> enemies (int level) {
		return ListedGameObjectResourceFolder ("Enemy/"+level);
	}
	#endregion
	
	#region Firearm
	public List<GameObject> firearms {
		get {
			return ListedGameObjectResourceFolder ("Firearm");
		}
	}
	#endregion
	
	#region Map
	public GameObject universeMap {
		get {
			return GameObjectResource ("Map/Universe");
		}
	}
	
	public List<Material> skybox {
		get {
			return ListedMaterialResourceFolder ("Universe Skybox");
		}
	}
	
	public GameObject missionMap {
		get {
			return GameObjectResource ("Map/Mission");
		}
	}
	
	public GameObject mission (MissionType missionType) {
		return GameObjectResource ("Map/Mission/Mission"+missionType.ToString());
	}
	
	public GameObject shop {
		get {
			return GameObjectResource ("Map/Shop");
		}
	}
	
	public GameObject intel {
		get {
			return GameObjectResource ("Prop/Intel/Intel");
		}
	}
	
	public List<GameObject> containers {
		get {
			return ListedGameObjectResourceFolder ("Map/Container");
		}
	}
	#endregion
	
	#region Obstacle
	#endregion
	
	#region Player
	public List<GameObject> bodies {
		get {
			return ListedGameObjectResourceFolder ("Player");
		}
	}
	#endregion
	
	#region Power
	public List<GameObject> powers {
		get {
			return ListedGameObjectResourceFolder ("Power");
		}
	}
	#endregion
	
	#region Projectile
	#endregion
	
	#region Ship
	public GameObject ship {
		get {
			return GameObjectResource ("Ship/Ship");
		}
	}
	public List<GameObject> shipGraphics {
		get {
			return ListedGameObjectResourceFolder ("Ship/Graphics");
		}
	}
	public List<Material> shipsTrails {
		get {
			return ListedMaterialResourceFolder ("Ship/Trail");
		}
	}
	#endregion
	
	#region Solar Body
	public GameObject planetArm {
		get {
			return GameObjectResource ("Solar Body/Planet Arm");
		}
	}
	
	public List<GameObject> suns {
		get {
			return ListedGameObjectResourceFolder ("Solar Body/Sun");
		}
	}
	
	public GameObject planet {
		get {
			return GameObjectResource ("Solar Body/Planet/Planet");
		}
	}
	
	public List<GameObject> stations {
		get {
			return ListedGameObjectResourceFolder ("Solar Body/Station");
		}
	}
	
	public List<Texture2D> planetTextures (PlanetTexture pT) {
		return ListedTexture2DResourceFolder ("Solar Body/Planet/"+pT.ToString ());
	} 
	#endregion
	
	#region Tile
	#endregion
	
	List<GameObject> ListedGameObjectResourceFolder (string folder) {
		Object[] objectArray = Resources.LoadAll (folder);
		
		List<GameObject> gameObjectList = new List<GameObject> ();
		
		for (int i = 0; i < objectArray.Length; i++) {
			if (objectArray [i] as GameObject != null) 
				gameObjectList.Add (objectArray [i] as GameObject);
		}
		
		return gameObjectList;
	}
	
	List<Texture2D> ListedTexture2DResourceFolder (string folder) {
		Object[] objectArray = Resources.LoadAll (folder);
		
		List<Texture2D> gameObjectList = new List<Texture2D> ();
		
		for (int i = 0; i < objectArray.Length; i++) {
			if (objectArray [i] as Texture2D != null) 
				gameObjectList.Add (objectArray [i] as Texture2D);
		}
		
		return gameObjectList;
	}
	
	List<Material> ListedMaterialResourceFolder (string folder) {
		Object[] objectArray = Resources.LoadAll (folder);
		
		List<Material> gameObjectList = new List<Material> ();
		
		for (int i = 0; i < objectArray.Length; i++) {
			if (objectArray [i] as Material != null) 
				gameObjectList.Add (objectArray [i] as Material);
		}
		
		return gameObjectList;
	}
	
	GameObject GameObjectResource (string file) {
		return Resources.Load (file) as GameObject;	
	}
}

public enum PlanetTexture {
	Hot,
	Cold,
	Habitable,
	Gas,
	Arid,
	General
}