using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

	public float speed = 10f;

	private Vector2 moveVelocity;
	private Rigidbody2D rb;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		moveVelocity = input * speed;
	}

	void FixedUpdate() {
		rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
	}
}
