using UnityEngine;
using System.Collections;

[AddComponentMenu("Utility/MessageUtility")]
/// <summary>
/// Message Utility.
/// This class provides standardize of the instruction messages used for network communication.
/// </summary>
public class MessageUtility : Utility {
	static public string destroy = "destroy";
	static public bool Destroy (string order) { return StringUtility.Compare (destroy, order); }
	
	static public string feedback = "feedback";
	static public bool Feedback (string order) { return StringUtility.Compare (feedback, order); }
	
	static public string popUp = "popUp";
	static public bool PopUp (string order) { return StringUtility.Compare (popUp, order); }
	
	static public string activate = "activate";
	static public bool Activate (string order) { return StringUtility.Compare (activate, order); }
	
	#region Game Manager Sync
	static public string setGameManagerSyncState = "setGameManagerSyncState";
	static public bool SetGameManagerSyncState (string order) { return StringUtility.Compare (setGameManagerSyncState, order); }
	#endregion
	
	#region Entity
	static public string setName = "setName";
	static public bool SetName (string order) { return StringUtility.Compare (setName, order); }
	static public string setDescription = "setDescription";
	static public bool SetDescription (string order) { return StringUtility.Compare (setDescription, order); }
	static public string setParent = "setParent";
	static public bool SetParent (string order) { return StringUtility.Compare (setParent, order); }
	#endregion
	
	#region Actor
	#endregion
	
	#region Character
	#endregion
	
	#region Attribute
	static public string setPoolAmount = "setPoolAmount";
	static public bool SetPoolAmount (string order) { return StringUtility.Compare (setPoolAmount, order); }
	static public string setAmount = "setAmount";
	static public bool SetAmount (string order) { return StringUtility.Compare (setAmount, order); }
	static public string changePoolAmount = "changePoolAmount";
	static public bool ChangePoolAmount (string order) { return StringUtility.Compare (changePoolAmount, order); }
	static public string changeAmount = "changeAmount";
	static public bool ChangeAmount (string order) { return StringUtility.Compare (changeAmount, order); }
	#endregion
	
	#region Map
	static public string setMapWidth = "setMapWidth";
	static public bool SetMapWidth (string order) { return StringUtility.Compare (setMapWidth, order); }
	static public string setMapHeight = "setMapHeight";
	static public bool SetMapHeight (string order) { return StringUtility.Compare (setMapHeight, order); }
	static public string setMapCompressMapTile = "setMapCompressMapTile";
	static public bool SetMapCompressMapTile (string order) { return StringUtility.Compare (setMapCompressMapTile, order); }
	static public string setAmbience = "setAmbience";
	static public bool SetAmbience (string order) { return StringUtility.Compare (setAmbience, order); }
	#endregion

	#region Manager
	static public string setManagerState = "setManagerState";
	static public bool SetManagerState (string order) { return StringUtility.Compare (setManagerState, order); }
	#endregion
	
	#region PlayClipAtPoint
	static public string setPlayClipAtPoint = "setPlayClipAtPoint";
	static public bool SetPlayClipAtPoint (string order) { return StringUtility.Compare (setPlayClipAtPoint, order); }
	#endregion
	
	#region Firearm
	static public string spawnBullet = "spawnBullet";
	static public bool SpawnBullet (string order) { return StringUtility.Compare (spawnBullet, order); }
	static public string setVisibilty = "setVisibilty";
	static public bool SetVisibilty (string order) { return StringUtility.Compare (setVisibilty, order); }
	static public string setFirearmController = "setFirearmController";
	static public bool SetFirearmController (string order) { return StringUtility.Compare (setFirearmController, order); }
	#endregion
	
	#region AnimationController
	static public string setAnimation = "setAnimation";
	static public bool SetAnimation (string order) { return StringUtility.Compare (setAnimation, order); }
	static public string setAnimationLocation = "setAnimationLocation";
	static public bool SetAnimationLocation (string order) { return StringUtility.Compare (setAnimationLocation, order); }
	#endregion
	
	#region Character
	static public string setPlayerName = "setPlayerName";
	static public bool SetPlayerName (string order) { return StringUtility.Compare (setPlayerName, order); }
	#endregion
	
	#region SlidingAreaState
	static public string setLocked = "setLocked";
	static public bool SetLocked (string order) { return StringUtility.Compare (setLocked, order); }
	static public string setSlidingAreaState = "setSlidingAreaState";
	static public bool SetSlidingAreaState (string order) { return StringUtility.Compare (setSlidingAreaState, order); }
	static public string destroySwitch = "destroySwitch";
	static public bool DestroySwitch (string order) { return StringUtility.Compare (destroySwitch, order); }
	#endregion
	
	#region Tunnel Light
	static public string setBreachedLights = "setBreachedLights";
	static public bool SetBreachedLights (string order) { return StringUtility.Compare (setBreachedLights, order); }
	static public string setDefaultLights = "setDefaultLights";
	static public bool SetDefaultLights (string order) { return StringUtility.Compare (setDefaultLights, order); }
	#endregion
}