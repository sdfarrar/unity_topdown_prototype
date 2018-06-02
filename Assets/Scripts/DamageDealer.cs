﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class DamageDealer : MonoBehaviour {

	public IntegerReference DamageAmount;
	public StringListReference DamageableTags;

	//TODO keep a cache of damageable components?

	private void OnTriggerEnter2D(Collider2D other) {
		bool canDamage = DamageableTags.Value.Exists( tag => other.gameObject.CompareTag(tag) );
		Debug.Log("can damage: " + canDamage);
		if(!canDamage){ return; }

		IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
		if(damageable!=null){ damageable.TakeDamage(this); }
	}
}
