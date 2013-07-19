using UnityEngine;
using System.Collections;

public class Tile : Entity {
	public int x;
	public int y;
	
	public void Start () {
		transform.parent = managerMap.currentMission.transform;
	}
}
