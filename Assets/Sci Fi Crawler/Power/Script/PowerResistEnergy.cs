using UnityEngine;
using System.Collections;

public class PowerResistEnergy : Power {
	public ResistanceBlock resistanceBlock;
	
	override public bool Effect (Controller user) {
		return user.character.AddResistanceBlock (resistanceBlock);
	}
}
