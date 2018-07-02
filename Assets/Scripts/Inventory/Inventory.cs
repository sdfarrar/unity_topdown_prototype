using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName="Inventory/Inventory")]
public class Inventory : ScriptableObject {

	public Wallet Wallet;
	public InventoryItem[] Items = new InventoryItem[0];
	public Serializer Serializer;
	public InventoryState InventoryState;
	public UnityEvent OnInventoryChanged;

	private Dictionary<string, InventoryItem> m_guidToItem = new Dictionary<string, InventoryItem>();
	
	private void OnEnable() {
		m_guidToItem.Clear();

		for(int i=0; i<Items.Length; ++i){
			m_guidToItem.Add(Items[i].GUID, Items[i]);
		}

		if(InventoryState!=null){
			InventoryState.Reload(m_guidToItem);
		}
	}

	public void Save() {
		Serializer.Serialize("inventory", InventoryState);
	}

	public void Load() {
		Serializer.Deserialize<InventoryState>("inventory", InventoryState);
		InventoryState.Reload(m_guidToItem);
		OnInventoryChanged.Invoke();
	}

	public void AddItem(InventoryItem item) {
		if(InventoryState.Add(item)){ OnInventoryChanged.Invoke(); }
	}

	public void AddItem(InventoryItem item, int quantity){
		item.ApplyChange(quantity);
		InventoryState.Add(item);
		OnInventoryChanged.Invoke();
	}

	public void RemoveItem(InventoryItem item) {
		if(InventoryState.Remove(item)){ OnInventoryChanged.Invoke(); }
	}

	public void RemoveItem(InventoryItem item, int quantity){
		item.ApplyChange(quantity);
		OnInventoryChanged.Invoke();
	}

#if UNITY_EDITOR

	private void OnValidate() {
		FindItems();
		InventoryState state = UnityEditor.AssetDatabase.LoadAssetAtPath<InventoryState>(UnityEditor.AssetDatabase.GetAssetPath(this));
		if(state==null) {
			state = ScriptableObject.CreateInstance<InventoryState>();
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

	[ContextMenu("Add Random Item")]
	private void AddRandomItem() {
		int index = Random.Range(0, Items.Length);
		InventoryItem item = Items[index];
		Debug.Log("Adding: " + item.name);
		AddItem(item);
	}

	[ContextMenu("Save")]
	private void SaveFromEditor(){ Save(); }
	[ContextMenu("Load")]
	private void LoadFromEditor(){ Load(); }
	[ContextMenu("Clear")]
	private void ClearState(){
		foreach (var item in Items) {
			InventoryState.Remove(item);
		}
	}

#endif
	
}
