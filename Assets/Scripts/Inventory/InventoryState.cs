using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryState : ScriptableObject {
	public List<string> OwnedGUIDS = new List<string> ();
	public HashSet<string> set = new HashSet<string>();
	public List<InventoryItem> OwnedItem = new List<InventoryItem>();

	public void Reload(Dictionary<string, InventoryItem> items){
		OwnedItem.Clear();
		set.Clear();

		for(int i=0; i<OwnedGUIDS.Count; ++i){
			string guid = OwnedGUIDS[i];
			set.Add(guid);
			OwnedItem.Add(items[guid]);
		}
	}

	public bool Add(InventoryItem item){
		bool result = set.Add(item.GUID);
		if(result){
			OwnedGUIDS.Add(item.GUID);
			OwnedItem.Add(item);
		}
		return result;
	}

	public bool Remove(InventoryItem item){
		bool result = set.Remove(item.GUID);
		if(result){
			OwnedGUIDS.Remove(item.GUID);
			OwnedItem.Remove(item);
		}
		return result;
	}
}
