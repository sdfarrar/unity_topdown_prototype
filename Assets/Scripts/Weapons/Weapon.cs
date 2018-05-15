using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

	public GameObject weaponGO; // should probably be extracted out into some weapon manager class

	private new SpriteRenderer renderer;
	private new BoxCollider2D collider;

	private bool isActive;

	protected bool isAttacking;

	public Sprite image;
	public Color tint;
	public bool hasCollider;

	public abstract void PrimaryAttack();
	public abstract void AlternateAttack();
	public abstract int GetIndex();

	void Awake () {
		renderer = GetComponentInChildren<SpriteRenderer>();

		collider = GetComponentInChildren<BoxCollider2D>();
		if(collider!=null){
			collider.enabled = hasCollider;
		}

	}

	void Start(){
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

	public void Activate(){
		renderer.sprite = image;
		renderer.color = tint;
	}
}