using UnityEngine;
using System.Collections;

abstract public class Power : MonoBehaviour {
	public Transform popup;
	
	float currentCoolDown;
	public float coolDown;
	
	public int energyCost;
	
	void Update () {
		if (!CanUse ())
			currentCoolDown = Mathf.Clamp (currentCoolDown - Time.deltaTime, 0.0f, coolDown);
		transform.localScale = new Vector3 (1.0f, 1.0f, (coolDown - currentCoolDown) / coolDown);
		popup.renderer.materials[0].mainTextureScale = new Vector2 (1.0f, (coolDown - currentCoolDown) / coolDown);
	}
	
	public void SetLocal (Transform local) {
		transform.position = local.position;
		transform.rotation = local.rotation;
	}
	
	public  bool CanUse () {
		return !(currentCoolDown > 0.0f);
	}
	
	public void SetUsed (Controller user) {
		user.character.ChangeEnergy (-energyCost);
		currentCoolDown = coolDown;	
	}
	
	public bool EnoughEnergy (Controller user) {
		return user.character.currentEnergy > energyCost;
	}
	
	public void Use (Controller user) {
		if (user != null)
		if (!CanUse ())
			return;
		if (!EnoughEnergy (user))
			return;
		if (Effect (user))
			SetUsed (user);
	}
	public abstract bool Effect (Controller user);
}