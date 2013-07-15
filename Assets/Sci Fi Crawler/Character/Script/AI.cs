using UnityEngine;
using System.Collections;
using System.Collections.Generic;

abstract public class AI : MonoBehaviour {
	public void Start () {
		aiState = AIState.None;	
	}
	
	public Controller controller;
	
	#region Target Finding
	public float senseRate;
	protected float currentSense = 0.0f;
	public List<Controller> targets = new List<Controller> ();
	protected Controller CurrentTarget () {
		if (targets.Count > 0)
			return targets [0];
		return null;
	}
	protected void AddTarget (Controller c) {
		if (!targets.Contains (c))
			targets.Insert (0, c);	
	}
	protected void RemoveTarget (Controller c) {
		targets.Remove (c);	
	}
	protected void RemoveAllTargets () {
		targets = new List<Controller> ();	
	}
	#endregion
	
	#region Target Tile
	protected List<Tile> path = new List<Tile> ();
	protected int currentTileI = 0;
	protected Tile targetCurrentTile; 
	public float movementStateThreshold; 
	protected bool TargetMoved () {
		targetCurrentTile = CurrentTarget ().currentTile.currentTile;
		return path [path.Count - 1].x != targetCurrentTile.x || path[path.Count - 1].y != targetCurrentTile.y;
	}
	#endregion
	
	#region Attack
	public Attack attack;
	public float attackRate;
	protected float currentAttackDelay = 0.0f;
	#endregion
	
	public AIState aiState;
	public void Update () {
		if (currentAttackDelay > 0.0f)
			currentAttackDelay -= Time.deltaTime;
		if (currentSense > 0.0f) {
			currentSense -= Time.deltaTime;
		} else {
			SenseTargets ();
		}
		
		switch (aiState) {
			case AIState.None :	
				None (); break;
			case AIState.Idle :	
				Idle (); break;
			case AIState.Movement :	
				Movement (); break;
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
	
	virtual protected void Movement () {
	}
	
	virtual protected void Attack () {
	}
}
