using UnityEngine;
using System.Collections;

public class Ship : Entity {
	public Transform cameraPosition;
	public void SetMainCameraToCP () {
		InterfaceUtility.SetCameraToTransform (cameraPosition, true);
	}
	
	public Transform graphic;
	
	public Vector3 moveDirection;
	
	public float maxSpeed;
	
	public void Update () {
		if (rigidbody.velocity.magnitude < maxSpeed)
			rigidbody.AddForce (moveDirection * 25);
		
		if (rigidbody.velocity != Vector3.zero)
			graphic.rotation = Quaternion.LookRotation (rigidbody.velocity);
	}
}
