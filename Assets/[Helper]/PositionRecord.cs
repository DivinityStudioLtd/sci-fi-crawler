using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PositionRecord : MonoBehaviour {
	public List<Vector3> positionRecords;
	
	float currentTime;
	void Awake () {
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
	
	public Vector3 TargetPosition (int level) {
		level = Mathf.Clamp (level, 0, positionRecords.Count - 2);
		return Vector3.Lerp (positionRecords [level], positionRecords [level+1], Mathf.Clamp (currentTime, 0.0f, 1.0f));
	}
}
