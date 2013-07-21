using UnityEngine;
using System.Collections;

[AddComponentMenu("Utility/MathUtility")]
/// <summary>
/// Math Utility.
/// A static class that has mathimatical calculations needed for the game.
/// </summary>
public class MathUtility : Utility {
	public static Vector3 ProjectOntoPlane(Vector3 v, Vector3 normal) {
		return v-Vector3.Project(v,normal);
	}
}