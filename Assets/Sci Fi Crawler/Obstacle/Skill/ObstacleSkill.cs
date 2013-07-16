using UnityEngine;
using System.Collections;

public class ObstacleSkill : Obstacle {
	public SkillBlock obstacleSkill;
	
	public float obstacleHealth;
	public float currentObstacleHealth;
	
	public ParticleSystem deathParticle;
	
	//public float regenRate;
	
	public Transform popUp;
	
	void Start () {
		currentObstacleHealth = obstacleHealth;
	}
	
	public void Update () {
		currentObstacleHealth = Mathf.Clamp (currentObstacleHealth, 0.0f, obstacleHealth);	
		float ratio = currentObstacleHealth / obstacleHealth;
		popUp.localScale = new Vector3 (ratio, ratio, ratio);
	}
}
