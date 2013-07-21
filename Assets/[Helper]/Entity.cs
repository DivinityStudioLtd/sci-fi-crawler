using UnityEngine;
using System.Collections;

public class Entity : Linker {
	public string entityName;
	
	public void SetParent (Transform newParent, bool align) {
		if (newParent == null) {
			transform.parent = null;
		} else {
			transform.parent = newParent;
			if (align) {
				transform.localPosition = Vector3.zero;
				transform.localRotation = Quaternion.identity;
			}
		}
	}
}
