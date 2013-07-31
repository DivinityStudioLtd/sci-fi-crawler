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
	
	public Transform cameraPosition;
	public float farClipPlane = 0.02f;
	
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
				Camera.main.transform.parent = cameraPosition;
				Camera.main.transform.position = cameraPosition.position;
				Camera.main.transform.rotation = cameraPosition.rotation;
			}
			Camera.main.farClipPlane = farClipPlane;
			Camera.main.transform.GetChild (0).camera.farClipPlane = farClipPlane;
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