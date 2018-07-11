﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryStateV2 : ScriptableObject {

	public string HERLP;

	// For quick access of inventory items
	private Dictionary<string, InventoryItem> guidToItemMap = new Dictionary<string, InventoryItem>();
}
