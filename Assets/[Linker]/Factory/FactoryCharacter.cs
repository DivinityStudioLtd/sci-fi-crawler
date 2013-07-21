using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FactoryCharacter : Factory {
	public void SpawnCharacter (GameObject prefab, Vector3 spawnPosition) {
		GameObject e = Instantiate (prefab, spawnPosition + new Vector3 (0.0f, 1.0f, 0.0f), Quaternion.identity) as GameObject;
		Controller ec = e.GetComponent<Controller> ();
		AI ai = e.GetComponent<AI> ();
		ec.firearms.Add ((Instantiate (ai.possibleFirearms [Random.Range (0, ai.possibleFirearms.Count)]) as GameObject).GetComponent<Firearm> ());
		ec.SetupFirearms ();
	}
	public void SpawnPlayerCharacter (Vector3 spawnPosition) {
		List<GameObject> bodies = managerPrefab.bodies;
		
		managerPlayer.currentBody = (Instantiate (bodies [Random.Range (0, bodies.Count)], spawnPosition + new Vector3 (0.0f, 1.0f, 0.0f), Quaternion.identity) as GameObject).GetComponent<Controller> ();
	
		List<GameObject> firearms = managerPrefab.firearms;
		
		foreach (GameObject go in firearms) {
			Firearm f = (Instantiate (go) as GameObject).GetComponent<Firearm> ();
			managerPlayer.currentBody.firearms.Add (f);
		}
		managerPlayer.currentBody.SetupFirearms ();
		
		List<GameObject> powers = managerPrefab.powers;
		
		for (int i = 0; i < 3; i++) {
			int ran = Random.Range (0, powers.Count);
			Power p = (Instantiate (powers [ran]) as GameObject).GetComponent<Power> ();
			powers.RemoveAt (ran);
			managerPlayer.currentBody.powers.Add (p);
		}
		
		managerPlayer.currentBody.SetupPowers ();
	}
	public void SpawnPlayerShip () {
		Vector3 spawnPosition = new Vector3 (0.0f, 0.0f, (managerMap.universe.SolarBodiesOfType (SolarBodyType.Planet) + 2) * FactoryMap.PLANET_SPACING);
		Ship s = (Instantiate (managerPrefab.ships [Random.Range (0, managerPrefab.ships.Count)], spawnPosition, Quaternion.identity) as GameObject).GetComponent<Ship> ();
		
		managerPlayer.ship = s;
	}
}
[System.Serializable]
public class ItemBucket {
	public List<GameObject> firearms;
	public List<GameObject> powers;
	public List<GameObject> bodies;
	public int credit;
	
	public ItemBucket () {
		firearms = new List<GameObject> ();
		powers = new List<GameObject> ();
		bodies = new List<GameObject> ();
		credit = 0;
	}
}