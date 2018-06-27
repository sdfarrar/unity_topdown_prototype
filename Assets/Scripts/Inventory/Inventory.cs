using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : ScriptableObject {

	public InventoryItem[] Items = new InventoryItem[0];
	public Serializer Serializer;
	public InventoryState InventoryState;
	public UnityEvent OnInventoryChanged;

	private Dictionary<string, InventoryItem> m_guidToItem = new Dictionary<string, InventoryItem>();
	
	private void OnEnable() {

	}

	public void Save() {

	}

	public void Load() {

	}

	
}
