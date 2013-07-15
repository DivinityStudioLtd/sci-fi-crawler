using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlidingArea : MonoBehaviour {
	public List<Transform> path;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
[System.Serializable]
public class SlidingAreaNode {
	public Transform node;
	public float transitionTime;
}