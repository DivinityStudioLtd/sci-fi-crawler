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
				if (AddTarget (co))
					aIState = AIState.Move;
			}
		}
	}
	
	protected override void Move () {
		if (!(targets.Count > 0)) {
			aIState = AIState.Idle;
			controller.characterMotor.moveDirection = Vector3.zero;
			return;
		}
		
		if (CurrentTarget () != null && Vector3.Distance (controller.transform.position, CurrentTarget ().transform.position) < movementStateThreshold) {
			aIState = AIState.Attack;
			controller.characterMotor.moveDirection = Vector3.zero;
			return;
		}
		
		if (path.Count == 0 || TargetMoved ()) {
			currentTileI = 0;
			path = managerMap.Path (controller.currentTile.currentTile, CurrentTarget ().currentTile.currentTile);
		} else if (currentTileI < path.Count) {
			Vector3 target = new Vector3 (path [currentTileI].transform.position.x, controller.transform.position.y, path [currentTileI].transform.position.z);
		
			if (Vector3.Distance (controller.transform.position, target) < 3)
				currentTileI++;
			
			controller.transform.LookAt (target);
			controller.characterMotor.moveDirection = transform.forward;
		} else {
			controller.characterMotor.moveDirection = Vector3.zero;
		}
	}
	
	protected override void Attack () {
		if (!(targets.Count > 0)) {
			aIState = AIState.Idle;
			controller.characterMotor.moveDirection = Vector3.zero;
			controller.CurrentFirearm.SetTrigger (false);
			return;
		}
		
		if (Vector3.Distance (controller.transform.position, CurrentTarget ().transform.position) > movementStateThreshold) {
			aIState = AIState.Move;
			controller.characterMotor.moveDirection = Vector3.zero;
			controller.CurrentFirearm.SetTrigger (false);
			return;
		}
		
		controller.transform.LookAt (CurrentTarget ().positionRecord.TargetPosition (aILevel));
		if (Vector3.Distance (transform.position, CurrentTarget ().transform.position) > movementStateThreshold - 2) {
			controller.characterMotor.moveDirection = transform.forward;
		} else {
			controller.characterMotor.moveDirection = Vector3.zero;
		}
		
		controller.CurrentFirearm.SetTrigger (true);
	}
}
