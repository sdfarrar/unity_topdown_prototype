using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MagicMeter))]
public class MagicMeterEditor : Editor {

	public override void OnInspectorGUI() {
		base.OnInspectorGUI();

		MagicMeter meter = (MagicMeter)target;

		GUILayout.Label("Change magic meter UI amount only.");
		meter.SolidImage.fillAmount = EditorGUILayout.Slider("Magic Amount", meter.SolidImage.fillAmount, 0, 1);
	}

}
