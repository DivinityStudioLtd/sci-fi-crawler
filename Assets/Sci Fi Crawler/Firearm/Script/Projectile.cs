using UnityEngine;
using System.Collections;

public class Projectile : Entity {
	public TrailRenderer trailRenderer;
	public float trailLength;
	
	public float speed;
	public float distance;
	public float currentDistance;
	
	public int team;
	public Attack attack; 
	public float sweepRadius;
	
	void Start () {
		if (trailRenderer != null) {
			trailRenderer.time = trailLength / speed;
			trailRenderer.materials [0].mainTextureScale = new Vector2 (trailLength, 1.0f);
		}
		currentDistance = 0.0f;
	}
	
	public void Update () {
		float distanceThisUpdate = speed * Time.deltaTime;
		
		attack.range = distanceThisUpdate;
		attack.Attacking (this, team, sweepRadius);
		if (attack.lastAttackHit)
			Destroy (this.gameObject);
		
		transform.position += transform.forward * distanceThisUpdate;
		currentDistance += distanceThisUpdate;
		
		if (currentDistance > distance)
			Destroy (this.gameObject);
	}
}
