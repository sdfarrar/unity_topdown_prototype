using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class InventoryItemPickup : MonoBehaviour, ICollectable {

	public InventoryItem Item;
	
	private new SpriteRenderer renderer;

	private void Awake() {
		renderer = GetComponent<SpriteRenderer>();
		renderer.sprite = Item.Sprite;
	}

    public void OnCollect(Inventory inventory) {
		Debug.Log("IventoryItemPickup");
		inventory.AddItem(Item);
		Destroy(this.gameObject);
    }

#if UNITY_EDITOR
	public bool SetSpriteInEditor;
	private void OnValidate() {
		if(!SetSpriteInEditor){ GetComponent<SpriteRenderer>().sprite = null; }
		if(!SetSpriteInEditor || Item==null || Item.Sprite==null){ return; }
		GetComponent<SpriteRenderer>().sprite = Item.Sprite;
	}
#endif

}
