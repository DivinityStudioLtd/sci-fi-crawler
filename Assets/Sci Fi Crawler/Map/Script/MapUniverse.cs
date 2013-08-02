using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapUniverse : Map {
	public void Awake () {
		managerMap.universe = this;
	}
	
	public int level;
	
	public Shop shop;
	
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
