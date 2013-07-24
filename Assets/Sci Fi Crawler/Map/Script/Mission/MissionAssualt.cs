using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionAssualt : Mission {
	public List<Controller> enemies;
	
	public override bool Completed () {
		return enemies.Count == 0;	
	}
}
