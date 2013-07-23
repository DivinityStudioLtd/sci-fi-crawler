using UnityEngine;
using System.Collections;

public class InterfaceSolar : Interface {
	public override bool SetDisplay (bool newDisplay) {
		if (pa != null)
			InterfaceUtility.SetCameraToTransform (pa.cameraPosition, true);
		else
			managerPlayer.ship.SetMainCameraToCP ();
		return base.SetDisplay (newDisplay);
	}
	
	public PlanetArm pa;
	
	public void Update () {
		if (!display)
			return;
		
		SolerViewPositioning ();
		
		if (Vector3.Distance (managerPlayer.ship.transform.position, managerMap.universe.transform.position) > (managerMap.universe.SolarBodiesOfType (SolarBodyType.Planet) + 3) * FactoryMap.PLANET_SPACING)
			managerPlayer.ship.transform.position = (managerMap.universe.SolarBodiesOfType (SolarBodyType.Planet) + 3) * FactoryMap.PLANET_SPACING * (managerPlayer.ship.transform.position - managerMap.universe.transform.position).normalized;
		
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.LinuxPlayer
			|| Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
			KeyboardMouse ();
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			TouchScreen ();
	}
	
	void SolerViewPositioning () {
		if (pa == null && managerPlayer.ship.currentPlanetArm != null) {
			pa = managerPlayer.ship.currentPlanetArm;
			InterfaceUtility.SetCameraToTransform (pa.cameraPosition, true);
			managerPlayer.ship.moveDirection = Vector3.zero;
			managerPlayer.ship.rigidbody.velocity = Vector3.zero;
			managerPlayer.ship.SetParent (pa.shipPosition, true);
			managerPlayer.ship.graphic.rotation = Quaternion.identity;
			
			managerPlayer.ship.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
		}
		if (pa != null && managerPlayer.ship.currentPlanetArm == null) {
			pa = null;
			managerPlayer.ship.SetMainCameraToCP ();
			managerPlayer.ship.SetParent (null, true);
			managerPlayer.ship.transform.position = new Vector3 (managerPlayer.ship.transform.position.x, 0.0f, managerPlayer.ship.transform.position.z);
			
			managerPlayer.ship.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		}
	}
	
	SolarBody selectedSolarBody;
	void KeyboardMouse () {
		if (pa == null) {
			managerPlayer.ship.moveDirection = new Vector3 (Input.GetAxis("Vertical"), 0.0f, -Input.GetAxis("Horizontal"));
		} else {
			if (Input.GetButtonDown ("Fire1")) {
				foreach (RaycastHit r_c_h in Physics.RaycastAll (Camera.mainCamera.ScreenPointToRay (Input.mousePosition))) {
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
	
	void OnGUI () {
		if (!display) 
			return;
		MenuGame ();
		
		if (selectedSolarBody != null) {
			MenuSolarBody ();
		}
	}
	
	public void MenuGame () {
        GUILayout.BeginArea (new Rect (10,10,200,100));
		if (GUILayout.Button ("Solar View"))
			managerInterface.SetInterface (interfaceSolar);
        if (GUILayout.Button ("Inventory"))
			managerInterface.SetInterface (interfaceInventory);
        if (GUILayout.Button ("Shop"))
			managerInterface.SetInterface (interfaceShop);
        if (GUILayout.Button ("Option"))
			managerInterface.SetInterface (interfaceMainMenu);
        GUILayout.EndArea ();
	}
	void MenuSolarBody () {
        GUILayout.BeginArea (new Rect (Screen.width-210,10,200,100));
	        GUILayout.Label("Location: " + selectedSolarBody.solarBodyType.ToString ());
			if (selectedSolarBody.mapMission == null) {
				GUILayout.Label("No mission at this location");
			} else {
		        GUILayout.Label("Mission: " + selectedSolarBody.mapMission.missionType.ToString ());
		        GUILayout.Label("Level: " + selectedSolarBody.mapMission.level);
		        if (GUILayout.Button ("Take Mission")) {
					managerGame.UniverseToMission (selectedSolarBody.mapMission);
				}
			}
        GUILayout.EndArea ();
	}
}