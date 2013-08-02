using UnityEngine;
using System.Collections;

[AddComponentMenu("Utility/PreferenceUtility")]
/// <summary>
/// Preference Utility.
/// Used to allow the players to save their preferences.
/// </summary>
public class PreferenceUtility : Utility {
	/*
	static string lastJoinIP = "Last Join IP"; 
	static public string LastJoinIP {
		get {return PlayerPrefs.GetString (lastJoinIP);}
		set {PlayerPrefs.SetString (lastJoinIP, value);}
	}
	static string lastHostPort = "Last Host Port"; 
	static public string LastHostPort {
		get {return PlayerPrefs.GetString (lastHostPort);}
		set {PlayerPrefs.SetString (lastHostPort, value);}
	}
	static string lastJoinPort = "Last Join Port";
	static public string LastJoinPort {
		get {return PlayerPrefs.GetString (lastJoinPort);}
		set {PlayerPrefs.SetString (lastJoinPort, value);}
	}
	
	static string outgoingPassword = "Outgoing Password"; 
	static public string OutgoingPassword {
		get {return PlayerPrefs.GetString (outgoingPassword);}
		set {PlayerPrefs.SetString (outgoingPassword, value);}
	}
	static string incomingPassword = "Incoming Password"; 
	static public string IncomingPassword {
		get {return PlayerPrefs.GetString (incomingPassword);}
		set {PlayerPrefs.SetString (incomingPassword, value);}
	}
	static string isListenerServer = "Is Listener Server"; 
	static public bool IsListenerServer {
		get {return bool.Parse (PlayerPrefs.GetString (isListenerServer));}
		set {PlayerPrefs.SetString (isListenerServer, value.ToString ());}
	}
	
	static string mapWidth = "Map Width";
	static public int MapWidth {
		get {return PlayerPrefs.GetInt (mapWidth);}
		set {PlayerPrefs.SetInt (mapWidth, value);}
	}
	static string mapHeight = "Map Height";
	static public int MapHeight {
		get {return PlayerPrefs.GetInt (mapHeight);}
		set {PlayerPrefs.SetInt (mapHeight, value);}
	}
	
	
*/
	#region Game Setup
	static string gameName = "Game Name"; 
	static public string GameName {
		get {return PlayerPrefs.GetString (gameName);}
		set {PlayerPrefs.SetString (gameName, value);}
	}
	#endregion
	
	#region Gameplay Setup
	static string playerName = "Player Name";
	static public string PlayerName {
		get {return PlayerPrefs.GetString (playerName);}
		set {PlayerPrefs.SetString (playerName, value);}
	}
	static string xSensitivity = "X Sensitivity";
	static public int XSensitivity {
		get {return PlayerPrefs.GetInt (xSensitivity);}
		set {PlayerPrefs.SetInt (xSensitivity, value);}
	}
	static string ySensitivity = "Y Sensitivity";
	static public int YSensitivity {
		get {return PlayerPrefs.GetInt (ySensitivity);}
		set {PlayerPrefs.SetInt (ySensitivity, value);}
	}
	#endregion
	
	#region Audio
	static string gameVolume = "Game Volume";
	static public float GameVolume {
		get {return PlayerPrefs.GetFloat (gameVolume);}
		set {PlayerPrefs.SetFloat (gameVolume, value);}
	}
	static string ambienceVolume = "Ambience Volume";
	static public float AmbienceVolume {
		get {return PlayerPrefs.GetFloat (ambienceVolume);}
		set {PlayerPrefs.SetFloat (ambienceVolume, value);}
	}
	static string musicVolume = "Music Volume";
	static public float MusicVolume {
		get {return PlayerPrefs.GetFloat (musicVolume);}
		set {PlayerPrefs.SetFloat (musicVolume, value);}
	}
	#endregion
	
	new public void Awake () {
		if (!PlayerPrefs.HasKey (PreferenceUtility.xSensitivity))
			XSensitivity = 10;
		if (!PlayerPrefs.HasKey (PreferenceUtility.ySensitivity))
			YSensitivity = 10;
		/*
		if (!PlayerPrefs.HasKey (PreferenceUtility.lastJoinIP))
			LastJoinIP = "127.0.0.1";
		if (!PlayerPrefs.HasKey (PreferenceUtility.lastHostPort))
			LastHostPort = "1234";
		if (!PlayerPrefs.HasKey (PreferenceUtility.lastJoinPort))
			LastJoinPort = "1234";
		
		if (!PlayerPrefs.HasKey (PreferenceUtility.gameName))
			GameName = "The Farm";	
		if (!PlayerPrefs.HasKey (PreferenceUtility.outgoingPassword))
			GameName = "";
		if (!PlayerPrefs.HasKey (PreferenceUtility.incomingPassword))
			GameName = "";	
		if (!PlayerPrefs.HasKey (PreferenceUtility.isListenerServer))
			IsListenerServer = false;	
		
		if (!PlayerPrefs.HasKey (PreferenceUtility.mapWidth))
			MapWidth = 3;
		if (!PlayerPrefs.HasKey (PreferenceUtility.mapHeight))
			MapHeight = 3;
		
		if (!PlayerPrefs.HasKey (PreferenceUtility.playerName))
			PlayerName = "Woodland Wander";
			*/
		
		if (!PlayerPrefs.HasKey (PreferenceUtility.gameVolume))
			GameVolume = 1.0f;
		if (!PlayerPrefs.HasKey (PreferenceUtility.ambienceVolume))
			AmbienceVolume = 1.0f;
		if (!PlayerPrefs.HasKey (PreferenceUtility.musicVolume))
			MusicVolume = 1.0f;
	}
}
