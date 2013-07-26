using UnityEngine;
using System.Collections;

public class MissionSteal : Mission {
	public Intel intel;
	
	public override bool Completed () {
		return intel.collected;	
	}
	
	public override string MissionStatus () {
		if (intel.collected)
			return "Intel Collected";
		return "Intel Not Collected";
	}
}
