using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIBasic : AI {
	
	protected override void SenseTargets () {
		foreach (Collider c in Physics.OverlapSphere (transform.position, (senseRadius + (controller.character.currentHealth == controller.character.maxHealth ? 0 : 3)) * CompressedMap.TILE_SIZE)) {
			if (c.CompareTag("Mouse Plane"))
				continue;
			Entity e = FindUtility.FindEntity (c);
			if (e == null)
				continue;
			
			Controller co = e.GetComponent<Controller> ();
			if (co == null)
				continue;
			
			if (co.team != controller.team) {
				AddTarget (co);
				aIState = AIState.Attack;
			}
		}
	}
	
	protected override void Move () {
		if (path.Count > 0)
			if (path [path.Count - 1] != CurrentTarget ().currentTile.currentTile)
				managerMap.Path (controller.currentTile.currentTile, CurrentTarget ().currentTile.currentTile);
		
		if (path.Count == 0)
			managerMap.Path (controller.currentTile.currentTile, CurrentTarget ().currentTile.currentTile);
	}
	
	protected override void Attack () {
		if (targets.Count == 0) {
			aIState = AIState.Idle;
			controller.characterMotor.moveDirection = new Vector3 (0,0,0);
			return;
		}
			controller.transform.LookAt (new Vector3 (CurrentTarget().transform.position.x, controller.transform.position.y, CurrentTarget().transform.position.z));
		if (Vector3.Distance (transform.position, CurrentTarget ().transform.position) > 6) {
			controller.characterMotor.moveDirection = transform.forward;
		} else {
			controller.characterMotor.moveDirection = Vector3.zero;
		}
		
		controller.CurrentFirearm.SetTrigger (true);
	}
}
