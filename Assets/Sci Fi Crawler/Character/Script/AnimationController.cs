using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {
	public void LookRotation (Vector3 lookRotation) {
		transform.rotation = Quaternion.LookRotation (lookRotation);
	}
	public void LookAt (Vector3 lookAt) {
		transform.LookAt (lookAt);
	}
}
