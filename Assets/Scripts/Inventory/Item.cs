using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName="Inventory/Item")]
public class Item : ScriptableObject {
	[ReadOnlyInEditor]
	public string GUID;
	public bool Unique;
	public Sprite Sprite;

	//public void ApplyChangeToQuantity(int value){
	//	int amount = Mathf.Clamp(Quantity+value, 0, Max.Value);
	//	Quantity = amount;
	//}

#if UNITY_EDITOR
	private void OnValidate() {
		if(UpdateGUID()){ EditorUtility.SetDirty(this); }
	}

	private bool UpdateGUID(){
		string path = AssetDatabase.GetAssetPath(this);
		string guid = AssetDatabase.AssetPathToGUID(path);
		if(GUID.Equals(guid)){ return false; }
		GUID = guid;
		return true;
	}
#endif

}
