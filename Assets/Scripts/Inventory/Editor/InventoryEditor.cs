using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InventoryV2))]
public class InventoryEditor : Editor {

	public override void OnInspectorGUI(){
		DrawDefaultInspector();
		InventoryV2 inventory = (InventoryV2)target;

		// Draw slots ourselves?
	}

}
