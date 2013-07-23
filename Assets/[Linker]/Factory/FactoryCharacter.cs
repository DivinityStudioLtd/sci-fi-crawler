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
		managerPlayer.currentBody = (Instantiate (managerPlayer.selected.bodies[0], spawnPosition + new Vector3 (0.0f, 1.0f, 0.0f), Quaternion.identity) as GameObject).GetComponent<Controller> ();
	
		foreach (GameObject go in managerPlayer.selected.firearms) {
			Firearm f = (Instantiate (go) as GameObject).GetComponent<Firearm> ();
			managerPlayer.currentBody.firearms.Add (f);
		}
		managerPlayer.currentBody.SetupFirearms ();
		
		foreach (GameObject go in managerPlayer.selected.powers) {
			Power p = (Instantiate (go) as GameObject).GetComponent<Power> ();
			managerPlayer.currentBody.powers.Add (p);
		}
		managerPlayer.currentBody.SetupPowers ();
	}
	
	public void SpawnPlayerShip () {
		Vector3 spawnPosition = new Vector3 (0.0f, 0.0f, (managerMap.universe.SolarBodiesOfType (SolarBodyType.Planet) + 2) * FactoryMap.PLANET_SPACING);
		Ship s = (Instantiate (managerPrefab.ships [Random.Range (0, managerPrefab.ships.Count)], spawnPosition, Quaternion.identity) as GameObject).GetComponent<Ship> ();
		
		managerPlayer.ship = s;
	}
	
	public void SetupPlayer () {
		ItemBucket ib = new ItemBucket ();
		ib.bodies.Add (managerPrefab.bodies [Random.Range (0, managerPrefab.bodies.Count)]);
		for (int i = 0; i < 2; i++)
			ib.firearms.Add (managerPrefab.firearms [Random.Range (0, managerPrefab.firearms.Count)]);
		for (int i = 0; i < 3; i++)
			ib.powers.Add (managerPrefab.powers [Random.Range (0, managerPrefab.powers.Count)]);
		managerPlayer.selected = ib;
		
		/*
		for (int i = 0; i < 5; i++)
			managerPlayer.playerInventory.bodies.Add (managerPrefab.bodies [Random.Range (0, managerPrefab.bodies.Count)]);
		for (int i = 0; i < 5; i++)
			managerPlayer.playerInventory.firearms.Add (managerPrefab.firearms [Random.Range (0, managerPrefab.firearms.Count)]);
		for (int i = 0; i < 5; i++)
			managerPlayer.playerInventory.powers.Add (managerPrefab.powers [Random.Range (0, managerPrefab.powers.Count)]);*/
	}
	
	public void SpawnInventory (InventoryPosition ip) {
		GameObject go;
		
		go = (Instantiate (managerPlayer.selected.bodies [0]) as GameObject);
		go.GetComponent<Entity> ().SetParent (ip.bodyPosition, true);
		go.GetComponent<Controller> ().hUD3D.gameObject.SetActive (false);
		
		for (int i = 0; i < managerPlayer.selected.firearms.Count; i++) {
			go = (Instantiate (managerPlayer.selected.firearms [i]) as GameObject);
			go.GetComponent<Entity> ().SetParent (ip.firearmPositions [i], true);
		}
		
		for (int i = 0; i < managerPlayer.selected.powers.Count; i++) {
			go = (Instantiate (managerPlayer.selected.powers [i]) as GameObject);
			go.GetComponent<Entity> ().SetParent (ip.powerPositions [i], true);
			
		}
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