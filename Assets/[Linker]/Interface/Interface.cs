using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Interface/Interface")]
/// <summary>
/// Interface.
/// The parent class for the various interface.
/// </summary>
abstract public class Interface : Linker {
	public List<GUISkin> guiSkins;
	
	protected List<Vector2> scrollPositions = new List<Vector2> ();
	
	public InterfaceEnum interfaceEnum;
	
	public Transform cameraPosition;
	
	#region Linker Setup
	public void Awake () {
		JoinLinker ();
	}
	
	/// <summary>
	/// Joins the linker as an Interface.
	/// </summary>
	public void JoinLinker () {
		AddInterface (this);
	}
	#endregion
	
	public bool display;
	virtual public bool SetDisplay (bool newDisplay) {
		if (newDisplay) {
			if (cameraPosition != null) {
				Camera.mainCamera.transform.parent = cameraPosition;
				Camera.mainCamera.transform.position = cameraPosition.position;
				Camera.mainCamera.transform.rotation = cameraPosition.rotation;
			}
		}
		display = newDisplay; 
		return display; 
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	public void OnGUI () {
		if (!display) 
			return;

	}
}