using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionSteal : Mission {
	public Controller keyHolder;
	public Intel intel;
	
	public override bool Completed () {
		return keyHolder == null && intel.collected;
	}
	
	public override string MissionStatus () {
		string returnString = "";
		if (keyHolder != null)
			returnString += "Key Holder: " + string.Format("{0:0.00}", Vector3.Distance (managerPlayer.currentBody.transform.position, keyHolder.transform.position));
		else
			returnString += "Key Holder Neutralized";
		returnString += "\n";
		if (!intel.collected)
			returnString += "Intel: " + string.Format("{0:0.00}", Vector3.Distance (managerPlayer.currentBody.transform.position, intel.transform.position));
		else
			returnString += "Intel Collected";
		return returnString;
	}
}
