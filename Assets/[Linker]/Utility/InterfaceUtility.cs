using UnityEngine;
using System.Collections;

[AddComponentMenu("Utility/InterfaceUtility")]
/// <summary>
/// Interface Utility.
/// A class class that contains methods and variable for Interface and GUI drawing. 
/// </summary>
public class InterfaceUtility : Utility {
	static public int p = 5;
	static public int selectButton = 60;
	static public int changeButton = 30;
	
	void Update () {
		MeasureScreen ();	
	}
	
	#region Screen Measurements
	static public int
		w,	h;
	
	static void MeasureScreen () {
		w = UnityEngine.Screen.width;	
		h = UnityEngine.Screen.height;	
	}
	
	static public float ScreenWidthDivided (float divisor) { return w / divisor; }

	static public float ScreenHeightDivided (float divisor) { return h / divisor; }
	
	static public Rect CenteredRect (float width, float height) {
		return new Rect (ScreenWidthDivided (2.0f) - width / 2.0f, ScreenHeightDivided (2.0f) - height / 2.0f, width, height);
	}
	
	static public void SetCameraToTransform (Transform newParent, bool align) {
		if (newParent == null) {
			Camera.main.transform.parent = null;
		} else {
			Camera.main.transform.parent = newParent;
			if (align) {
				Camera.main.transform.localPosition = Vector3.zero;
				Camera.main.transform.localRotation = Quaternion.identity;
			}
		}
	}
	#endregion
}