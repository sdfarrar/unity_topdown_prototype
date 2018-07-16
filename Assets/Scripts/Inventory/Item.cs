using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Inventory/Item")]
public class Item : ScriptableObject {
	[ReadOnlyInEditor]
	public string GUID;
	public bool Unique;
	public Sprite Sprite;

	//public void ApplyChangeToQuantity(int value){
	//	int amount = Mathf.Clamp(Quantity+value, 0, Max.Value);
	//	Quantity = amount;
	//}
}
