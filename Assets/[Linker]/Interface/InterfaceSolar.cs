using UnityEngine;
using System.Collections;

public class InterfaceSolar : Interface {
	public override bool SetDisplay (bool newDisplay) {
		managerPlayer.ship.SetMainCameraToCP ();
		return base.SetDisplay (newDisplay);
	}
	public void Update () {
		if (!display)
			return;
		if (Application.platform == RuntimePlatform.WindowsPlayer
			|| Application.platform == RuntimePlatform.OSXPlayer
			|| Application.platform == RuntimePlatform.LinuxPlayer
			|| Application.platform == RuntimePlatform.WindowsEditor)
			KeyboardMouse ();
		if (Application.platform == RuntimePlatform.Android 
			|| Application.platform == RuntimePlatform.IPhonePlayer)
			TouchScreen ();
	}
	
	void KeyboardMouse () {
		managerPlayer.ship.moveDirection = new Vector3 (Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal"));
	}
	
	void TouchScreen () {
	
	}
}
