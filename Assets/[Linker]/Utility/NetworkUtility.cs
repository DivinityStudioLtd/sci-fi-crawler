using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

[AddComponentMenu("Utility/NetworkUtility")]
/// <summary>
/// Network Utility.
/// This is a static class that provides networks utilities for the game.
/// </summary>
public class NetworkUtility : Utility {
	static public bool isServer {
		get {
			return Network.isServer;
		}
	}

	public static void SetMinimumAllocatableViewIDs (int viewIDs) {
		Network.minimumAllocatableViewIDs = viewIDs;
	}
	
	#region Master Server
	/// <summary>
	/// Sets the Master Server.
	/// </summary>
	/// <param name='newIp'>
	/// New ip.
	/// </param>
	/// <param name='newPort'>
	/// New port.
	/// </param>
	static public void SetMasterServer (string newIp, int newPort) {
		MasterServer.ipAddress = newIp;
		MasterServer.port = newPort;
	}
	
	/// <summary>
	/// Refreshes the of game found on the Master Server.
	/// </summary>
	/// <param name='gameName'>
	/// Game name.
	/// </param>
	static public void RefreshGameList (string gameName) {
		MasterServer.ClearHostList ();
		MasterServer.RequestHostList (gameName);	
	}
	
	/// <summary>
	/// Returns a list of game that have been collected from the Master Server.
	/// </summary>
	/// <returns>
	/// The games.
	/// </returns>
	static public HostData[] QueryGames () {
		return MasterServer.PollHostList ();
	}
	
	/// <summary>
	/// Registers the game on the Master Server.
	/// </summary>
	/// <param name='gameName'>
	/// Game name.
	/// </param>
	/// <param name='roomName'>
	/// Room name.
	/// </param>
	static public void RegisterGame (string gameName, string roomName) {
 		MasterServer.RegisterHost (gameName, roomName);
	}
	
	/// <summary>
	/// Unregisters the game on the Master Server.
	/// </summary>
	static public void UnregisterGame () {
		MasterServer.UnregisterHost ();
	}
	#endregion
	
	#region Join
	/// <summary>
	/// Joins the game.
	/// </summary>
	/// <param name='ip'>
	/// Ip.
	/// </param>
	/// <param name='port'>
	/// Port.
	/// </param>
	static public void JoinGame (string ip, int port, string password) {
		Network.Connect (ip, port, password); 
	}
	
	/// <summary>
	/// Joins the game.
	/// </summary>
	/// <param name='h'>
	/// H.
	/// </param>
	static public void JoinGame (HostData h, string password) {
		JoinGame (h.ip[0], h.port, password);
	}
	#endregion
	
	#region Host
	/// <summary>
	/// Hosts the game.
	/// </summary>
	/// <param name='port'>
	/// Port.
	/// </param>
	/// <param name='playerCount'>
	/// Player count.
	/// </param>
	static public void HostGame (int port, int playerCount, string password) {
        Network.incomingPassword = password;
		Network.InitializeServer (playerCount, port, true);
	}
	#endregion
	
	#region Disconnect
	/// <summary>
	/// Disconnect from the current game.
	/// </summary>
	static public void Disconnect () {
		Network.Disconnect ();
	}
	#endregion
	
	#region Instantiate
	/// <summary>
	/// Networks an instantiation. This variant spawns the gameobject with a zero transform
	/// </summary>
	/// <returns>
	/// The instantiated gameobject.
	/// </returns>
	/// <param name='toBeSpawned'>
	/// The prefab you would like to be spawned.
	/// </param>
	static public GameObject NetworkInstantiate (GameObject toBeSpawned) {
		return NetworkInstantiate (toBeSpawned, Vector3.zero, Quaternion.identity, 0);
	}
	
	/// <summary>
	/// Networks an instantiation. This variant
	/// </summary>
	/// <returns>
	/// The instantiate.
	/// </returns>
	/// <param name='toBeSpawned'>
	/// To be spawned.
	/// </param>
	/// <param name='vector3'>
	/// The position to spawn the prefab at.
	/// </param>
	/// <param name='quaternion'>
	/// The rotation to spawn the prefab at.
	/// </param>
	/// <param name='group'>
	/// The network layer to spawn the prefab on.
	/// </param>
	static public GameObject NetworkInstantiate (GameObject toBeSpawned, Vector3 vector3, Quaternion quaternion, int group = 0) {
		return Network.Instantiate (toBeSpawned, vector3, quaternion, group) as GameObject;
	}
	#endregion
}