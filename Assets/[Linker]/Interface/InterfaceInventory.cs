using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InterfaceInventory : Interface {
	public ItemBucketState itemBucketState;
	public static int EQUIP_WIDTH = 75;
	public override bool SetDisplay (bool newDisplay) {
		itemBucketState = ItemBucketState.Main;
		if (newDisplay) {
			factoryCharacter.SpawnInventory (inventoryPosition);
		} else {
			CleanUp ();	
		}
		return base.SetDisplay (newDisplay);
	}
	Rect bottomLeft;
	int scrollBlockHeight;
	
	public InventoryPosition inventoryPosition;
	new public void OnGUI () {
		bottomLeft = new Rect (10,40,Screen.width / 3,Screen.height - 40);
		scrollBlockHeight = (Screen.height - 60) / 3;
		if (!display) 
			return;
		interfaceSolar.MenuGame ();
		
        GUILayout.BeginArea (bottomLeft);
		switch (itemBucketState) {
		case ItemBucketState.Main :
			InventoryMain ();
			break;
		case ItemBucketState.Body :
			BodySelect ();
			break;
		case ItemBucketState.Firearm :
			FirearmSelect ();
			break;
		case ItemBucketState.Power :
			PowerSelect ();
			break;
		}
        GUILayout.EndArea ();
	}
	
	void CleanUp () {
		Destroy (inventoryPosition.bodyPosition.GetChild (0).gameObject);
		foreach (Transform t in inventoryPosition.firearmPositions)
			Destroy (t.GetChild (0).gameObject);
		foreach (Transform t in inventoryPosition.powerPositions)
			Destroy (t.GetChild (0).gameObject);
	}

	void InventoryMain () {
		if (GUILayout.Button ("Body",GUILayout.Height (scrollBlockHeight))) {
			scrollPosition = Vector2.zero;
			itemBucketState = ItemBucketState.Body;
		}
        if (GUILayout.Button ("Firearm",GUILayout.Height (scrollBlockHeight))) {
			scrollPosition = Vector2.zero;
			itemBucketState = ItemBucketState.Firearm;
		}
        if (GUILayout.Button ("Power",GUILayout.Height (scrollBlockHeight))) {
			scrollPosition = Vector2.zero;
			itemBucketState = ItemBucketState.Power;
		}
	}
	Vector2 scrollPosition = Vector2.zero;
	void PowerSelect () {
		if (GUILayout.Button ("Back"))
			itemBucketState = ItemBucketState.Main;
		BottomRightSelectedPower ();
		
		scrollPosition = GUILayout.BeginScrollView (scrollPosition, false, true);//, GUILayout.Height (scrollBlockHeight * 2));
		for (int i = 0; i < managerPlayer.playerInventory.powers.Count; i++) {
			GUILayout.BeginHorizontal ();
			Power p = managerPlayer.playerInventory.powers [i].GetComponent<Power> ();
			GUILayout.Label (p.ToString ());
			
			GUILayout.FlexibleSpace();
			
			GUILayout.BeginVertical ();
			if (GUILayout.Button ("Slot 1",GUILayout.Width (EQUIP_WIDTH))) {
				Swap (managerPlayer.selected.powers, 0, managerPlayer.playerInventory.powers, i);
				i--;
			}
			if (GUILayout.Button ("Slot 2",GUILayout.Width (EQUIP_WIDTH))) {
				Swap (managerPlayer.selected.powers, 1, managerPlayer.playerInventory.powers, i);
				i--;
			}
			if (GUILayout.Button ("Slot 3",GUILayout.Width (EQUIP_WIDTH))) {
				Swap (managerPlayer.selected.powers, 2, managerPlayer.playerInventory.powers, i);
				i--;
			}
			GUILayout.EndVertical ();
			GUILayout.EndHorizontal ();
		}
		GUILayout.EndScrollView ();
	}
	public void BottomRightSelectedPower () {
		GUILayout.BeginHorizontal ();
		foreach (GameObject go in managerPlayer.selected.powers) {
			Power p = go.GetComponent<Power> ();
			GUILayout.Label (p.ToString ());
		}
		GUILayout.EndHorizontal ();
		
	}
	
	void FirearmSelect () {
		if (GUILayout.Button ("Back"))
			itemBucketState = ItemBucketState.Main;
		BottomRightSelectedFirearm ();
		
		scrollPosition = GUILayout.BeginScrollView (scrollPosition, false, true);//, GUILayout.Height (scrollBlockHeight * 2));
		for (int i = 0; i < managerPlayer.playerInventory.firearms.Count; i++) {
			GUILayout.BeginHorizontal ();
			Firearm f = managerPlayer.playerInventory.firearms [i].GetComponent<Firearm> ();
			GUILayout.Label (f.ToString ());
			
			GUILayout.FlexibleSpace();
			
			GUILayout.BeginVertical ();
			if (GUILayout.Button ("Slot 1",GUILayout.Width (EQUIP_WIDTH))) {
				Swap (managerPlayer.selected.firearms, 0, managerPlayer.playerInventory.firearms, i);
				i--;
			}
			if (GUILayout.Button ("Slot 2",GUILayout.Width (EQUIP_WIDTH))) {
				Swap (managerPlayer.selected.firearms, 1, managerPlayer.playerInventory.firearms, i);
				i--;
			}
			GUILayout.EndVertical ();
			GUILayout.EndHorizontal ();
		}
		GUILayout.EndScrollView ();
	}
	public void BottomRightSelectedFirearm () {
		GUILayout.BeginHorizontal ();
		foreach (GameObject go in managerPlayer.selected.firearms) {
			Firearm f = go.GetComponent<Firearm> ();
			GUILayout.Label (f.ToString ());
		}
		GUILayout.EndHorizontal ();
	}
	
	void BodySelect () {
		if (GUILayout.Button ("Back"))
			itemBucketState = ItemBucketState.Main;
		
		BottomRightSelectedBody ();
		
		scrollPosition = GUILayout.BeginScrollView (scrollPosition, false, true);//, GUILayout.Height (scrollBlockHeight * 2));
		for (int i = 0; i < managerPlayer.playerInventory.bodies.Count; i++) {
			GUILayout.BeginHorizontal ();
			Controller c = managerPlayer.playerInventory.bodies [i].GetComponent<Controller> ();
			GUILayout.Label (c.ToString ());
			
			GUILayout.FlexibleSpace();
			
			if (GUILayout.Button ("Equip",GUILayout.Width (EQUIP_WIDTH))) {
				Swap (managerPlayer.selected.bodies, 0, managerPlayer.playerInventory.bodies, i);
				i--;
			}
			GUILayout.EndHorizontal ();
		}
		GUILayout.EndScrollView ();
	}
	public void BottomRightSelectedBody () {
		GUILayout.Label (managerPlayer.selected.bodies [0].GetComponent <Controller> ().ToString ());
	}
	
	void Swap (List<GameObject> toList, int to, List<GameObject> fromList, int fro) {
		GameObject tempTo = toList [to];
		GameObject tempFrom = fromList [fro];
		
		toList.RemoveAt (to);
		fromList.RemoveAt (fro);
		
		toList.Insert (to, tempFrom); 
		fromList.Add (tempTo);
		
		CleanUp ();	
		factoryCharacter.SpawnInventory (inventoryPosition);
	}
	
}
public enum ItemBucketState {
	Body,
	Firearm,
	Power,
	Main
}
