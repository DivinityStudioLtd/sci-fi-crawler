using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	float currentRotationTime;
	public float rotationTime; 
	
	public void SetRotationTime (float newTime) {
		rotationTime = newTime;
		currentRotationTime = Random.Range (0, 1);//rotationTime);
	}
	
	void FixedUpdate () {
		if (rotationTime == 0.0f)
			return;
		currentRotationTime += Time.deltaTime;
		if (currentRotationTime > rotationTime)
			currentRotationTime -= rotationTime;
		transform.rotation = Quaternion.Euler (new Vector3 (0.0f, currentRotationTime / rotationTime * 360.0f, 0.0f));
		
	}
}
