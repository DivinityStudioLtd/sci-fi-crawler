using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FactoryCharacter : Factory {
	public Controller SpawnCharacter (GameObject prefab, Vector3 spawnPosition) {
		GameObject e = Instantiate (prefab, spawnPosition + new Vector3 (0.0f, 1.0f, 0.0f), Quaternion.identity) as GameObject;
		Controller ec = e.GetComponent<Controller> ();
		AI ai = e.GetComponent<AI> ();
		ec.firearms.Add ((Instantiate (ai.possibleFirearms [Random.Range (0, ai.possibleFirearms.Count)]) as GameObject).GetComponent<Firearm> ());
		ec.firearms [0].RandomizeStats ();
		ec.SetupFirearms ();
		return ec;
	}
	
	public void SpawnPlayerCharacter (Vector3 spawnPosition) {
		managerPlayer.currentBody = (Instantiate (managerPlayer.selected.bodies[0], spawnPosition + new Vector3 (0.0f, 1.0f, 0.0f), Quaternion.identity) as GameObject).GetComponent<Controller> ();
		managerPlayer.currentBody.gameObject.SetActive (true);
		foreach (GameObject go in managerPlayer.selected.firearms) {
			Firearm f = (Instantiate (go) as GameObject).GetComponent<Firearm> ();
			f.gameObject.SetActive (true);
			managerPlayer.currentBody.firearms.Add (f);
		}
		managerPlayer.currentBody.SetupFirearms ();
		
		foreach (GameObject go in managerPlayer.selected.powers) {
			Power p = (Instantiate (go) as GameObject).GetComponent<Power> ();
			p.gameObject.SetActive (true);
			managerPlayer.currentBody.powers.Add (p);
		}
		managerPlayer.currentBody.SetupPowers ();
	}
	
	public void SpawnPlayerShip () {
		Vector3 spawnPosition = new Vector3 (0.0f, 0.0f, (managerMap.universe.SolarBodiesOfType (SolarBodyType.Planet) + 2) * FactoryMap.PLANET_SPACING);
		Ship s = (Instantiate (managerPrefab.ship, spawnPosition, Quaternion.identity) as GameObject).GetComponent<Ship> ();
		GameObject shipGraphics = Instantiate (managerPrefab.shipGraphics [Random.Range (0, managerPrefab.shipGraphics.Count)], s.transform.position, Quaternion.identity) as GameObject;
		shipGraphics.transform.parent = s.graphic.transform;
		s.trailRenderer.material = managerPrefab.shipsTrails [Random.Range (0, managerPrefab.shipsTrails.Count)];
		managerPlayer.ship = s;
	}
	
	public void SetupPlayer () {
		ItemBucket ib = new ItemBucket ();
		GameObject go = Instantiate (managerPrefab.bodies [Random.Range (0, managerPrefab.bodies.Count)]) as GameObject;
		go.SetActive (false);
		ib.bodies.Add (go);
		go.transform.parent = managerPlayer.transform;
		for (int i = 0; i < 2; i++) {
			go = Instantiate (managerPrefab.firearms [Random.Range (0, managerPrefab.firearms.Count)]) as GameObject;
			go.GetComponent<Firearm> ().RandomizeStats ();
			go.SetActive (false);
			ib.firearms.Add (go);
			go.transform.parent = managerPlayer.transform;
		}
		for (int i = 0; i < 3; i++) {
			go = Instantiate (managerPrefab.powers [Random.Range (0, managerPrefab.powers.Count)]) as GameObject;
			go.SetActive (false);
			ib.powers.Add (go);
			go.transform.parent = managerPlayer.transform;
		}
		managerPlayer.selected = ib;
	}
	
	public void SpawnInventory (InventoryPosition ip) {
		GameObject go;
		
		go = (Instantiate (managerPlayer.selected.bodies [0]) as GameObject);
		go.SetActive (true);
		go.GetComponent<Entity> ().SetParent (ip.bodyPosition, true);
		go.GetComponent<Controller> ().hUD3D.gameObject.SetActive (false);
		
		for (int i = 0; i < managerPlayer.selected.firearms.Count; i++) {
			go = (Instantiate (managerPlayer.selected.firearms [i]) as GameObject);
			go.SetActive (true);
			go.GetComponent<Entity> ().SetParent (ip.firearmPositions [i], true);
		}
		
		for (int i = 0; i < managerPlayer.selected.powers.Count; i++) {
			go = (Instantiate (managerPlayer.selected.powers [i]) as GameObject);
			go.SetActive (true);
			go.GetComponent<Entity> ().SetParent (ip.powerPositions [i], true);
		}
	}
	
	public void SpawnShop (ShopPositions shopPosition, GameObject buy, GameObject sell) {
		GameObject go;
		if (buy != null) {
			go = (Instantiate (buy, Vector3.zero, Quaternion.identity) as GameObject);
			go.GetComponent <Entity> ().SetParent (shopPosition.buy, true);
			if (go.GetComponent<Controller> () != null)
				go.GetComponent<Controller> ().hUD3D.gameObject.SetActive (false);
			go.SetActive (true);
		}
		if (sell != null) {
			go = (Instantiate (sell, Vector3.zero, Quaternion.identity) as GameObject);
			go.GetComponent <Entity> ().SetParent (shopPosition.sell, true);
			if (go.GetComponent<Controller> () != null)
				go.GetComponent<Controller> ().hUD3D.gameObject.SetActive (false);
			go.SetActive (true);
		}
	}
	
	public Target SpawnTarget (GameObject prop, Vector3 spawnPosition) {
		GameObject go = Instantiate (prop, spawnPosition + new Vector3 (0.0f, 1.0f, 0.0f), Quaternion.Euler (new Vector3 (0, Random.Range (0.0f, 360.0f), 0))) as GameObject;
		Target p = go.GetComponent<Target> ();
		p.SetParent (managerMap.currentMission.transform, false);
		return p;
	}
	
	public Intel SpawnIntel (GameObject intel, Vector3 spawnPosition) {
		GameObject go = Instantiate (intel, spawnPosition + new Vector3 (0.0f, 1.0f, 0.0f), Quaternion.Euler (new Vector3 (0, Random.Range (0.0f, 360.0f), 0))) as GameObject;
		Intel i = go.GetComponent<Intel> ();
		i.SetParent (managerMap.currentMission.transform, false);
		return i;
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