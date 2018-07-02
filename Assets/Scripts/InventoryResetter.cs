﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class InventoryResetter : MonoBehaviour {

	public Inventory Inventory;

	private void Awake(){
		GetComponent<BoxCollider2D>().isTrigger = true;
	}

	private void OnTriggerEnter2D(Collider2D other)	{
		if(other.CompareTag("Player")){
			Inventory.Reset();
		}
	}
}
