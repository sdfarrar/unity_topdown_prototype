using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor {

	public override void OnInspectorGUI(){
		DrawDefaultInspector();
		Inventory inventory = (Inventory)target;

		// Draw slots ourselves?
	}

}
