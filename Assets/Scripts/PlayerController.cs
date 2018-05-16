using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private IEnumerable<Weapon> weapons;
	private Weapon activeWeapon;

	public int currentWeaponSlot;
	private int lastWeaponSlot;

	private Vector2 direction;

	private PlayerMovement movement;

	void Start () {
		direction = Vector2.up;
		weapons = GetComponentsInChildren<Weapon>();
		movement = GetComponent<PlayerMovement>();
		ChangeWeapon();
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.K)){
			activeWeapon.PrimaryAttack(movement.GetFacing());
		}else if(Input.GetKeyDown(KeyCode.L)){
			activeWeapon.AlternateAttack(movement.GetFacing());
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
