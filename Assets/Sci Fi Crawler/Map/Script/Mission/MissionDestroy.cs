using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionDestroy : Mission {
	public List<Target> targets;
	
	public override bool Completed () {
		return targets.Count == 0;
	}
	
	public void Update () {
		for (int i = 0; i < targets.Count; i++)
			if (targets [i] == null) {
				targets.RemoveAt (i);
				i--;
			}	
	}
	
	public override string MissionStatus () {
		string returnString = "";
		if (targets.Count != 0) {
			returnString += "Targets" + "\n";
			int counter = 0; 
			foreach (Target p in targets)
				returnString += (++counter) + ": " + string.Format("{0:0.00}", Vector3.Distance (managerPlayer.currentBody.transform.position, p.transform.position)) + "\n";
		} else {
			returnString += "No Targets Remaining";
		}
		return returnString;
	}
}
