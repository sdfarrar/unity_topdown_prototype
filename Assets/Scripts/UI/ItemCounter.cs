using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCounter : AbstractCounter {

	public InventoryItem Item;

    protected override void UpdateText() {
		CountText.text = Item.Quantity < 10 ? "0"+Item.Quantity : ""+Item.Quantity;
    }
}
