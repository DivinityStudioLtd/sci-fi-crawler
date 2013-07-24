using UnityEngine;
using System.Collections;

public class Container : Entity {
	public ItemBucket reward;
	
	public void Start () {
		SetParent (managerMap.currentMission.transform, false);	
	}
}
