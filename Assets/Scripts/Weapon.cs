using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

	private GameObject weaponGO;
	private new SpriteRenderer renderer;
	private new BoxCollider2D collider;

	protected bool isAttacking;

	public Sprite image;
	public Color tint;
	public bool hasCollider;

	public abstract void PrimaryAttack();
	public abstract void AlternateAttack();

	void Awake () {
		renderer = GetComponentInChildren<SpriteRenderer>();
		renderer.sprite = image;
		renderer.color = tint;

		collider = GetComponentInChildren<BoxCollider2D>();
		if(collider!=null){
			collider.enabled = hasCollider;
		}

		weaponGO = GameObject.FindWithTag("Weapon");
		weaponGO.SetActive(false);
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
