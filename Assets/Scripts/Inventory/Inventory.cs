using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
#endif

[CreateAssetMenu(menuName="Inventory/Inventory")]
public class Inventory : ScriptableObject {

	public Serializer Serializer;
	
	public Wallet Wallet;

	public InventorySlot Bombs;
	public InventorySlot Arrows;

	public UnityEvent OnInventoryChanged;
	public UnityEvent OnNewItemAdded;

	// Used to build item lookup table
	[SerializeField]
	private List<InventorySlot> Slots = new List<InventorySlot>();

	private Dictionary<string, Item> itemLookup = new Dictionary<string, Item>();

	//public InventoryState InventoryState;

	private void OnEnable()	{
		itemLookup.Clear();
		for(int i=0; i<Slots.Count; ++i){
			itemLookup.Add(Slots[i].Item.GUID, Slots[i].Item);
		}
	}

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

	public bool UnlockItem(Item item, int quantity){
		InventorySlot slot = Slots.Find(s =>  s.Item.GUID == item.GUID );
		if(slot == null){ return false; }

		slot.Unlocked = true;
		slot.ApplyChangeToCount(quantity);
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

	public void ResetInventory(){
        Bombs.Unlocked = false;
		Bombs.Count.SetValue(0);
		Wallet.CurrentAmount = 0;
		OnInventoryChanged.Invoke();
	}

#if UNITY_EDITOR

	private void OnValidate() {
		Slots = new List<InventorySlot>();

		FieldInfo[] props = this.GetType().GetFields();
		foreach(FieldInfo prop in props){
			if(prop.FieldType == typeof(InventorySlot)){
				InventorySlot slot = (InventorySlot)prop.GetValue(this);
				if(slot.Item==null || slot.Item.GUID.Equals("")){ continue; }
				Slots.Add(slot);
			}
		}
		EditorUtility.SetDirty(this);
	}


	[ContextMenu("Save")]
	private void SaveFromEditor(){ Save(); }
	[ContextMenu("Load")]
	private void LoadFromEditor(){ Load(); }
	[ContextMenu("Clear")]
	private void ClearLookup(){
		itemLookup.Clear();
	}

#endif
}

[System.Serializable]
public class InventorySlot {
	public Item Item;
	public IntegerVariable Count;
	public IntegerReference Max;
	public bool Unlocked;

	public void ApplyChangeToCount(int value){
		int amount = Mathf.Clamp(Count.Value+value, 0, Max.Value);
		Count.SetValue(amount);
	}
}