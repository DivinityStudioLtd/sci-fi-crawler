using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetArm : Entity {
	public Rotate armRotate;
	public Transform edge;
	
	public Transform planetPosition;
	public List<Transform> planetSolarBodyPositions;
	
	public Transform cameraPosition;
	public Transform shipPosition;
}

