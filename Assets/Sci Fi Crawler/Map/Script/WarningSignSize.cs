using UnityEngine;
using System.Collections;

public class WarningSignSize : MonoBehaviour {
	void Update () {
		if (Linker.interfaceSolar.pa != null)
			transform.localScale = new Vector3 (0.2f, 0.2f, 0.2f);
		else
			transform.localScale = new Vector3 (0.4f, 0.4f, 0.4f);
	}
}
