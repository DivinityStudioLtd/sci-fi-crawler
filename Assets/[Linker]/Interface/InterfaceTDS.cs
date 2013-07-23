using UnityEngine;
using System.Collections;

public class InterfaceTDS : Interface {
	public override bool SetDisplay (bool newDisplay) {
		managerPlayer.currentBody.SetMainCameraToTDS  ();
		return base.SetDisplay (newDisplay);
	}
	public float maxCameraOffset;
	
	public void Update () {
		if (!display)
			return;
		if (Application.platform == RuntimePlatform.WindowsPlayer
			|| Application.platform == RuntimePlatform.OSXPlayer
			|| Application.platform == RuntimePlatform.LinuxPlayer
			|| Application.platform == RuntimePlatform.WindowsEditor
			|| Application.platform == RuntimePlatform.OSXEditor)
			KeyboardMouse ();
		if (Application.platform == RuntimePlatform.Android 
			|| Application.platform == RuntimePlatform.IPhonePlayer)
			TouchScreen ();
	}
	
	public void KeyboardMouse () {
		managerPlayer.currentBody.characterMotor.moveDirection = new Vector3 (Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
		
		//if (managerPlayer.controller.characterMotor.moveDirection != Vector3.zero)
		//	managerPlayer.controller.animationController.LookRotation (managerPlayer.controller.characterMotor.moveDirection);
		
		if (Input.GetButtonDown ("Fire1"))
			managerPlayer.currentBody.CurrentFirearm.SetTrigger (true);//, true);
		if (Input.GetButtonUp ("Fire1"))
			managerPlayer.currentBody.CurrentFirearm.SetTrigger (false);//, true);
		
		/*
		if (Input.GetButtonDown ("Fire2"))
			managerPlayer.controller.CurrentFirearm.SetTrigger (true);//, false);
		if (Input.GetButtonUp ("Fire2"))
			managerPlayer.controller.CurrentFirearm.SetTrigger (false);//, false);
		*/
		//if (Input.GetButton ("Fire1") || Input.GetButton ("Fire2"))
			foreach (RaycastHit r_c_h in Physics.RaycastAll (Camera.main.ScreenPointToRay (Input.mousePosition)))
				if (r_c_h.collider.CompareTag ("Mouse Plane")) {
					managerPlayer.currentBody.animationController.LookAt (r_c_h.point);
			
					Camera.main.transform.localPosition = new Vector3 (
						(Input.mousePosition.x - (Screen.width / 2)) / (Screen.width / 2) * maxCameraOffset,
						(Input.mousePosition.y - (Screen.height / 2)) / (Screen.height / 2) * maxCameraOffset,
						0.0f
						);
					}
		
		if (Input.GetButtonUp ("Swap Weapon")) {
			managerPlayer.currentBody.CurrentFirearm.SetTrigger (false);//, true);
			managerPlayer.currentBody.CurrentFirearm.SetTrigger (false);//, false);
			managerPlayer.currentBody.NextWeapon ();
		}
		
		if (Input.GetAxis("Mouse ScrollWheel") > 0.0f)
			managerPlayer.currentBody.CyclePower (true);
		if (Input.GetAxis("Mouse ScrollWheel") < 0.0f)
			managerPlayer.currentBody.CyclePower (false);
		if (Input.GetButton ("Fire2"))
			managerPlayer.currentBody.CurrentPower.Use (managerPlayer.currentBody);
		/*
		if (currentPopUpDelay > 0.0f) {
			currentPopUpDelay -= Time.deltaTime;
		}
		*/
		/*
		if (playerManager.movementController.characterMotor.moveDirection != Vector3.zero) {
			if (Input.GetButtonDown ("Run") && playerManager.character.canRun) {
				playerManager.firearmController.CurrentFirearm ().animationController.ControlAnimation (AnimationType.Animate, "run");
				playerManager.movementController.characterMotor.Run ();
			}
			if (Input.GetButton ("Run") && playerManager.character.canRun) {
				playerManager.character.DrainStamina ();
			}
			if (Input.GetButtonUp ("Run") || !playerManager.character.canRun) {
				playerManager.firearmController.CurrentFirearm ().animationController.ControlAnimation (AnimationType.Animate, "idle");
				playerManager.movementController.characterMotor.Walk ();
			}
		} else {
			playerManager.firearmController.CurrentFirearm ().animationController.ControlAnimation (AnimationType.Animate, "idle");
		}
		
		if (Input.GetButtonDown ("Interact")) {
			playerManager.movementController.fpsInteractor.Interact ();
		}
		
		if (Input.GetButtonDown ("Flashlight")) {
			//playerManager.controller.flashlight.Toggle ();
		}
		
		
		if (Input.GetButtonDown ("Reload")) {
			playerManager.firearmController.CurrentFirearm ().Reload ();
		}*/
		
		if (Input.GetKeyDown (KeyCode.Backspace))
			managerGame.MissionToUniverse ();
	}
	
	public void TouchScreen () {
		
	}
}
