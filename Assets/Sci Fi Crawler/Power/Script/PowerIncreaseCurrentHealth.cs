using UnityEngine;
using System.Collections;

public class PowerIncreaseCurrentHealth : Power {
	public ChangeOverTimeBlock cot;
	
	public override bool Effect (Controller user) {
		return user.character.AddChangeOverTimeBlock (cot);
	}
}
