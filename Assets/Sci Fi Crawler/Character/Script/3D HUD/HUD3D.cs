using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HUD3D : MonoBehaviour {
	public Transform healthBar;
	public Transform energyBar;
	public List<Transform> powerPositions;
	
	public void SetHealthBar (float current, float max) {
		SetBar (healthBar, current, max);
	}
	
	public void SetEnergyBar (float current, float max) {
		SetBar (energyBar, current, max);
	}
	
	void SetBar (Transform bar, float current, float max) {
		bar.localScale = new Vector3 (bar.localScale.x, current / max, bar.localScale.z);
	}
	
	public void SetPowerPositions (List<Power> powers) {
		for (int i = 0; i < powerPositions.Count; i++) {
			powers [i].SetLocal (powerPositions [i]);	
		}
	}
}