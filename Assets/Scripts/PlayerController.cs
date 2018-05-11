using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private IEnumerable<Weapon> weapons;
	private Weapon activeWeapon;

	void Start () {
		weapons = GetComponentsInChildren<Weapon>();
		activeWeapon = weapons.Where( weapon => weapon.enabled==true ).First();
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.K)){
			activeWeapon.PrimaryAttack();
		}else if(Input.GetKeyDown(KeyCode.L)){
			activeWeapon.AlternateAttack();
		}
	}

}
