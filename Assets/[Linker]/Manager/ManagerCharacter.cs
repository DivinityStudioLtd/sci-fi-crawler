using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerCharacter : Manager {
	public void DestoryNonPlayer () {
		foreach (Entity e in entities) {	
			if (e.GetComponent<Controller> () != managerPlayer.currentBody) {
				Destroy (e.gameObject);
			}
		}
	}
	public void PlayerCleanUp () {
		foreach (ItemBucket p_ib in managerPlayer.currentBody.missionsInventory) {
			GameObject go;
			foreach (GameObject f in p_ib.firearms) {
				go = Instantiate (f) as GameObject;
				go.GetComponent<Firearm> ().RandomizeStats ();
				go.transform.parent = managerPlayer.transform;
				go.SetActive (false);
				managerPlayer.playerInventory.firearms.Add (go);
			}
			foreach (GameObject p in p_ib.powers) {
				go = Instantiate (p) as GameObject;
				go.transform.parent = managerPlayer.transform;
				go.SetActive (false);
				managerPlayer.playerInventory.powers.Add (go);
			}
			foreach (GameObject b in p_ib.bodies) {
				go = Instantiate (b) as GameObject;
				go.transform.parent = managerPlayer.transform;
				go.SetActive (false);
				managerPlayer.playerInventory.bodies.Add (go);
			}
			managerPlayer.playerInventory.credit += p_ib.credit;
		}
		Destroy (managerPlayer.currentBody.gameObject);
	}
}
