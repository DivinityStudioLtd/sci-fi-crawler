using UnityEngine;
using System.Collections;

public class ObstacleSkillDoorLock : ObstacleSkill {
	public Door lockedDoor;
	new public void Update () {
		base.Update ();
		
		if (currentObstacleHealth > 0.0f)
			lockedDoor.Close ();
	}
}
