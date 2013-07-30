using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shop : Entity {
	public ItemBucket itemBucket;
	
	void Start () {
		managerMap.universe.shop = this;
		
		Shuffle ();
	}
	
	public int targetBodies;
	public int targetFirearms;
	public int targetPowers;
	public int shuffleBodies;
	public int shuffleFirearms;
	public int shufflePowers;
	
	public void Shuffle () {
		ShuffleAid (targetBodies, shuffleBodies, itemBucket.bodies, managerPrefab.bodies);
		ShuffleAid (targetFirearms, shuffleFirearms, itemBucket.firearms, managerPrefab.firearms);
		ShuffleAid (targetPowers, shufflePowers, itemBucket.powers, managerPrefab.powers);
	}
			
	void ShuffleAid (int targetAmount, int shuffleAmount, List<GameObject> shopList, List<GameObject> resourceList) {
		while (shopList.Count > targetAmount) {
			int i = Random.Range (0, shopList.Count);
			GameObject temp = shopList [i];
			shopList.RemoveAt (i);
			Destroy (temp);
		}
	
		for (int j = 0; j < shuffleAmount; j++) {
			if (shopList.Count == 0)
				break;
			int i = Random.Range (0, shopList.Count);
			GameObject temp = shopList [i];
			shopList.RemoveAt (i);
			Destroy (temp);
		}
		while (shopList.Count < targetAmount) {
			GameObject go = Instantiate (resourceList [Random.Range (0, resourceList.Count)]) as GameObject;
			shopList.Add (go);
			go.transform.parent = this.transform;
			go.SetActive (false);
			if (go.GetComponent<Firearm> () != null)
				go.GetComponent<Firearm> ().RandomizeStats ();
		}
	}
}
