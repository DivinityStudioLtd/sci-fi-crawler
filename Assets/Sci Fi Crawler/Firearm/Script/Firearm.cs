using UnityEngine;
using System.Collections;

public class Firearm : Entity {
	public string description;
	
	public int credits;
	
	public Controller controller;
	public void SetActive (bool active, Transform parent = null) {
		gameObject.SetActive (active);
		transform.parent = parent;
		transform.position = parent.position;
		transform.rotation = parent.rotation;
	} 
	
	public FiringMode baseStats;
	public FiringMode variation;
	
	public FiringMode stats;
	
	public Transform firingTransform;
	
	public void RandomizeStats () {
		stats.fireRate = Random.Range (baseStats.fireRate - variation.fireRate, baseStats.fireRate + variation.fireRate);
		stats.burstSize = Random.Range (baseStats.burstSize - variation.burstSize, baseStats.burstSize + variation.burstSize + 1);
		stats.accuracy = Random.Range (baseStats.accuracy - variation.accuracy, baseStats.accuracy + variation.accuracy);
		stats.damage = Random.Range (baseStats.damage - variation.damage, baseStats.damage + variation.damage + 1);
		stats.damageType = baseStats.damageType;
	}
	
	public void SetTrigger (bool triggerPulled){
		this.triggerPulled = triggerPulled;
	}
	
	void Update () {
		UpdateFiringMode (stats);
	}
	
	void UpdateFiringMode (FiringMode firingMode) {
		if (currentFireRate > 0.0f) 
			currentFireRate -= Time.deltaTime;
		
		if (!triggerPulled)
			return;
		
		if (currentFireRate <= 0.0f) {
			currentFireRate += firingMode.fireRate;
			for (int i = 0; i < firingMode.burstSize; i++) {
				Projectile p = ((GameObject) Instantiate (
					projectile, 
					firingTransform.position, 
					Quaternion.Euler (firingTransform.eulerAngles + new Vector3 (0.0f, Random.Range(-firingMode.accuracy, firingMode.accuracy),0.0f)))
					).GetComponent<Projectile> ();
				p.team = controller.team;
				p.attack.damage = stats.damage;
				p.attack.damageType = stats.damageType;
			}
		}
	}
	public string ToString () {
		string returnString = "";
		returnString += "Name: " + entityName + "\n";
		returnString += "Fire Rate: " + string.Format("{0:0.00}", stats.fireRate) + "\n";
		returnString += "Burst Size: " + stats.burstSize + "\n";
		returnString += "Accuracy: " + string.Format("{0:0.00}", stats.accuracy) + "\n";
		returnString += "Damage: " + string.Format("{0:0.00}", stats.damage) + "\n";
		returnString += "Damage Type: " + stats.damageType.ToString ();
		return returnString;
	}
	public bool triggerPulled;
	
	public float currentFireRate;
	public GameObject projectile; 
}

[System.Serializable]
public class FiringMode {
	public float fireRate;
	public int burstSize;
	
	public float accuracy;
	
	public float damage;
	public DamageType damageType;
	
}