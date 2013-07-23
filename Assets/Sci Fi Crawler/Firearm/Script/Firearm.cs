using UnityEngine;
using System.Collections;

public class Firearm : Entity {
	public string description;
	
	public Controller controller;
	public void SetActive (bool active, Transform parent = null) {
		gameObject.SetActive (active);
		transform.parent = parent;
		transform.position = parent.position;
		transform.rotation = parent.rotation;
	} 
	
	public FiringMode primary;
	//public FiringMode secondary;
	
	public Transform firingTransform;
	
	public void SetTrigger (bool triggerPulled){//, bool isPrimary) {
		//if (isPrimary) 
			AlterTrigger (primary, triggerPulled);
		//else
		//	AlterTrigger (secondary, triggerPulled);
	}
			
	void AlterTrigger (FiringMode firingMode, bool triggerPulled) {
		firingMode.triggerPulled = triggerPulled;
		
		//if (!firingMode.isAutomatic)
		//	firingMode.currentBurstSize = firingMode.burstSize;
	}
	
	void Update () {
		UpdateFiringMode (primary);
		//UpdateFiringMode (secondary);
	}
	
	void UpdateFiringMode (FiringMode firingMode) {
		if (firingMode.currentFireRate > 0.0f) 
			firingMode.currentFireRate -= Time.deltaTime;
		
		if (!firingMode.triggerPulled)
			return;
		
		//if (!firingMode.isAutomatic && firingMode.currentBurstSize == 0)
		//	return;
		
		
		if (firingMode.currentFireRate <= 0.0f) {
			firingMode.currentFireRate += firingMode.fireRate;
			for (int i = 0; i < firingMode.burstSize; i++) {
				Projectile p = ((GameObject) Instantiate (
					firingMode.projectile, 
					firingTransform.position, 
					Quaternion.Euler (firingTransform.eulerAngles + new Vector3 (0.0f, Random.Range(-firingMode.accuracy, firingMode.accuracy),0.0f)))
					).GetComponent<Projectile> ();
				p.team = controller.team;
			}
		}
	}
	public string ToString () {
		string returnString = "";
		returnString += "Name: " + entityName + "\n";
		returnString += "Fire Rate: " + primary.fireRate + "\n";
		returnString += "Burst Size: " + primary.burstSize + "\n";
		returnString += "Accuracy: " + primary.accuracy + "\n";
		returnString += "Damage: " + primary.projectile.GetComponent<Projectile> ().attack.damage + "\n";
		returnString += "Damage Type: " + primary.projectile.GetComponent<Projectile> ().attack.damageType.ToString ();
		return returnString;
	}
}
[System.Serializable]
public class FiringMode {
	public bool triggerPulled;
	public float fireRate;
	//public bool isAutomatic;
	public int burstSize;
	
	public float accuracy;
	
	public float currentFireRate;
	//public int currentBurstSize;
	
	public GameObject projectile; 
}