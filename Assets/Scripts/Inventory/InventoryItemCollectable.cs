﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class InventoryItemCollectable : MonoBehaviour, ICollectable {

	public ItemDropTemplate Drop;
	
	private new SpriteRenderer renderer;

	private void Awake() {
		if(Drop==null){ return; }
		renderer = GetComponent<SpriteRenderer>();
		renderer.sprite = Drop.Item.Sprite;
	}

    public void OnCollect(Inventory inventory) {
		if(inventory.UpdateItemQuantity(Drop.Item, Drop.Quantity.Value)){
			Destroy(this.gameObject);
		}
    }

}
