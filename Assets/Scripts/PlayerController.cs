using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private IEnumerable<Weapon> weapons;
	private Weapon activeWeapon;

	public int currentWeaponSlot;
	private int lastWeaponSlot;

	void Start () {
		weapons = GetComponentsInChildren<Weapon>();
		ChangeWeapon();
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.K)){
			activeWeapon.PrimaryAttack();
		}else if(Input.GetKeyDown(KeyCode.L)){
			activeWeapon.AlternateAttack();
		}

		CheckChangeWeaponInput();
		if(currentWeaponSlot!=lastWeaponSlot){
			ChangeWeapon();
		}
	}

	private void CheckChangeWeaponInput(){
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			currentWeaponSlot = 0;
		}else if(Input.GetKeyDown(KeyCode.Alpha2)){
			currentWeaponSlot = 1;
		}

	}

	private void ChangeWeapon(){
		activeWeapon = weapons.Where( weapon => weapon.GetIndex()==currentWeaponSlot ).First();
		activeWeapon.Activate();
		lastWeaponSlot = currentWeaponSlot;
	}

}
