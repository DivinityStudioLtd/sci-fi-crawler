using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerPrefab : Manager {
	public List<GameObject> firearms {
		get {
			return ListedResourceFolder ("Firearm");
		}
	}
	public List<GameObject> powers {
		get {
			return ListedResourceFolder ("Power");
		}
	}
	public List<GameObject> players {
		get {
			return ListedResourceFolder ("Player");
		}
	}
	
	List<GameObject> ListedResourceFolder (string folder) {
		Object[] objectArray = Resources.LoadAll (folder);
		
		List<GameObject> gameObjectList = new List<GameObject> ();
		
		for (int i = 0; i < objectArray.Length; i++) {
			gameObjectList.Add (objectArray [i] as GameObject);
		}
		
		return gameObjectList;
	}
}
