using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {
	public int damage;
	public DamageType damageType;
	public float pushPower;
	public float range;
	
	public bool lastAttackHit;
	public string lastHitEntertainer;
	
	public bool ranged;
	
	public void Attacking (Entity origin, int team, float sweepRadius) {
		RaycastHit[] raycastHits = Physics.SphereCastAll (transform.position, sweepRadius, transform.forward, range);
		
		if (raycastHits.Length > 0) {
			int i = ranged ? 0 : raycastHits.Length - 1;
			int change = ranged ? 1 : -1;
			
			for (; i >= 0 && i < raycastHits.Length;) {
				RaycastHit raycastHit = raycastHits[i];
				if (raycastHit.collider != null&& !raycastHit.collider.isTrigger) {
					Entity e = FindUtility.FindEntity (raycastHit.collider);
					if (e == origin) {
						i += change;
						continue;
					}
					
					if (Hit (e, team))
						return;
				}
				i += change;
			}	
		}
		lastAttackHit = false;
		lastHitEntertainer = "miss";
	}
	
	bool Hit (Entity e, int team) {
		Controller c = e as Controller;
		if (c != null && c.team != team) {
			c.character.ChangeHealth (damage, damageType);
		    //if (e.gameObject.rigidbody != null && !e.gameObject.rigidbody.isKinematic)
		    //    e.rigidbody.AddForce (transform.forward * pushPower);
			lastAttackHit = true;
			lastHitEntertainer = c.entityName;
			return true;
		}
		
		Tile t = e as Tile;
		if (t != null) {
			lastAttackHit = true;
			lastHitEntertainer = t.entityName;
			return true;
		}
		
		return false;
	}
}