using UnityEngine;
using System.Collections;

public class SolarBody : Entity {
	public MapMission mapMission;
	public SolarBodyType solarBodyType;
	void Start () {
		managerMap.universe.solarbodies.Add (this);	
	}
}

public enum SolarBodyType {
	Sun,
	Planet,
	Moon,
	Station
}
