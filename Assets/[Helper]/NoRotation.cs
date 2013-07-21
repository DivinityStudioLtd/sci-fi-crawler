using UnityEngine;
using System.Collections;

public class NoRotation : MonoBehaviour {
	void Update () {
		transform.rotation = Quaternion.identity;
	}
}
