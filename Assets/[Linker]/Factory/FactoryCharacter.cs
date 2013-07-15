using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FactoryCharacter : Factory {
	public void SpawnCharacter (string character) {
		
	}
	public void SpawnPlayerCharacter () {
		List<GameObject> players = managerPrefab.players;
		
		managerPlayer.controller = (Instantiate (players [Random.Range (0, players.Count)], new Vector3 (0.0f, 1.0f, 0.0f), Quaternion.identity) as GameObject).GetComponent<Controller> ();
	
		List<GameObject> firearms = managerPrefab.firearms;
		
		foreach (GameObject go in firearms) {
			Firearm f = (Instantiate (go) as GameObject).GetComponent<Firearm> ();
			managerPlayer.controller.firearms.Add (f);
		}
		managerPlayer.controller.SetupFirearms ();
		
		List<GameObject> powers = managerPrefab.powers;
		
		for (int i = 0; i < 3; i++) {
			int ran = Random.Range (0, powers.Count);
			Power p = (Instantiate (powers [ran]) as GameObject).GetComponent<Power> ();
			powers.RemoveAt (ran);
			managerPlayer.controller.powers.Add (p);
		}
		
		managerPlayer.controller.SetupPowers ();
	}
}
