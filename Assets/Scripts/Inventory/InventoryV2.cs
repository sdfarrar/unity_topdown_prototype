using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName="Inventory/InventoryV2")]
public class InventoryV2 : ScriptableObject {


	public Serializer Serializer;
	
	public Wallet Wallet;
	public InventoryItem[] Items;
	//public InventoryItem Bombs;
	//public InventoryItem Arrows;

	public UnityEvent OnInventoryChanged;
	public UnityEvent OnNewItemAdded;

	public InventoryStateV2 InventoryState;

	public void Save() {
		//Serializer.Serialize("inventory", InventoryState);
	}

	public void Load() {
		//Serializer.Deserialize<InventoryState>("inventory", InventoryState);
		//InventoryState.Reload(m_guidToItem);
		OnInventoryChanged.Invoke();
	}

#if UNITY_EDITOR

	private void OnValidate() {
		FindItems();
		InventoryStateV2 state = UnityEditor.AssetDatabase.LoadAssetAtPath<InventoryStateV2>(UnityEditor.AssetDatabase.GetAssetPath(this));
		if(state==null) {
			state = ScriptableObject.CreateInstance<InventoryStateV2>();
			state.name = "InventoryState";
			UnityEditor.AssetDatabase.AddObjectToAsset(state, this);
			UnityEditor.AssetDatabase.SaveAssets();
		}
		InventoryState = state;
	}

	[ContextMenu("Populate Items")]
	private void FindItems() {
		string[] guids = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(InventoryItem).Name);
		Items = new InventoryItem[guids.Length];
		for(int i=0; i<guids.Length; ++i){
			string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[i]);
			InventoryItem item = UnityEditor.AssetDatabase.LoadAssetAtPath<InventoryItem>(path);
			Items[i] = item;
			item.GUID = guids[i];
			UnityEditor.EditorUtility.SetDirty(this);
		}
	}

	//[ContextMenu("Add Random Item")]
	//private void AddRandomItem() {
	//	int index = Random.Range(0, Items.Length);
	//	InventoryItem item = Items[index];
	//	Debug.Log("Adding: " + item.name);
	//	AddItem(item);
	//}

	//[ContextMenu("Save")]
	//private void SaveFromEditor(){ Save(); }
	//[ContextMenu("Load")]
	//private void LoadFromEditor(){ Load(); }
	//[ContextMenu("Clear")]
	//private void ClearState(){
	//	foreach (var item in Items) {
	//		InventoryState.Remove(item);
	//	}
	//}

#endif
}
