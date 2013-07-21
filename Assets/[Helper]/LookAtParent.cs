using UnityEngine;
using System.Collections;

public class LookAtParent : MonoBehaviour {
	void LateUpdate () {
		if (transform.parent != null)
			transform.LookAt (transform.parent);
	}
}
