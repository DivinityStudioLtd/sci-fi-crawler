using UnityEngine;
using System.Collections;

abstract public class Mission: Entity {
	public void Start () {
		managerMap.currentMission.mission = this;
		SetParent (managerMap.currentMission.transform, true);
	}
	
	public abstract bool Completed ();
}
