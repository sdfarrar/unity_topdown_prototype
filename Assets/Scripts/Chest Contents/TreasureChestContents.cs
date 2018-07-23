using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TreasureChestContents : ScriptableObject {
	public ItemDropTemplate Contents;
	public string Text;

	public abstract bool AddToInventory(Inventory inventory);
}

