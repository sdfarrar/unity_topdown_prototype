using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

	public float speed = 10f;

	private int direction;
	private int lastDirection;

	public float dashSpeed;
	public float dashTime;
	public GameObject dashEffect;
	private float elapsedDashTime;
	private bool dashing;

	private Vector2 moveVelocity;
	private Rigidbody2D rb;
	public PlayerInput playerInput;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		Vector2 input = new Vector2(playerInput.HorizontalRaw, playerInput.VerticalRaw).normalized;
		moveVelocity = input * speed;

		UpdateDirection();
		if(!dashing && direction!=0 && playerInput.DashPressed){
			dashing = true;
			Instantiate(dashEffect, transform.position, Quaternion.identity);
		}
		if(dashing){
			Dash();
		}
	}

	void FixedUpdate() {
		if(!dashing){
			rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
		}
	}

	public Vector2 GetDirection(){
		return GetDirection(direction);
	}

	public Vector2 GetFacing(){
		return (direction==0) ? GetDirection(lastDirection) : GetDirection(direction);
	}

	private Vector2 GetDirection(int dir){
		switch(dir){
			case 1: return Vector2.left;
			case 2: return Vector2.right;
			case 3: return Vector2.up;
			case 4: return Vector2.down;
			default: return Vector2.zero;
		}
	}

	private void UpdateDirection(){
		if(dashing){ return; }
		if(direction!=0){
			lastDirection=direction;
		}
		direction = 0;
		if(Input.GetKey(KeyCode.A)){ // Left
			direction = 1;
		}else if(Input.GetKey(KeyCode.D)){ // Right
			direction = 2;
		}else if(Input.GetKey(KeyCode.W)){ // Up
			direction = 3;
		}else if(Input.GetKey(KeyCode.S)){ // Down
			direction = 4;
		}
	}

	private void Dash(){
		if(elapsedDashTime>=dashTime){
			elapsedDashTime = direction = 0;
			dashing = false;
			rb.velocity = Vector2.zero;
		}else{
			elapsedDashTime += Time.deltaTime;
			switch(direction){
				case 1: rb.velocity = Vector2.left * dashSpeed; break;
				case 2: rb.velocity = Vector2.right * dashSpeed; break;
				case 3: rb.velocity = Vector2.up * dashSpeed; break;
				case 4: rb.velocity = Vector2.down * dashSpeed; break;
			}
		}
	}

	private void LogLastDirection(){
		switch(lastDirection){
			case 1: Debug.Log("Left"); break;
			case 2: Debug.Log("Right"); break;
			case 3: Debug.Log("Up"); break;
			case 4: Debug.Log("Down"); break;
			default: Debug.Log("None"); break;
		}
	}
}
