using UnityEngine;

public class Sword : Weapon {

	public bool instantStab;

	private Animator animator;

	private void Start(){
		animator = GetComponent<Animator>();
	}

	public override int GetIndex(){
		return 0;
	}

	public override void PrimaryAttack(Vector2 direction){
		if(isAttacking){ return; }
		Debug.Log("PrimaryAttack");
		if(instantStab){
			animator.Play("stab_up_instant", -1, 0f);
		}else{
			animator.Play("stab_up", -1, 0f);
		}
	}

	public override void AlternateAttack(Vector2 direction){
		if(isAttacking){ return; }
		Debug.Log("AlternateAttack");
		animator.Play("slice_up", -1, 0f);
	}

}
