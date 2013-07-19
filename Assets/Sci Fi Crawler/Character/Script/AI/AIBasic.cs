using UnityEngine;
using System.Collections;

public class AIBasic : AI {
	
	protected override void SenseTargets () {
		foreach (Collider c in Physics.OverlapSphere (transform.position, senseRadius * CompressedMap.TILE_SIZE)) {
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
	
	protected override void Attack () {
		Vector3 targetPosition = CurrentTarget ().positionRecord.TargetPosition (aILevel);
		transform.LookAt (new Vector3 (targetPosition.x, transform.position.y, targetPosition.z));
		
		controller.CurrentFirearm.SetTrigger (true);
	}
}
