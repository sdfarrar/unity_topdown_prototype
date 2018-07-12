using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class BombDrop : MonoBehaviour, ICollectable {

	public ItemDrop BombItem;

    void Start () {
		
	}
	
	void Update () {
		
	}

    public void OnCollect(InventoryV2 inventory) {
		inventory.ApplyBombCountChange(BombItem.Quantity);
		Destroy(this.gameObject);
    }

}
