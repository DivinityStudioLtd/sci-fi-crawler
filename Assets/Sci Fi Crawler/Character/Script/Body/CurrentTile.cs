using UnityEngine;
using System.Collections;

public class CurrentTile : MonoBehaviour {
	public Tile currentTile;
	
    void OnTriggerStay (Collider other) {
		if (other.GetComponent<Tile> () != null) {
			currentTile = other.gameObject.GetComponent<Tile> ();
		}
    }
}
