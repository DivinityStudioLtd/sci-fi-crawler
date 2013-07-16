using UnityEngine;
using System.Collections;

public class ObstacleSkill : Obstacle {
	public SkillBlock obstacleSkill;
	
	public float obstacleHealth;
	public float currentObstacleHealth;
	
	public float regenRate;
	
	public Transform popUp;
	
	public void Update () {
		currentObstacleHealth = Mathf.Clamp (currentObstacleHealth + Time.deltaTime, 0.0f, obstacleHealth);	
		float ratio = currentObstacleHealth / obstacleHealth;
		popUp.localScale = new Vector3 (ratio, ratio, ratio);
	}
}
