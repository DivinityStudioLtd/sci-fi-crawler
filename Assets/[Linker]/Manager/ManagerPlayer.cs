using UnityEngine;
using System.Collections;

public class ManagerPlayer : Manager {
	public Controller controller;
	//public override void ManagerStart () {
	//}
	
	public override void ManagerWorking () {
		managerInterface.SetInterface (interfaceTDS);
		base.ManagerWorking ();
	}
}