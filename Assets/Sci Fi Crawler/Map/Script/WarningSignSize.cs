using UnityEngine;
using System.Collections;

public class WarningSignSize : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Linker.interfaceSolar.pa != null)
			transform.localScale = new Vector3 (0.2f, 0.2f, 0.2f);
		else
			transform.localScale = new Vector3 (0.4f, 0.4f, 0.4f);
	}
}
