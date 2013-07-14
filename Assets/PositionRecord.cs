using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PositionRecord : MonoBehaviour {
	public List<Vector3> positionRecords;
	
	float currentTime;
	void Start () {
		for (int i = 0; i < 5; i++)
			positionRecords.Add (transform.position);
	}
	void Update () {
		currentTime -= Time.deltaTime;
		
		if (currentTime <= 0.0f) {
			currentTime += 1.0f;
			positionRecords.Insert (0, transform.position);
			positionRecords.RemoveAt (positionRecords.Count - 1);
		}
	}
}
