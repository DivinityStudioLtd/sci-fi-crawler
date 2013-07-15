using UnityEngine;
using System.Collections;

public class ManagerGame : Manager {
	public string gameName;
	public int mapSeed;
	
	public void SetRandom () {
		Random.seed = managerGame.mapSeed;
	}
	
	public override void ManagerStart () {
		managerInterface.SetInterface (interfaceLogin);
		base.ManagerStart ();
	}
	
	public override void ManagerWorking () {
		if (managerPlayer.controller == null) {
			factoryCharacter.SpawnPlayerCharacter ();
			managerInterface.SetInterface (interfaceTDS);
		}
	}
}