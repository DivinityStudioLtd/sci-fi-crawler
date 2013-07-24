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
		while (shopList.Count > targetAmount)
			shopList.RemoveAt (Random.Range (0, shopList.Count));
	
		for (int i = 0; i < shuffleAmount; i++) {
			if (shopList.Count == 0)
				break;
			shopList.RemoveAt (Random.Range (0, shopList.Count));
		}
		while (shopList.Count < targetAmount)
			shopList.Add (resourceList [Random.Range (0, resourceList.Count)]);
	}
}
