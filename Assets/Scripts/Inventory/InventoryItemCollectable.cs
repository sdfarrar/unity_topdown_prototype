using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class InventoryItemCollectable : MonoBehaviour, ICollectable {

	//TODO Deal with key items
	//TODO Deal with initial pickup of new item
	//TODO Deal with items that have a quantity

	public ItemDrop Drop;
	
	private new SpriteRenderer renderer;

	private void Awake() {
		renderer = GetComponent<SpriteRenderer>();
		renderer.sprite = Drop.Item.Sprite;
	}

    public void OnCollect(Inventory inventory) {
		Debug.Log("IventoryItemPickup");
		inventory.AddItem(Drop.Item, Drop.Quantity.Value);
		Destroy(this.gameObject);
    }

#if UNITY_EDITOR
	//public bool SetSpriteInEditor;
	//private void OnValidate() {
	//	if(!SetSpriteInEditor){ GetComponent<SpriteRenderer>().sprite = null; }
	//	if(!SetSpriteInEditor || Item==null || Item.Sprite==null){ return; }
	//	GetComponent<SpriteRenderer>().sprite = Item.Sprite;
	//}
#endif

}
