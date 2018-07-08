using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Inventory/Item")]
public class InventoryItem : ScriptableObject {
	[ReadOnlyInEditor]
	public string GUID;
	public bool Unique;
	public int Quantity;
	public IntegerReference Max;
	public Sprite Sprite;

	public void ApplyChangeToQuantity(int value){
		int amount = Mathf.Clamp(Quantity+value, 0, Max.Value);
		Quantity = amount;
	}
}
