using UnityEngine;
using System.Collections;

[AddComponentMenu("Utility/Utility")]
/// <summary>
/// Utility.
/// The parent class for the various utilities.
/// </summary>
abstract public class Utility : Linker {
	#region Linker Setup
	public void Awake () {
		JoinLinker ();
	}
	/// <summary>
	/// Joins the linker as a Utility.
	/// </summary>
	public void JoinLinker () {
		AddUtility (this);
	}
	#endregion
}