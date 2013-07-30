using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerPlayer : Manager {
	public bool hasArtefact;
	
	public Controller currentBody;
	
	public ItemBucket selected;
	
	public ItemBucket playerInventory;
	
	public override void ManagerWorking () {
		
	}
	public bool setup = false;
	public Ship ship;
}
