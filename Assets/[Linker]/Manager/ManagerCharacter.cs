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
			foreach (GameObject go in p_ib.firearms)
				managerPlayer.playerInventory.firearms.Add (go);
			foreach (GameObject go in p_ib.powers)
				managerPlayer.playerInventory.powers.Add (go);
			foreach (GameObject go in p_ib.bodies)
				managerPlayer.playerInventory.bodies.Add (go);
			managerPlayer.playerInventory.credit += p_ib.credit;
		}
		Destroy (managerPlayer.currentBody.gameObject);
	}
}
