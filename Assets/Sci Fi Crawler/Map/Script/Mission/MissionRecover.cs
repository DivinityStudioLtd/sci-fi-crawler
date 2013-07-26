using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionRecover : Mission {
	public List<Controller> enemies;
	public Intel intel;
	
	public override bool Completed () {
		return enemies.Count == 0 && intel.collected;
	}
	
	public override string MissionStatus () {
		return "";
	}
}
