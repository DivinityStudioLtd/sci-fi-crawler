using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Manager/Manager")]
/// <summary>
/// Manager.
/// The parent class for the various manager.
/// </summary>
abstract public class Manager : Linker {
	public ManagerStateEnum state_e;
	
	/// <summary>
	/// On awake, call the base awake, and then collect the default children entities.
	/// </summary>
	public void Awake () {
		JoinLinker ();
		entities = new List<Entity> ();
		CollectEntites ();
		SetState (ManagerStateEnum.Start);
		state_e = ManagerStateEnum.Start;
	}
	
	public void LateUpdate () {
		for (int i = 0; i < entities.Count; i++)
			if (entities [i] == null)
				entities.RemoveAt (i);
	}
	
	#region Linker Setup
	/// <summary>
	/// Joins the linker as a Manager.
	/// </summary>
	public void JoinLinker () {
		AddManager (this);
	}
	
	/// <summary>
	/// Collects the default entities.
	/// </summary>
	protected void CollectEntites () {
		foreach (Transform t in transform)
			if (t.GetComponent ("Entity") != null)
				entities.Add (t.GetComponent ("Entity") as Entity);
	}
	#endregion
	
	public void Update () {
		switch (state_e) {
		case ManagerStateEnum.Start :
			ManagerStart ();
			break;
		case ManagerStateEnum.Working :
			ManagerWorking ();
			break;
		case ManagerStateEnum.Finish :
			ManagerFinishing ();
			break;
		}
	}
	
	virtual public void ManagerStart () {
		state_e = ManagerStateEnum.Working;
	}
	
	virtual public void ManagerWorking () {
		state_e = ManagerStateEnum.Finish;
	}
	
	virtual public void ManagerFinishing () {
		state_e = ManagerStateEnum.None;
	}
	
	#region Entity Management
	public List<Entity> entities;
	/// <summary>
	/// Gets an entity by a given name.
	/// </summary>
	/// <returns>
	/// The entity.
	/// </returns>
	/// <param name='s'>
	/// S.
	/// </param>
	public Entity GetEntity (string s) {
		foreach (Entity entity in entities) 
			if (entity != null)
				if (StringUtility.Compare (s, entity.entityName))
					return entity;
		return null;
	}
	
	/// <summary>
	/// Gets an entity in a particular position.
	/// </summary>
	/// <returns>
	/// The entity.
	/// </returns>
	/// <param name='i'>
	/// I.
	/// </param>
	public Entity GetEntity (int i) {
		return entities [i];
		/*
		foreach (Entity entity in entities)
			if (entity != null)
				if (entity.entityID == i)
					return entity;
		return null;
		*/
	}
	
	/// <summary>
	/// Gets the index/ID of a particular entity.
	/// </summary>
	/// <returns>
	/// The entity I.
	/// </returns>
	/// <param name='e'>
	/// E.
	/// </param>
	public int GetEntityID (Entity e) {
		return entities.IndexOf (e);	
	}
	
	/// <summary>
	/// Adds the entity.
	/// </summary>
	/// <param name='e'>
	/// E.
	/// </param>
	public void AddEntity (Entity e) {
		entities.Add (e);	
	}
	
	/// <summary>
	/// Removes the entity.
	/// </summary>
	/// <param name='e'>
	/// E.
	/// </param>
	public void RemoveEntity (Entity e) {
		entities.Remove (e);	
	}
	
	#endregion
	/// <summary>
	/// Sets the state.
	/// </summary>
	/// <param name='newNetworkManagerEnum'>
	/// New network manager enum.
	/// </param>
	public void SetState (ManagerStateEnum newNetworkManagerEnum) {
		state_e = newNetworkManagerEnum;
	}
	
	#region Network View
	[RPC]
	public void Message (string message) {
		string[] messages = message.Split (' ');
		if (messages.Length == 0)
			return;
		
		string order = messages[0];
		
		if (MessageUtility.SetManagerState (order)) {
			state_e = (ManagerStateEnum) int.Parse (messages[1]);
		}
	}
	#endregion
}