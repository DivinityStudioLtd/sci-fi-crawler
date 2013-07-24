using UnityEngine;
using System.Collections;

public class PickUpInteractor : MonoBehaviour {
	public Controller controller;
	public float pickUpDistance;
	void Update () {
		foreach (Collider collider in Physics.OverlapSphere (transform.position, pickUpDistance)) {
			Entity e = FindUtility.FindEntity (collider);
			if (e == null)
				continue;
			Container c = e.GetComponent<Container> ();
			if (c != null) {	
				controller.missionsInventory.Add (c.reward);
				Destroy (collider.gameObject);
			}
			Intel i = e.GetComponent<Intel> ();
			if (i != null)
				i.Collect ();
		}
	}
}
