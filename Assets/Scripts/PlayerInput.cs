using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerInput : ScriptableObject {

	public bool AttackPressed;
	public bool AttackHeldDown;

	public bool InteractPressed;
	public bool InteractHeldDown;

	//TODO allow configuration on these
	public KeyCode AttackKey = KeyCode.Keypad5;
	public KeyCode InteractKey = KeyCode.Keypad4;

	public void Update() {
		AttackPressed = Input.GetKeyDown(AttackKey);
		AttackHeldDown = Input.GetKey(AttackKey);

		InteractPressed = Input.GetKeyDown(InteractKey);
		InteractHeldDown = Input.GetKey(InteractKey);
	}

}
