﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Inventory/Item")]
public class InventoryItem : ScriptableObject {
	[ReadOnlyInEditor]
	public string GUID;
	public bool hasQuantity;
	public IntegerReference Quantity;
	public IntegerReference Max;
	public Sprite Sprite;
}
