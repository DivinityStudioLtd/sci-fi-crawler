using UnityEngine;
using System.Collections;

public class ManagerInterface : Manager {
	public Interface previousInterface;
	public Interface currentInterface;
	
	public bool changed;
	
	public override void ManagerStart () {
		base.ManagerStart ();
	}
	public override void ManagerWorking () {
		
	}
	
	/*
	void Update () {
		changed = false;
		if (Network.peerType == NetworkPeerType.Disconnected 
		    || Network.peerType == NetworkPeerType.Connecting)
			return;
		
		InterfaceSwitch ("Menu", InterfaceEnum.MainMenu, previousInterfaceEnum);
		// InterfaceSwitch ("Cabin", InterfaceEnum.Cabin, gameManager.CurrentMountEnum ());
	}
	
	void InterfaceSwitch (string button, InterfaceEnum targetScreen, InterfaceEnum altScreen) {
		if (Input.GetButtonDown (button)) {
			if (currentInterfaceEnum != targetScreen)
				SetInterface (targetScreen);
			else
				SetInterface (altScreen);
		}
	}
	*/
	public void PreviousInterface () {
		SetInterface (previousInterface);
	}
	
	public void SetInterface (Interface newInterface) {
		if (changed)
			return;
		if (currentInterface != null)
			currentInterface.SetDisplay (false);
		previousInterface = currentInterface;
		newInterface.SetDisplay (true);
		currentInterface = newInterface;
	}
}
