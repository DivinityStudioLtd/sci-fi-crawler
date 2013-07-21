using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapUniverse : Map {
	static public float MAX_DISTANCE = 250.0f;
	public void Awake () {
		managerMap.universe = this;
	}
	
	public List<SolarBody> solarbodies;
	
	
	public int SolarBodiesOfType (SolarBodyType sbt) {
		int count = 0;
		foreach (SolarBody sb in solarbodies)
			if (sb.solarBodyType == sbt)
				count++;
		return count;
	}
	
	
	public int numberOfSolarBodies;
}
