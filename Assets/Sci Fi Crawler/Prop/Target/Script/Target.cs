using UnityEngine;
using System.Collections;

public class Target : Entity {
	public float maxHealth;
	public float currentHealth;
	
	public int team;
	
	public void ChangeHealth (float change) {
		currentHealth = Mathf.Clamp (currentHealth + change, 0.0f, maxHealth);
	}
	
	public void Start () {
		currentHealth = maxHealth;	
	}
	
	void Update () {
		if (currentHealth == 0.0f)
			Destroy (gameObject);
	}
	/*
	public Tile currentTile {
		get {
			
		}
	}*/
}
