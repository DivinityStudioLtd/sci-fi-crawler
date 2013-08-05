using UnityEngine;
using System.Collections;

public class MissionBoss : Mission {
	public Controller boss;
	
	public override bool Completed () {
		return boss == null;	
		
	}
	
	public override string MissionStatus () {
		return boss.entityName;
	}
}
