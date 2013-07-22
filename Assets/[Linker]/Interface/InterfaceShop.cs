using UnityEngine;
using System.Collections;

public class InterfaceShop : Interface {
	new public void OnGUI () {
		if (!display) 
			return;
		interfaceSolar.MenuGame ();
	}
}
