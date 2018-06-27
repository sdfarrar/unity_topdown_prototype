using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryState : ScriptableObject {
	public List<string> OwnedGUIDS = new List<string> ();
	public HashSet<string> set = new HashSet<string>();
	public List<InventoryItem> OwnedItem = new List<InventoryItem>();
}
