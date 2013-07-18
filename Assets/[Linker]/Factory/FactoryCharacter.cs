using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FactoryCharacter : Factory {
	public void SpawnCharacter (string character) {
		
	}
	public void SpawnPlayerCharacter (Vector3 startPosition) {
		List<GameObject> players = managerPrefab.players;
		
		managerPlayer.controller = (Instantiate (players [Random.Range (0, players.Count)], startPosition + new Vector3 (0.0f, 1.0f, 0.0f), Quaternion.identity) as GameObject).GetComponent<Controller> ();
	
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
	
	public ItemBucket GenerateReward (int points) {
		ItemBucket ib = new ItemBucket ();
		while (points > 0){
			int ran = (int) (100.0f * Random.Range (0.0f,1.0f));
			if (points >= 8) {
				if (ran == Mathf.Clamp (ran, 100, 90)) {
					
				} else if (ran == Mathf.Clamp (ran, 89, 70)) {
					
				} else if (ran == Mathf.Clamp (ran, 69, 40)) {
					
				} else if (ran == Mathf.Clamp (ran, 39, 0)) {
					
				}
			} else if (points >= 4) {
				if (ran == Mathf.Clamp (ran, 100, 90)) {
					
				} else if (ran == Mathf.Clamp (ran, 89, 43)) {
					
				} else if (ran == Mathf.Clamp (ran, 42, 0)) {
					
				}
				
			} else if (points >= 2) {
				if (ran == Mathf.Clamp (ran, 100, 90)) {
					
				} else if (ran == Mathf.Clamp (ran, 89, 70)) {
					
				}
			} else if (points >= 1) {
				
			}
		}
		return ib;
	}
}
[System.Serializable]
public class ItemBucket {
	public List<Firearm> firearms;
	public List<Power> powers;
	public List<Controller> bodies;
	public int credit;
	
	public ItemBucket () {
		firearms = new List<Firearm> ();
		powers = new List<Power> ();
		bodies = new List<Controller> ();
		credit = 0;
	}
}