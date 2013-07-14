using UnityEngine;
using System.Collections;

[AddComponentMenu("Factory/Factory")]
/// <summary>
/// Factory.
/// The parent class for various factories.
/// </summary>
abstract public class Factory : Linker {
	#region Linker Setup
	public void Awake () {
		JoinLinker ();
	}
	/// <summary>
	/// Joins the linker as a Factory.
	/// </summary>
	public void JoinLinker () {
		AddFactory (this);
	}
	#endregion
}
