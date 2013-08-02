using UnityEngine;
using System.Collections;

public class PositionSelf : MonoBehaviour {
public Transform target;
	void Update () {
		if (target != null)
			transform.position = target.position;
	}
}
