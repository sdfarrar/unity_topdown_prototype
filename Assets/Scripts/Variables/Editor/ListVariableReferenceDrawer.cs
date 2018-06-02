using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(StringListReference), true)]
public class ListVariableReferenceDrawer : VariableReferenceDrawer {
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		float height = base.GetPropertyHeight(property, label);

		return useConstant.boolValue
			? EditorGUI.GetPropertyHeight(constantValue, label, true)
			: height;
	}

	public override Rect CalculateButtonRect(Rect position){
		Rect btnRect = base.CalculateButtonRect(position);
		btnRect.height = EditorGUIUtility.singleLineHeight; // prevents toggle button height from extending to size of the list when expanded
		return btnRect;
	}

	public override float CaclulatePositionXMin(Rect btnRect){
		return useConstant.boolValue
			? base.CaclulatePositionXMin(btnRect) + 10
			: base.CaclulatePositionXMin(btnRect);
	}

	// Prevents Editor from having a delay when expanding the list when using "Constant Value"
	public override bool CanCacheInspectorGUI(SerializedProperty property){ return false; }
}

