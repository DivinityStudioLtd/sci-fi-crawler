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
	
	/// <summary>
	/// records the screens measurements.
	/// </summary>
	static void MeasureScreen () {
		w = UnityEngine.Screen.width;	
		h = UnityEngine.Screen.height;	
	}
	/// <summary>
	/// Returns the screens width divided by a given value.
	/// </summary>
	/// <param name='divisor'>
	/// Divisor.
	/// </param>
	static public float ScreenWidthDivided (float divisor) { return w / divisor; }
	/// <summary>
	/// Returns the screens height divided by a given value.
	/// </summary>
	/// <param name='divisor'>
	/// Divisor.
	/// </param>
	static public float ScreenHeightDivided (float divisor) { return h / divisor; }
	
	/// <summary>
	/// Gives the rectangle with a given width and height.
	/// </summary>
	/// <returns>
	/// The rect.
	/// </returns>
	/// <param name='width'>
	/// Width.
	/// </param>
	/// <param name='height'>
	/// Height.
	/// </param>
	static public Rect CenteredRect (float width, float height) {
		return new Rect (ScreenWidthDivided (2.0f) - width / 2.0f, ScreenHeightDivided (2.0f) - height / 2.0f, width, height);
	}
	#endregion
}