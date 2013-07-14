using UnityEngine;
using System.Collections;

public class PowerRemoveFire : Power {
	override public bool Effect (Controller user) {
		foreach (ChangeOverTimeBlock changeOverTime in user.character.changesOverTime)
			if (changeOverTime.damageType == DamageType.Fire)
				changeOverTime.timeRemaining = 0.0f;
		return true;
	}
}
