using UnityEngine;
using System.Collections;

public class Intel : Entity {
	public GameObject graphics;
	public bool collected;
	// Use this for initialization
	void Start () {
		collected = false;
		SetParent (managerMap.currentMission.transform, false);
	}
	public void Collect () {
		collected = true;
		graphics.SetActive (false);
	}
}
