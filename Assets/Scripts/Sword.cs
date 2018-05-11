using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon {

	private Animator animator;
	//private bool isAttacking = false;

	private void Start(){
		animator = GetComponent<Animator>();
	}

	public override void PrimaryAttack(){
		if(isAttacking){ return; }
		Debug.Log("PrimaryAttack");
		animator.Play("stab_up", -1, 0f);
	}

	public override void AlternateAttack(){
		if(isAttacking){ return; }
		Debug.Log("AlternateAttack");
		animator.Play("slice_up", -1, 0f);
	}

	//public override void AttackEnd(){
	//	isAttacking = false;
	//}

	//public override void AttackStart(){
	//	isAttacking = true;
	//}
}
