using UnityEngine;
using System.Collections;

[AddComponentMenu("Utility/LightUtility")]
public class LightUtility : Utility {
	
	static public void SpawnLight (GameObject light, Vector3 position, Quaternion rotation) {
		Instantiate (light, position, rotation);
	}
}
