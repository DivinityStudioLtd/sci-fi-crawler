using UnityEngine;
using System.Collections;

public class MapUniverse : Map {
	public void Awake () {
		managerMap.universe = this;
	}
}
