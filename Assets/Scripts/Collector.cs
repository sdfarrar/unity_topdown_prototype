using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Collector : MonoBehaviour {

	public InventoryV2 Inventory;

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag=="Collectable"){
			ICollectable collectable = other.gameObject.GetComponent<ICollectable>();
			if(collectable==null){ return; }
			collectable.OnCollect(Inventory);
		}
	}
	
}
