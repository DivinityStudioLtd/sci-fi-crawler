using UnityEngine;
using System.Collections;

public class InterfaceSolar : Interface {
	public override bool SetDisplay (bool newDisplay) {
		
		if (pa != null) {
			InterfaceUtility.SetCameraToTransform (pa.cameraPosition, true);
		} else {
			managerPlayer.ship.SetMainCameraToCP (true);
		}
		managerPlayer.ship.moveDirection = Vector3.zero;
		managerGame.mapGUI.gameObject.SetActive (newDisplay);
		return base.SetDisplay (newDisplay);
	}
	
	public PlanetArm pa;
	
	int buttonBar;
	
	public void Update () {
		buttonBar = (Screen.width - 60) / 5;
		if (!display)
			return;
		
		managerGame.mapGUI.transform.position = managerPlayer.ship.transform.position + new Vector3 (0, 40.0f, 0);
		
		SolerViewPositioning ();
		
		if (managerMap.universe == null)
			return;
		if (managerMap.managerMapState != ManagerMapState.Waiting)
			return;
		
		if (Vector3.Distance (managerPlayer.ship.transform.position, managerMap.universe.transform.position) > (managerMap.universe.SolarBodiesOfType (SolarBodyType.Planet) + 3) * FactoryMap.PLANET_SPACING) {
			Vector3 position = (managerMap.universe.SolarBodiesOfType (SolarBodyType.Planet) + 3) * FactoryMap.PLANET_SPACING * (managerPlayer.ship.transform.position - managerMap.universe.transform.position).normalized;
			managerPlayer.ship.transform.position = position;
		}
		
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.LinuxPlayer
			|| Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
			KeyboardMouse ();
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			TouchScreen ();
		
		Camera.main.transform.localPosition = Vector3.Lerp (Camera.main.transform.localPosition, Vector3.zero, 0.1f);
		Camera.main.transform.localRotation = Quaternion.Lerp (Camera.main.transform.localRotation, Quaternion.identity, 0.1f);
	}
	
	void SolerViewPositioning () {
		if (pa == null && managerPlayer.ship.currentPlanetArm != null) {
			PlanetArmPosition ();
		}
		if (pa != null && managerPlayer.ship.currentPlanetArm == null) {
			SolarPosition ();
		}
	}
	
	void PlanetArmPosition () {
		pa = managerPlayer.ship.currentPlanetArm;
		InterfaceUtility.SetCameraToTransform (pa.cameraPosition, false);
		
		managerPlayer.ship.moveDirection = Vector3.zero;
		managerPlayer.ship.rotationDirection = 0.0f;
		managerPlayer.ship.rigidbody.velocity = Vector3.zero;
		managerPlayer.ship.SetParent (pa.shipPosition, true);
		managerPlayer.ship.graphic.rotation = Quaternion.identity;
		
		managerPlayer.ship.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
		managerPlayer.ship.trailRenderer.time = 0.0f;
	}
	void SolarPosition () {
		pa = null;
		
		managerPlayer.ship.SetMainCameraToCP (false);
		
		managerPlayer.ship.SetParent (null, true);
		managerPlayer.ship.transform.position = new Vector3 (managerPlayer.ship.transform.position.x, 0.0f, managerPlayer.ship.transform.position.z);
		
		managerPlayer.ship.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		managerPlayer.ship.trailRenderer.time = Ship.TRAIL_LENGTH;
	}
	
	SolarBody selectedSolarBody;
	void KeyboardMouse () {
		if (pa == null) {
			managerPlayer.ship.rotationDirection = Input.GetAxis ("Horizontal");
			managerPlayer.ship.moveDirection = managerPlayer.ship.graphic.transform.forward * Input.GetAxis ("Vertical");
		} else {
			if (Input.GetButtonDown ("Fire1")) {
				foreach (RaycastHit r_c_h in Physics.RaycastAll (Camera.main.ScreenPointToRay (Input.mousePosition))) {
					Entity e = FindUtility.FindEntity (r_c_h.collider);
					
					if (e == null)
						continue;
					
					SolarBody sb = e.GetComponent <SolarBody> ();
					if (sb != null) {
						Transform t = sb.transform;
						while (t.parent != null) {
							if (t == pa.transform) {
								selectedSolarBody = sb;
							}
							t = t.parent;	
						}
					}
					
					Ship s = e.GetComponent<Ship> ();
					if (s != null) {
						s.currentPlanetArm = null;
						selectedSolarBody = null;
					}
				}
			}
		}
	}
	
	void TouchScreen () {
	
	}
	
	new void OnGUI () {
		if (!display) 
			return;
		MenuGame ();
		
		if (selectedSolarBody != null)
			MenuSolarBody ();
	}
	
	public void MenuGame () {
        GUILayout.BeginArea (new Rect (10,10,Screen.width - 20,100));
		GUILayout.BeginHorizontal ("box");
		if (GUILayout.Button ("Solar View", GUILayout.Width (buttonBar)))
			managerInterface.SetInterface (interfaceSolar);
        if (GUILayout.Button ("Inventory", GUILayout.Width (buttonBar)))
			managerInterface.SetInterface (interfaceInventory);
		
		GUILayout.FlexibleSpace ();
		GUILayout.Label (managerPlayer.playerInventory.credit.ToString ());
		GUILayout.FlexibleSpace ();
        if (GUILayout.Button ("Shop", GUILayout.Width (buttonBar)))
			managerInterface.SetInterface (interfaceShop);
        if (GUILayout.Button ("Option", GUILayout.Width (buttonBar))) {
			interfaceMainMenu.mainMenuEnum = MainMenuEnum.MainMenu;
			managerInterface.SetInterface (interfaceMainMenu);
		}
		GUILayout.EndHorizontal ();
        GUILayout.EndArea ();
	}
	void MenuSolarBody () {
        GUILayout.BeginArea (new Rect (Screen.width-210,40,200,140));
		GUILayout.BeginVertical ("box");
		    GUILayout.Label("Location: " + selectedSolarBody.solarBodyType.ToString ());
			switch (selectedSolarBody.solarBodyType) {
			case SolarBodyType.Moon:
			case SolarBodyType.Station:
			case SolarBodyType.Planet:
				if (selectedSolarBody.mapMission == null) {
					GUILayout.Label("No mission at this location");
				} else {
			        GUILayout.Label("Mission: " + selectedSolarBody.mapMission.missionType.ToString ());
			        GUILayout.Label("Level: " + selectedSolarBody.mapMission.level);
			        GUILayout.Label("Reward: " + selectedSolarBody.mapMission.credits);
			        if (GUILayout.Button ("Take Mission"))
						managerGame.UniverseToMission (selectedSolarBody.mapMission);
				}
				break;
			case SolarBodyType.JumpGate:
				//if (managerPlayer.hasArtefact) {
					GUILayout.Label("Artefact Collected");
			        if (GUILayout.Button ("Make Jump")) {
						SolarPosition ();
						managerPlayer.ship.currentPlanetArm = null;
						managerPlayer.ship.transform.position = new Vector3 (managerPlayer.ship.transform.position.x, 100.0f, managerPlayer.ship.transform.position.z);
						managerGame.UniverseExit ();
					}
				//} else {
					GUILayout.Label("Artefact Not Collected");
				//}
				break;
			}
		GUILayout.EndVertical ();
        GUILayout.EndArea ();
	}
}