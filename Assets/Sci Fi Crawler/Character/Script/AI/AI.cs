using UnityEngine;
using System.Collections;
using System.Collections.Generic;

abstract public class AI : Entity {
	public List<GameObject> possibleFirearms;
	public List<GameObject> possiblePowers;
	
	public void Start () {
		controller.characterMotor.characterController.Move (Vector3.forward);	
		aIState = AIState.Idle;	
	}
	
	public Controller controller;
	
	public int aILevel;
	
	#region Target Finding
	public float senseRate;
	protected float currentSense = 0.0f;
	public float senseRadius;
	public List<Controller> targets = new List<Controller> ();
	protected Controller CurrentTarget () {
		if (targets.Count > 0)
			return targets [0];
		return null;
	}
	protected bool AddTarget (Controller c) {
		if (!targets.Contains (c)) {
			targets.Insert (0, c);	
			return true;
		}
		return false;
	}
	protected bool RemoveTarget (Controller c) {
		targets.Remove (c);	
		return true;
	}
	protected void RemoveAllTargets () {
		targets = new List<Controller> ();	
	}
	#endregion
	
	#region Target Tile
	public List<Tile> path = new List<Tile> ();
	protected int currentTileI = 0; 
	public float movementStateThreshold; 
	protected bool TargetMoved () {
		if (CurrentTarget () == null)
			return false;
		if (path.Count == 0)
			return true;
		Tile targetCurrentTile = CurrentTarget ().currentTile.currentTile;
		return path [path.Count - 1].x != targetCurrentTile.x || path[path.Count - 1].y != targetCurrentTile.y;
	}
	#endregion
	
	#region Attack
	public Attack attack;
	public float attackRate;
	protected float currentAttackDelay = 0.0f;
	#endregion
	
	public AIState aIState;
	public void Update () {
		if (controller.character.currentHealth == 0) {
			controller.CurrentFirearm.SetTrigger (false);
			Destroy (this.gameObject, 1.0f);
			Destroy (this);
			return;
		}
		
		if (aIState == AIState.None)
			return;
		
		if (currentAttackDelay > 0.0f)
			currentAttackDelay -= Time.deltaTime;
		if (currentSense > 0.0f) {
			currentSense -= Time.deltaTime;
		} else {
			SenseTargets ();
		}
		
		switch (aIState) {
			case AIState.None :	
				None (); break;
			case AIState.Idle :	
				Idle (); break;
			case AIState.Move :
				Move (); break;
			case AIState.Attack :	
				Attack (); break;
		}
		/*
		if (!characterMotor.MovingGround) {
			animationController.ControlAnimation (AnimationType.Animate, "idle");
		} else {
			animationController.ControlAnimation (AnimationType.Animate, "run");
		}*/
	}
	
	virtual protected void SenseTargets () {
	}
	
	virtual protected void FindTargets () {
	}
	
	virtual protected void None () {
	}
	
	virtual protected void Idle () {
	}
	
	virtual protected void Move () {
	}
	
	virtual protected void Attack () {
	}
	/*
	public bool TileBlockingTarget () {
		if (targets.Count == 0)
			return false;
		
		RaycastHit[] hits = Physics.RaycastAll (
			controller.transform.position, 
			CurrentTarget ().transform.position - controller.transform.position, 
			Vector3.Distance (controller.transform.position, CurrentTarget ().transform.position)
			);
		foreach (RaycastHit hit in hits) {
			if (hit.collider.isTrigger)
				continue;
			Entity e = FindUtility.FindEntity (hit.collider);
			
			if (e == null)
				continue;
			
			if (e as Tile != null)
				if ((e as Tile).mapTileType == MapTileType.Wall)
					return true;
		}
		return false;
	}*/
}
