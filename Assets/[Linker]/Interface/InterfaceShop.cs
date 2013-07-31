using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InterfaceShop : Interface {
	public static int BUY_SELL_WIDTH = 75;
	public override bool SetDisplay (bool newDisplay) {
		itemBucketState = ItemBucketState.Body;
		if (managerMap.universe.shop.itemBucket.bodies.Count > 0)
			selectedBuy = managerMap.universe.shop.itemBucket.bodies [0];
		selectedSell = managerPlayer.selected.bodies [0];
		
		if (newDisplay) {
			DisplayShop ();
		} else {
			CleanUp ();	
		}
		return base.SetDisplay (newDisplay);
	}
	new public void OnGUI () {
		if (!display) 
			return;
		interfaceSolar.MenuGame ();
		ShopBar ();
		switch (itemBucketState) {
		case ItemBucketState.Body:
			BuyArea (managerMap.universe.shop.itemBucket.bodies);
			SellArea (managerPlayer.selected.bodies, managerPlayer.playerInventory.bodies);
			break;
		case ItemBucketState.Firearm:
			BuyArea (managerMap.universe.shop.itemBucket.firearms);
			SellArea (managerPlayer.selected.firearms, managerPlayer.playerInventory.firearms);
			break;
		case ItemBucketState.Power:
			BuyArea (managerMap.universe.shop.itemBucket.powers);
			SellArea (managerPlayer.selected.powers, managerPlayer.playerInventory.powers);
			break;
		}
	}
	
	void ShopBar () {
		GUILayout.BeginArea (new Rect (10,40,Screen.width - 20,100));
		GUILayout.BeginHorizontal ();
        if (GUILayout.Button ("Body")) {
			itemBucketState = ItemBucketState.Body;
			if (managerMap.universe.shop.itemBucket.bodies.Count > 0)
				selectedBuy = managerMap.universe.shop.itemBucket.bodies [0];
			else 
				selectedBuy = null;
			selectedSell = managerPlayer.selected.bodies [0];
			CleanUp ();	
			DisplayShop ();
		}
        if (GUILayout.Button ("Firearm")) {
			itemBucketState = ItemBucketState.Firearm;
			if (managerMap.universe.shop.itemBucket.firearms.Count > 0)
				selectedBuy = managerMap.universe.shop.itemBucket.firearms [0];
			else 
				selectedBuy = null;
			selectedSell = managerPlayer.selected.firearms [0];
			CleanUp ();	
			DisplayShop ();
		}
        if (GUILayout.Button ("Power")) {
			itemBucketState = ItemBucketState.Power;
			if (managerMap.universe.shop.itemBucket.powers.Count > 0)
				selectedBuy = managerMap.universe.shop.itemBucket.powers [0];
			else 
				selectedBuy = null;
			selectedSell = managerPlayer.selected.powers [0];
			CleanUp ();	
			DisplayShop ();
		}
		GUILayout.EndHorizontal ();
        GUILayout.EndArea ();
	}
	
	public ItemBucketState itemBucketState;
	
	public ShopPositions powerShopPositions;
	public ShopPositions bodyShopPositions;
	public ShopPositions firearmShopPositions;
	
	Rect buyRect;
	Vector2 buyScroll;
	public void BuyArea (List<GameObject> list) {
		buyRect = new Rect (10, Screen.height / 2, (Screen.width / 2) - 20, Screen.height / 2);
        GUILayout.BeginArea (buyRect);
		buyScroll = GUILayout.BeginScrollView (buyScroll, false, true);//, GUILayout.Height (scrollBlockHeight * 2));
		
		for (int i = 0; i < list.Count; i++) {
			GUILayout.BeginHorizontal ("box");
			switch (itemBucketState) {
			case ItemBucketState.Body:
				GUILayout.Label (list [i].GetComponent<Controller> ().ToString ());
				GUILayout.FlexibleSpace();
				
				GUILayout.BeginVertical ();
				GUILayout.Label ("Credits: " + list [i].GetComponent<Controller> ().credits);
				if (GUILayout.Button ("View",GUILayout.Width (BUY_SELL_WIDTH))) {
					selectedBuy = list [i];
					CleanUp ();
					DisplayShop ();
				}
				if (GUILayout.Button ("Buy",GUILayout.Width (BUY_SELL_WIDTH))) {
					if (managerPlayer.playerInventory.credit >= list [i].GetComponent<Controller> ().credits) {
						if (list [i] == selectedBuy) {
							selectedBuy = null;
							CleanUp ();
							DisplayShop ();
						}
						managerPlayer.playerInventory.credit -= list [i].GetComponent<Controller> ().credits;
						managerPlayer.playerInventory.bodies.Add (list [i]);
						list.RemoveAt (i);
						i--;
					}
				}
				GUILayout.EndVertical ();
				break;
			case ItemBucketState.Firearm:
				GUILayout.Label (list [i].GetComponent<Firearm> ().ToString ());
				
				GUILayout.FlexibleSpace();
				
				GUILayout.BeginVertical ();
				GUILayout.Label ("Credits: " + list [i].GetComponent<Firearm> ().credits);
				if (GUILayout.Button ("View",GUILayout.Width (BUY_SELL_WIDTH))) {
					selectedBuy = list [i];
					CleanUp ();
					DisplayShop ();
				}
				if (GUILayout.Button ("Buy",GUILayout.Width (BUY_SELL_WIDTH))) {
					if (managerPlayer.playerInventory.credit >= list [i].GetComponent<Firearm> ().credits) {
						if (list [i] == selectedBuy) {
							selectedBuy = null;
							CleanUp ();
							DisplayShop ();
						}
						managerPlayer.playerInventory.credit -= list [i].GetComponent<Firearm> ().credits;
						managerPlayer.playerInventory.firearms.Add (list [i]);
						list.RemoveAt (i);
						i--;
					}
				}
				GUILayout.EndVertical ();
				break;
				
			case ItemBucketState.Power:
				GUILayout.Label (list [i].GetComponent<Power> ().ToString ());
				GUILayout.FlexibleSpace();
				
				GUILayout.BeginVertical ();
				GUILayout.Label ("Credits: " + list [i].GetComponent<Power> ().credits);
				if (GUILayout.Button ("View",GUILayout.Width (BUY_SELL_WIDTH))) {
					selectedBuy = list [i];
					CleanUp ();
					DisplayShop ();
				}
				if (GUILayout.Button ("Buy",GUILayout.Width (BUY_SELL_WIDTH))) {
					if (managerPlayer.playerInventory.credit >= list [i].GetComponent<Power> ().credits) {
						if (list [i] == selectedBuy) {
							selectedBuy = null;
							CleanUp ();
							DisplayShop ();
						}
						managerPlayer.playerInventory.credit -= list [i].GetComponent<Power> ().credits;
						managerPlayer.playerInventory.powers.Add (list [i]);
						list.RemoveAt (i);
						i--;
					}
				}
				GUILayout.EndVertical ();
				break;
				
			}
			GUILayout.EndHorizontal ();
		}
		
		GUILayout.EndScrollView ();
        GUILayout.EndArea ();
	}
	
	Rect sellRect;
	Vector2 sellScroll;
	public void SellArea (List<GameObject> preList, List<GameObject> list) {
		sellRect = new Rect ((Screen.width / 2) + 10, Screen.height / 2, (Screen.width / 2) - 20, Screen.height / 2);
        GUILayout.BeginArea (sellRect);
		sellScroll = GUILayout.BeginScrollView (sellScroll, false, true);//, GUILayout.Height (scrollBlockHeight * 2));
		
		
		for (int i = 0; i < preList.Count; i++) {
			GUILayout.BeginHorizontal ("box");
			switch (itemBucketState) {
			case ItemBucketState.Body:
				GUILayout.Label (preList [i].GetComponent<Controller> ().ToString ());
				GUILayout.FlexibleSpace();
				
				GUILayout.BeginVertical ();
				if (GUILayout.Button ("View",GUILayout.Width (BUY_SELL_WIDTH))) {
					selectedSell = preList [i];
					CleanUp ();
					DisplayShop ();
				}
				GUILayout.Label ("Equip");
				GUILayout.EndVertical ();
				break;
			case ItemBucketState.Firearm:
				GUILayout.Label (preList [i].GetComponent<Firearm> ().ToString ());
				
				GUILayout.FlexibleSpace();
				
				GUILayout.BeginVertical ();
				if (GUILayout.Button ("View",GUILayout.Width (BUY_SELL_WIDTH))) {
					selectedSell = preList [i];
					CleanUp ();
					DisplayShop ();
				}
				GUILayout.Label ("Equip");
				GUILayout.EndVertical ();
				break;
				
			case ItemBucketState.Power:
				GUILayout.Label (preList [i].GetComponent<Power> ().ToString ());
				GUILayout.FlexibleSpace();
				
				GUILayout.BeginVertical ();
				if (GUILayout.Button ("View",GUILayout.Width (BUY_SELL_WIDTH))) {
					selectedSell = preList [i];
					CleanUp ();
					DisplayShop ();
				}
				GUILayout.Label ("Equip");
				GUILayout.EndVertical ();
				break;
				
			}
			
			
			GUILayout.EndHorizontal ();
		}
		
		for (int i = 0; i < list.Count; i++) {
			GUILayout.BeginHorizontal ("box");
			switch (itemBucketState) {
			case ItemBucketState.Body:
				GUILayout.Label (list [i].GetComponent<Controller> ().ToString ());
				GUILayout.FlexibleSpace();
				
				GUILayout.BeginVertical ();
				GUILayout.Label ("Credits: " + list [i].GetComponent<Controller> ().credits);
				if (GUILayout.Button ("View",GUILayout.Width (BUY_SELL_WIDTH))) {
					selectedSell = list [i];
					CleanUp ();
					DisplayShop ();
				}
				if (GUILayout.Button ("Sell",GUILayout.Width (BUY_SELL_WIDTH))) {
					if (list [i] == selectedSell) {
						selectedSell = null;
						CleanUp ();
						DisplayShop ();
					}
					managerPlayer.playerInventory.credit += list [i].GetComponent<Controller> ().credits;
					managerMap.universe.shop.itemBucket.bodies.Add (list [i]);
					list.RemoveAt (i);
					i--;
				}
				GUILayout.EndVertical ();
				break;
			case ItemBucketState.Firearm:
				GUILayout.Label (list [i].GetComponent<Firearm> ().ToString ());
				
				GUILayout.FlexibleSpace();
				
				GUILayout.BeginVertical ();
				GUILayout.Label ("Credits: " + list [i].GetComponent<Firearm> ().credits);
				if (GUILayout.Button ("View",GUILayout.Width (BUY_SELL_WIDTH))) {
					selectedSell = list [i];
					CleanUp ();
					DisplayShop ();
				}
				if (GUILayout.Button ("Sell",GUILayout.Width (BUY_SELL_WIDTH))) {
					if (list [i] == selectedSell) {
						selectedSell = null;
						CleanUp ();
						DisplayShop ();
					}
					managerPlayer.playerInventory.credit += list [i].GetComponent<Firearm> ().credits;
					managerMap.universe.shop.itemBucket.firearms.Add (list [i]);
					list.RemoveAt (i);
					i--;
				}
				GUILayout.EndVertical ();
				break;
				
			case ItemBucketState.Power:
				GUILayout.Label (list [i].GetComponent<Power> ().ToString ());
				GUILayout.FlexibleSpace();
				
				GUILayout.BeginVertical ();
				GUILayout.Label ("Credits: " + list [i].GetComponent<Power> ().credits);
				if (GUILayout.Button ("View",GUILayout.Width (BUY_SELL_WIDTH))) {
					selectedSell = list [i];
					CleanUp ();
					DisplayShop ();
				}
				if (GUILayout.Button ("Sell",GUILayout.Width (BUY_SELL_WIDTH))) {
					if (list [i] == selectedSell) {
						selectedSell = null;
						CleanUp ();
						DisplayShop ();
					}
					managerPlayer.playerInventory.credit += list [i].GetComponent<Power> ().credits;
					managerMap.universe.shop.itemBucket.powers.Add (list [i]);
					list.RemoveAt (i);
					i--;
				}
				GUILayout.EndVertical ();
				break;
				
			}
			GUILayout.EndHorizontal ();
		}
		
		GUILayout.EndScrollView ();
        GUILayout.EndArea ();
	}
	
	GameObject selectedBuy;
	GameObject selectedSell;
	
	void DisplayShop () {
		switch (itemBucketState) {
		case ItemBucketState.Body:
			factoryCharacter.SpawnShop (bodyShopPositions, selectedBuy, selectedSell);	
			break;
		case ItemBucketState.Power:
			factoryCharacter.SpawnShop (powerShopPositions, selectedBuy, selectedSell);	
			break;
		case ItemBucketState.Firearm:
			factoryCharacter.SpawnShop (firearmShopPositions, selectedBuy, selectedSell);	
			break;
		}
	}
	
	void CleanUp () {
		if (bodyShopPositions.buy.childCount > 0)
			Destroy (bodyShopPositions.buy.GetChild(0).gameObject);
		if (bodyShopPositions.sell.childCount > 0)
			Destroy (bodyShopPositions.sell.GetChild(0).gameObject);
		
		if (powerShopPositions.buy.childCount > 0)
			Destroy (powerShopPositions.buy.GetChild(0).gameObject);
		if (powerShopPositions.sell.childCount > 0)
			Destroy (powerShopPositions.sell.GetChild(0).gameObject);
		
		if (firearmShopPositions.buy.childCount > 0)
			Destroy (firearmShopPositions.buy.GetChild(0).gameObject);
		if (firearmShopPositions.sell.childCount > 0)
			Destroy (firearmShopPositions.sell.GetChild(0).gameObject);
	}
}

[System.Serializable]
public class ShopPositions {
	public Transform buy;
	public Transform sell;
}