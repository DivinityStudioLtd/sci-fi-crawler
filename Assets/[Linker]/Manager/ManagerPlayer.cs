using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerPlayer : Manager {
	public bool hasArtifact;
	
	public Controller currentBody;
	
	public ItemBucket selected;
	
	public ItemBucket playerInventory;
	
	public override void ManagerWorking () {
		
	}
	
	public bool playerSetup;
	public bool shipSetup;
	public Ship ship;
	
	public void PositionShip () {
		shipSetup = true;
		
		ship.trailRenderer.time = Ship.TRAIL_LENGTH;
		
		Vector3 shipPosition = new Vector3 (0.0f, 0.0f, (managerMap.universe.SolarBodiesOfType (SolarBodyType.Planet) + 3) * FactoryMap.PLANET_SPACING);
		
		ship.transform.position = shipPosition;
		ship.graphic.gameObject.SetActive (true);
	}
}
