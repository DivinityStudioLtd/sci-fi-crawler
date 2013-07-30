using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionRecover : Mission {
	public List<Intel> intel;
	
	public override bool Completed () {
		foreach (Intel i in intel)
			if (!i.collected)
				return false;
		return true;	
	}
	
	public override string MissionStatus () {
		if (Completed ()) {
			return "Intel Collected";
		} else {
			string returnString = "";
			int count = 1;
			foreach (Intel i in intel)
				if (!i.collected)
					returnString += "\n" + (count++) + ": " + string.Format("{0:0.00}", Vector3.Distance (managerPlayer.currentBody.transform.position, i.transform.position));
			
			return "Intel" + returnString; 
		}
	}
}
