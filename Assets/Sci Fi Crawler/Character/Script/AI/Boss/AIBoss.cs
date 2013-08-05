using UnityEngine;
using System.Collections;

public class AIBoss : AI {
	protected override void SenseTargets () {
		if (AddTarget (managerPlayer.currentBody))
			aIState = AIState.Attack;
	}
	
	public bool isLeft;
	float dodgeTime;
	
	protected override void Attack () {
		
		transform.LookAt (new Vector3 (CurrentTarget ().transform.position.x, transform.position.y, CurrentTarget ().transform.position.z));
		
		dodgeTime -=Time.deltaTime;
		if (dodgeTime < 0.0f) {
			dodgeTime += Random.Range (1.0f, 3.0f);
			isLeft = !isLeft;
		}
		
		Vector3 moveDirection = transform.right * (isLeft ? -1 : 1);
		if (Vector3.Distance (transform.position, CurrentTarget ().transform.position) > 4)
			moveDirection += transform.forward;
		controller.characterMotor.moveDirection = moveDirection;
		
		controller.CurrentFirearm.SetTrigger (true);
		//controller.graphics.transform.rotation = Quaternion.Euler (controller.characterMotor.characterController.velocity);
	}
}
