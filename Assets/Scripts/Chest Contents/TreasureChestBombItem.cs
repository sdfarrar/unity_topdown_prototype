using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Chest Lootable/Bombs")]
public class TreasureChestBombItem : TreasureChestContents {

	public override bool AddToInventory(Inventory inventory){
		inventory.ApplyBombCountChange(Contents.Quantity);
		return true;
	}

}
