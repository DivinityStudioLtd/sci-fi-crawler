using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionAssualt : Mission {
	public List<Controller> enemies;
	public Target target;
	
	public override bool Completed () {
		return enemies.Count == 0 && target == null;	
	}
	public void Update () {
		for (int i = 0; i < enemies.Count; i++)
			if (enemies [i] == null) {
				enemies.RemoveAt (i);
				i--;
			}
	}
	
	public override string MissionStatus () {
		string returnString = "";
		if (enemies.Count != 0) {
			returnString += "Enemies" + "\n";
			int counter = 0; 
			foreach (Controller e in enemies)
				if (e != null)
					returnString += (++counter) + ": " + string.Format("{0:0.00}", Vector3.Distance (managerPlayer.currentBody.transform.position, e.transform.position)) + "\n";
		} else {
			returnString += "No Enemies Remaining" + "\n";
		}
		
		if (target != null)
			returnString += "Target: " + string.Format("{0:0.00}", Vector3.Distance (managerPlayer.currentBody.transform.position, target.transform.position));
		else
			returnString += "Target Destroyed";
		return returnString;
	}
}
