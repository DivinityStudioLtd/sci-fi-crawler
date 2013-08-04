using UnityEngine;
using System.Collections;

public class InterfaceTDS : Interface {
	public override bool SetDisplay (bool newDisplay) {
		managerPlayer.currentBody.SetMainCameraToTDS  ();
		managerGame.listener.GetComponent<PositionSelf> ().target = managerPlayer.currentBody.transform;
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
		
		if (Input.GetButtonDown ("Fire1"))
			managerPlayer.currentBody.CurrentFirearm.SetTrigger (true);
		if (Input.GetButtonUp ("Fire1"))
			managerPlayer.currentBody.CurrentFirearm.SetTrigger (false);
		
		Vector3 targetPosition = Vector3.zero;
		foreach (RaycastHit r_c_h in Physics.RaycastAll (Camera.main.ScreenPointToRay (Input.mousePosition)))
			if (r_c_h.collider.CompareTag ("Mouse Plane")) {
				managerPlayer.currentBody.animationController.LookAt (r_c_h.point);
				
			if (Input.GetButton ("Aim")) {
				managerPlayer.currentBody.characterMotor.isAiming = true;
			 		targetPosition = new Vector3 (
						(Input.mousePosition.x - (Screen.width / 2)) / (Screen.width / 2) * maxCameraOffset,
						(Input.mousePosition.y - (Screen.height / 2)) / (Screen.height / 2) * maxCameraOffset,
						0.0f
						);
			} else {
				managerPlayer.currentBody.characterMotor.isAiming = false;
			}
		}
		
		Camera.main.transform.localPosition = Vector3.Lerp (Camera.main.transform.localPosition, targetPosition, 0.25f);
		
		if (Input.GetButtonUp ("Swap Weapon")) {
			managerPlayer.currentBody.CurrentFirearm.SetTrigger (false);
			managerPlayer.currentBody.CurrentFirearm.SetTrigger (false);
			managerPlayer.currentBody.NextWeapon ();
		}
		
		if (Input.GetAxis("Mouse ScrollWheel") > 0.0f)
			managerPlayer.currentBody.CyclePower (true);
		if (Input.GetAxis("Mouse ScrollWheel") < 0.0f)
			managerPlayer.currentBody.CyclePower (false);
		if (Input.GetButton ("Fire2"))
			managerPlayer.currentBody.CurrentPower.Use (managerPlayer.currentBody);
		
		if (Input.GetKeyDown (KeyCode.Backspace))
			if (managerMap.currentMission.missionType == MissionType.Artifact && !managerMap.currentMission.mission.Completed ()) {
			
			} else {
				managerGame.MissionToUniverse ();
			}
	}
	
	public void TouchScreen () {
		
	}
	
	new void OnGUI () {
		if (!display) 
			return;
		GUILayout.BeginArea (new Rect (10, 10, 200,100));
		GUILayout.BeginVertical ("box");
		GUILayout.Label (managerMap.currentMission.mission.MissionStatus ());
		GUILayout.EndVertical ();
		GUILayout.EndArea ();
	}
}
