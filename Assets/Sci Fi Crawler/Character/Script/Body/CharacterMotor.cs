using UnityEngine;
using System.Collections;

public class CharacterMotor : MonoBehaviour {
	public CharacterController characterController;
	
	public Vector3 moveDirection;
	
	public float maxSpeed;
	
	public void Start () {
		//characterController.Move (transform.forward);	
	}
	
	public void Update () {
		characterController.Move (moveDirection.normalized * maxSpeed * Time.deltaTime);
	}
}
