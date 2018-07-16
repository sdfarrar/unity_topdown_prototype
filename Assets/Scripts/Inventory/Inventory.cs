using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName="Inventory/Inventory")]
public class Inventory : ScriptableObject {


	public Serializer Serializer;
	
	public Wallet Wallet;
	//public InventoryItem[] Items;

	public InventorySlot Bombs;
	public InventorySlot Arrows;

	public UnityEvent OnInventoryChanged;
	public UnityEvent OnNewItemAdded;

	public InventoryState InventoryState;

	public void Save() {
		//Serializer.Serialize("inventory", InventoryState);
	}

	public void Load() {
		//Serializer.Deserialize<InventoryState>("inventory", InventoryState);
		//InventoryState.Reload(m_guidToItem);
		OnInventoryChanged.Invoke();
	}

	public bool AddItem(Item item, int initialCount){
		OnInventoryChanged.Invoke();
		return true;
	}

	public bool UpdateItemQuantity(Item item, int count){
		OnInventoryChanged.Invoke();
		return true;
	}

	public bool ApplyChangeToWallet(int delta){
		Wallet.ApplyChange(delta);
		OnInventoryChanged.Invoke();
		return true;
	}

	public void ApplyBombCountChange(int quantity){
		Bombs.ApplyChangeToCount(quantity);
		OnInventoryChanged.Invoke();
	}

	public void Reset(){
		Bombs.Count.SetValue(0);
		Wallet.CurrentAmount = 0;
		OnInventoryChanged.Invoke();
	}

#if UNITY_EDITOR

	//private void OnValidate() {
	//	FindItems();
	//	InventoryStateV2 state = UnityEditor.AssetDatabase.LoadAssetAtPath<InventoryStateV2>(UnityEditor.AssetDatabase.GetAssetPath(this));
	//	if(state==null) {
	//		state = ScriptableObject.CreateInstance<InventoryStateV2>();
	//		state.name = "InventoryState";
	//		UnityEditor.AssetDatabase.AddObjectToAsset(state, this);
	//		UnityEditor.AssetDatabase.SaveAssets();
	//	}
	//	InventoryState = state;
	//}

	//[ContextMenu("Populate Items")]
	//private void FindItems() {
	//	string[] guids = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(InventoryItem).Name);
	//	Items = new InventoryItem[guids.Length];
	//	for(int i=0; i<guids.Length; ++i){
	//		string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[i]);
	//		InventoryItem item = UnityEditor.AssetDatabase.LoadAssetAtPath<InventoryItem>(path);
	//		Items[i] = item;
	//		item.GUID = guids[i];
	//		UnityEditor.EditorUtility.SetDirty(this);
	//	}
	//}

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

[System.Serializable]
public struct InventorySlot {
	public Item Item;
	public IntegerVariable Count;
	public IntegerReference Max;

	public void ApplyChangeToCount(int value){
		int amount = Mathf.Clamp(Count.Value+value, 0, Max.Value);
		Count.SetValue(amount);
	}
}