using UnityEngine;
using System.Collections;

public class Firearm : Entity {
	public string description;
	
	public FirearmType firearmType;
	
	void Start () {
		firingTransform.renderer.enabled = false;	
	}
	
	public int credits;
	
	public Controller controller;
	public void SetActive (bool active, Transform parent = null) {
		gameObject.SetActive (active);
		
		transform.parent = parent;
		transform.position = parent.position;
	} 
	
	public FiringMode baseStats;
	public FiringMode variation;
	
	public FiringMode stats;
	
	public Transform firingTransform;
	
	public void RandomizeStats (int level) {
		stats.fireRate = Mathf.Clamp (
			Random.Range (
				baseStats.fireRate - (variation.fireRate * level), 
				baseStats.fireRate + variation.fireRate
			), 
			0.0f, 
			float.PositiveInfinity
		);
		
		stats.burstSize = Random.Range (baseStats.burstSize - variation.burstSize, baseStats.burstSize + (variation.burstSize * level)+ 1);
		
		stats.accuracy = Mathf.Clamp (
			Random.Range (
				baseStats.accuracy - (variation.accuracy * level), 
				baseStats.accuracy + variation.accuracy
			), 
			0.0f, 
			float.PositiveInfinity
		);
		
		stats.damage = Random.Range (baseStats.damage - (variation.damage * level), baseStats.damage + variation.damage);
		stats.damageType = baseStats.damageType;
	}
	
	public void SetTrigger (bool triggerPulled){
		this.triggerPulled = triggerPulled;
	}
	
	void Update () {
		if (controller != null)
			transform.rotation = controller.animationController.transform.rotation;
		UpdateFiringMode (stats);
	}
	
	void UpdateFiringMode (FiringMode firingMode) {
		if (currentFireRate > 0.0f) 
			currentFireRate -= Time.deltaTime;
		
		if (!triggerPulled)
			return;
		
		if (currentFireRate <= 0.0f) {
			currentFireRate += firingMode.fireRate;
			
			float accuracyModifier = 1.0f;
			
			if (controller.characterMotor.characterController.velocity != Vector3.zero)
				accuracyModifier += 0.33f;
			
			if (controller.characterMotor.isAiming)
				accuracyModifier -= 0.33f;
			
			if (controller.character.HasWeaponProficiency (firearmType))
				accuracyModifier -= 0.33f;
			else
				accuracyModifier += 0.33f;
			
			for (int i = 0; i < firingMode.burstSize; i++) {
				Projectile p = ((GameObject) Instantiate (
					projectile, 
					firingTransform.position, 
					Quaternion.Euler (firingTransform.eulerAngles + new Vector3 (0.0f, Random.Range(-firingMode.accuracy, firingMode.accuracy) * accuracyModifier,0.0f)))
					).GetComponent<Projectile> ();
				p.team = controller.team;
				p.attack.damage = stats.damage;
				p.attack.damageType = stats.damageType;
			}
		}
	}
	public override string ToString () {
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

public enum FirearmType {
	Pistol,
	SMG,
	Rifle,
	Sniper,
	Shotgun,
	LMG,
	Explosive,
	Chemical,
	AR,
	Laser
}