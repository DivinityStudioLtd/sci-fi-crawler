using UnityEngine;
using System.Collections;

public class InterfaceInventory : Interface {
	public ItemBucketState itemBucketState;
	
	public override bool SetDisplay (bool newDisplay) {
		itemBucketState = ItemBucketState.Main;
		if (newDisplay) {
			factoryCharacter.SpawnInventory (inventoryPosition);
		} else {
			CleanUp ();	
		}
		return base.SetDisplay (newDisplay);
	}
	public InventoryPosition inventoryPosition;
	new public void OnGUI () {
		if (!display) 
			return;
		interfaceSolar.MenuGame ();
		
		switch (itemBucketState) {
		case ItemBucketState.Main :
			InventoryMain ();
			break;
		case ItemBucketState.Body :
			PowerSelect ();
			break;
		case ItemBucketState.Firearm :
			FirearmSelect ();
			break;
		case ItemBucketState.Power :
			BodySelect ();
			break;
		}
	}
	
	void CleanUp () {
		Destroy (inventoryPosition.bodyPosition.GetChild (0).gameObject);
		foreach (Transform t in inventoryPosition.firearmPositions)
			Destroy (t.GetChild (0).gameObject);
		foreach (Transform t in inventoryPosition.powerPositions)
			Destroy (t.GetChild (0).gameObject);
	}

	void InventoryMain () {
		int i = (Screen.height - 120 - 40) / 3;
        GUILayout.BeginArea (new Rect (10,120,Screen.width / 3,1000));
		if (GUILayout.Button ("Body",GUILayout.Height (i)))
			itemBucketState = ItemBucketState.Body;
        if (GUILayout.Button ("Firearm",GUILayout.Height (i)))
			itemBucketState = ItemBucketState.Body;
        if (GUILayout.Button ("Power",GUILayout.Height (i)))
			itemBucketState = ItemBucketState.Body;
        GUILayout.EndArea ();
	}
	
	void PowerSelect () {
		
	}
	
	void FirearmSelect () {
		
	}
	
	void BodySelect () {
		
	}
	
}
public enum ItemBucketState {
	Body,
	Firearm,
	Power,
	Main
}
