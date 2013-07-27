using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SunGenerator : MonoBehaviour {
	public List<GameObject> suns;
	
	void Start () {
		float i = Random.Range (0.0f, 1.0f);
		float j = Random.Range (0.0f, 1.0f - i);
		
		Color c = new Color (i, j, 1.0f - i - j);
		
		
		
		
		float sunRadius;
		if (suns.Count == 2)
			sunRadius = Random.Range (7.5f, 10.0f);
		else
			sunRadius = Random.Range (15.0f, 20.0f);
		
		foreach (GameObject go in suns) {
			go.transform.localScale = new Vector3 (sunRadius, sunRadius, sunRadius);
			go.renderer.materials [0].color = c;
			go.light.color = c;
		}
	}
}
