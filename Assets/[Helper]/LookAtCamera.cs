using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {
	void LateUpdate () {
		transform.LookAt (Camera.main.transform.position);
	}
}
