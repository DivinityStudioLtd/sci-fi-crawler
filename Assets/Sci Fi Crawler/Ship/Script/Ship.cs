using UnityEngine;
using System.Collections;

public class Ship : Entity {
	public static float TRAIL_LENGTH = 2.0f;
	
	public TrailRenderer trailRenderer;
	
	public Transform cameraPosition;
	public void SetMainCameraToCP () {
		InterfaceUtility.SetCameraToTransform (cameraPosition, true);
	}
	
	public Transform graphic;
	
	public Vector3 moveDirection;
	public float rotationDirection;
	
	public float maxSpeed;
	public float rotationSpeed;
	
	public void Update () {
		if (rigidbody.velocity.magnitude < maxSpeed)
			rigidbody.AddForce (moveDirection * 35);
		graphic.Rotate (Vector3.up * rotationDirection * rotationSpeed * Time.deltaTime);
		//if (rigidbody.velocity != Vector3.zero)
			//graphic.rotation = Quaternion.LookRotation (rigidbody.velocity);
	}
	
	public PlanetArm currentPlanetArm;
	
	void OnTriggerEnter(Collider other) {
        if (other.transform.parent == null)
			return;
		PlanetArm pa = other.transform.parent.GetComponent<PlanetArm> ();
		if (pa == null)
			return;
		if (pa == currentPlanetArm)
			return;
		currentPlanetArm = pa;
    }
}
