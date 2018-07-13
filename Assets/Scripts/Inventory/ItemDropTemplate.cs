using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Drops/Item Template")]
public class ItemDropTemplate : ScriptableObject {

	public Item Item;
	public IntegerReference Quantity;

}