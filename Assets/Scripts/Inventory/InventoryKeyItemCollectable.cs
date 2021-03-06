﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class InventoryKeyItemCollectable : MonoBehaviour, ICollectable {

	public ItemDropTemplate Drop;
	public int InitialCount;
	
	private new SpriteRenderer renderer;

	private void Awake() {
		if(Drop==null){ return; }
		renderer = GetComponent<SpriteRenderer>();
		renderer.sprite = Drop.Item.Sprite;
	}

    public void OnCollect(Inventory inventory) {
		inventory.AddItem(Drop.Item, InitialCount);
		Destroy(this.gameObject);
    }
}
