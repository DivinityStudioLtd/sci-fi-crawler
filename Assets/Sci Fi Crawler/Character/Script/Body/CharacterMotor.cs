using UnityEngine;
using System.Collections;

public class CharacterMotor : MonoBehaviour {
	public CharacterController characterController;
	
	public Vector3 moveDirection;
	
	public float maxSpeed;
	
	public bool isAiming;
	
	public void Update () {
		float speed = !isAiming ? maxSpeed : maxSpeed / 2.0f;
		characterController.Move (moveDirection.normalized * speed * Time.deltaTime);
	}
}
