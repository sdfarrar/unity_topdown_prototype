using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorController : MonoBehaviour {

	public PlayerMovement PlayerMovement;
	private Animator _Animator;

	private AnimState State;

	private struct Facing{
		public static string RIGHT = "FacingRight";
		public static string LEFT  = "FacingLeft";
		public static string UP    = "FacingUp";
		public static string DOWN  = "FacingDown";

		public static bool FacingRight(Vector2 facing){ return facing.Equals(Vector2.right); }
		public static bool FacingLeft(Vector2 facing){ return facing.Equals(Vector2.left); }
		public static bool FacingUp(Vector2 facing){ return facing.Equals(Vector2.up); }
		public static bool FacingDown(Vector2 facing){ return facing.Equals(Vector2.down); }
	}

	private struct AnimState {
		public bool IsIdle;
		public bool IsWalking;
		public bool Attacking;
		public bool FacingLeft;
		public bool FacingRight;
		public bool FacingUp;
		public bool FacingDown;

		public void UpdateAnimatorParams(Animator anim){
			anim.SetBool("FacingRight", FacingRight);
			anim.SetBool("FacingLeft", FacingLeft);
			anim.SetBool("FacingUp", FacingUp);
			anim.SetBool("FacingDown", FacingDown);
			anim.SetBool("IsIdle", IsIdle);
			anim.SetBool("IsWalking", IsWalking);
			anim.SetBool("Attacking", Attacking);
		}
	}


	void Start () {
		_Animator = GetComponent<Animator>();
		State = new AnimState();
		State.FacingDown = true;
	}
	
	void Update () {
		UpdateState();		
		State.UpdateAnimatorParams(_Animator);
	}

	private void UpdateState(){
		UpdateFacing();
		UpdateMovement();
		UpdateActions();
	}

	private void UpdateFacing(){
		Vector2 facing = PlayerMovement.GetFacing();
		State.FacingDown = facing.Equals(Vector2.down);
		State.FacingUp = facing.Equals(Vector2.up);
		State.FacingRight = facing.Equals(Vector2.right);
		State.FacingLeft = facing.Equals(Vector2.left);
	}

	private void UpdateMovement(){
		Vector2 input = PlayerMovement.GetInput();
		bool isWalking = (Mathf.Abs(input.x) + Mathf.Abs(input.y)) > 0;
		State.IsWalking = isWalking;
		State.IsIdle = (input.x==0 && input.y==0);
	}

	private void UpdateActions(){
		State.Attacking = PlayerMovement.playerInput.AttackPressed;
	}



}
