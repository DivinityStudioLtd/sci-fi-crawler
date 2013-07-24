using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controller : Entity {
	public int team;
	
	public int credits;
	
	public CharacterMotor characterMotor;
	public Character character;
	public AnimationController animationController;
	public HUD3D hUD3D;
	public List<Firearm> firearms;
	public Transform firearmTransform;
	public CurrentTile currentTile;
	public SkillInteractor skillInteractor;
	public PickUpInteractor pickUpInteractor;
	public PositionRecord positionRecord;
	
	public List<ItemBucket> missionsInventory;
	
	public List<Power> powers;
	
	int currentFirearm;
	
	void Start () {
		managerCharacter.AddEntity (this);
		SetupFirearms();	
		SetupPowers();	
		if (skillInteractor != null)
			skillInteractor.controller = this;
		if (pickUpInteractor != null)
			pickUpInteractor.controller = this;
	}
	
	public Firearm CurrentFirearm {
		get {
			return firearms [currentFirearm];	
		}
	}
	
	public void SetupFirearms () {
		foreach (Firearm f in firearms) {
			f.SetActive (false, firearmTransform);
			f.controller = this;
		}
		if (firearms.Count > 0)
			firearms[0].SetActive (true, firearmTransform);
		currentFirearm = 0;
	}
	
	public void SetupPowers () {
		foreach (Power p in powers) {
			p.transform.parent = hUD3D.transform;	
		}
		hUD3D.SetPowerPositions (powers);
	}
	
	public void NextWeapon () {
		firearms [currentFirearm].SetActive (false, firearmTransform);
		currentFirearm++;
		if (currentFirearm >= firearms.Count)
			currentFirearm = 0;
		firearms [currentFirearm].SetActive (true, firearmTransform);
	}
	
	public Power CurrentPower {
		get {
			return powers[0];	
		}
	}
	
	public void CyclePower (bool next) {
		int addPoint = next ? powers.Count - 1 : 0;
		int removePoint = next ? 0 : powers.Count - 1;
		Power temp = powers [removePoint];
		powers.RemoveAt (removePoint);
		powers.Insert (addPoint, temp);
		hUD3D.SetPowerPositions (powers);
	}
	
	public Transform tDSCameraTransform;
	public void SetMainCameraToTDS () {
		InterfaceUtility.SetCameraToTransform (tDSCameraTransform, true);
	}
	
	void LateUpdate () {
		characterMotor.maxSpeed = character.maxSpeed;
		hUD3D.SetHealthBar (character.currentHealth, character.maxHealth);
		hUD3D.SetEnergyBar (character.currentEnergy, character.maxEnergy);
	}
	
	public string ToString () {
		return "";
	}
}