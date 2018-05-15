using UnityEngine;

public class Bow : Weapon {

	public GameObject arrow;

	public override int GetIndex(){
	return 1;
	}

	public override void PrimaryAttack() {
		Debug.Log("Bow.PrimaryAttack");
	}

	public override void AlternateAttack() {
		PrimaryAttack();
	}

}
