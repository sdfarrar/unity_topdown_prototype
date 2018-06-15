using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerInput : ScriptableObject {

	public bool AttackPressed;
	public bool AttackHeldDown;

	public bool InteractPressed;
	public bool InteractHeldDown;

	public bool DashPressed;

	public float Horizontal;
	public float HorizontalRaw;

	public float Vertical;
	public float VerticalRaw;

	//TODO allow configuration on these
	public KeyCode AttackKey = KeyCode.Keypad5;
	public KeyCode InteractKey = KeyCode.Keypad4;
	public KeyCode DashKey = KeyCode.Space;

	public void Update() {
		Horizontal = Input.GetAxis("Horizontal");
		HorizontalRaw = Input.GetAxisRaw("Horizontal");

		Vertical = Input.GetAxis("Vertical");
		VerticalRaw = Input.GetAxisRaw("Vertical");

		AttackPressed = Input.GetKeyDown(AttackKey);
		AttackHeldDown = Input.GetKey(AttackKey);

		InteractPressed = Input.GetKeyDown(InteractKey);
		InteractHeldDown = Input.GetKey(InteractKey);

		DashPressed = Input.GetKeyDown(DashKey);
	}

}
