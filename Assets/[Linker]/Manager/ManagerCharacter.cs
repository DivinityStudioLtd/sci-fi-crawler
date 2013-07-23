using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerCharacter : Manager {
	public void DestoryNonPlayer () {
		foreach (Entity e in entities) {	
			if (e != managerPlayer.currentBody) {}
				Destroy (e.gameObject);
		}
	}
}
