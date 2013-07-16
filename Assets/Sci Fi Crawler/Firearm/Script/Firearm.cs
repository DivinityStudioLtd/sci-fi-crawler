using UnityEngine;
using System.Collections;

public class Firearm : MonoBehaviour {
	public Controller controller;
	public void SetActive (bool active, Transform parent = null) {
		gameObject.SetActive (active);
		if (active) {
			transform.parent = parent;
			transform.position = parent.position;
			transform.rotation = parent.rotation;
		} else {
		}
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
				Projectile p = ((GameObject) Instantiate (firingMode.projectile, firingTransform.position, firingTransform.rotation)).GetComponent<Projectile> ();
				p.team = controller.team;
			}
		}
	}
}
[System.Serializable]
public class FiringMode {
	public bool triggerPulled;
	public float fireRate;
	//public bool isAutomatic;
	public int burstSize;
	
	public float currentFireRate;
	//public int currentBurstSize;
	
	public GameObject projectile; 
}