using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AbstractItemDrop), true)]
public class ItemDropEditor : Editor {

	private Collider2D collider;
	private SpriteRenderer renderer;

	public override void OnInspectorGUI(){
		AbstractItemDrop drop = (AbstractItemDrop)target;

		EditorGUI.BeginChangeCheck();
		DrawDefaultInspector(); // works for the time being since we only have one field
		if(EditorGUI.EndChangeCheck() && drop.AutoUpdateSprite){
			if(renderer==null){ renderer = drop.GetComponent<SpriteRenderer>(); }
			renderer.sprite = drop.Drop.Item.Sprite;
		}

		if(collider==null){ collider = drop.GetComponent<Collider2D>(); }
		if(!collider.isTrigger){
			EditorGUILayout.HelpBox("Collider should be set to trigger!", MessageType.Warning);
		}
	}
}
