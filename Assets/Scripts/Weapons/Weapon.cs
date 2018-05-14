using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Perhaps this would work better as a scriptable object?
public abstract class Weapon : MonoBehaviour {

	private static GameObject weaponGO; // if not static then other subclasses break things when setting this to inactive

	private new SpriteRenderer renderer;
	private new BoxCollider2D collider;

	protected bool isAttacking;

	public Sprite image;
	public Color tint;
	public bool hasCollider;

	public abstract void PrimaryAttack();
	public abstract void AlternateAttack();
	public abstract int GetIndex(); // crappy way but it works for now. it'd probably be better if we had an array somewhere

	void Awake () {
		renderer = GetComponentInChildren<SpriteRenderer>();
		renderer.sprite = image;
		renderer.color = tint;

		collider = GetComponentInChildren<BoxCollider2D>();
		if(collider!=null){
			collider.enabled = hasCollider;
		}

		weaponGO = GameObject.FindWithTag("Weapon");
		//weaponGO.SetActive(false); // setting this to inactive will break when having multiple child classes
	}

	public void AttackStart(){
		isAttacking = true;
		weaponGO.SetActive(true);
	}

	public void AttackEnd(){
		isAttacking = false;
		weaponGO.SetActive(false);
	}
}