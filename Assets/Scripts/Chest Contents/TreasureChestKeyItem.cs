using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Chest Lootable/Key Item")]
public class TreasureChestKeyItem : TreasureChestContents {

	public override bool AddToInventory(Inventory inventory){
		return inventory.UnlockItem(Contents.Item, Contents.Quantity);
	}

}
