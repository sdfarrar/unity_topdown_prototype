using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Drops/Bomb")]
public class ItemDrop : ScriptableObject {

	public InventoryItem Item;
	public IntegerReference Quantity;
	public FloatReference DropChance;

}
