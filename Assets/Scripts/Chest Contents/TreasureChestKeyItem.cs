using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Chest Lootable/Key Item")]
public class TreasureChestKeyItem : TreasureChestContents {

	public override bool AddToInventory(Inventory inventory){
		//inventory.UnlockItem(Contents.Item);
        inventory.Bombs.Unlocked = true;
        inventory.ApplyBombCountChange(Contents.Quantity);
		return true;
	}

}
