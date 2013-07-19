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
		
		managerPlayer.controller = (Instantiate (bodies [Random.Range (0, bodies.Count)], spawnPosition + new Vector3 (0.0f, 1.0f, 0.0f), Quaternion.identity) as GameObject).GetComponent<Controller> ();
	
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
	
	public Reward GenerateReward (int points) {
		ItemBucket ib = new ItemBucket ();
		int bodiesToAdd = 0;
		int firearmsToAdd = 0;
		int powersToAdd = 0;
		int creditsToAdd = 0;
		
		while (points > 0) {
			int ran = (int) (100.0f * Random.Range (0.0f,1.0f));
			
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
				if (ran == Mathf.Clamp (ran, 63, 100)) {
					firearmsToAdd++;
					points -= 4;
				} else if (ran == Mathf.Clamp (ran, 43, 62)) {
					powersToAdd++;
					points -= 2;
				} else {
					creditsToAdd++;
					points -= 1;
				}
			} else if (points >= 2) {
				if (ran == Mathf.Clamp (ran, 65, 100)) {
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
			ib.credit += Random.Range (2000,4000);
		
		Reward r = new Reward ();
		r.reward = ib;
		return r;
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