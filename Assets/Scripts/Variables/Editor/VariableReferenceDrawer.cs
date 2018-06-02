using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(VariableReference), true)]
public class VariableReferenceDrawer : PropertyDrawer {
	protected SerializedProperty useConstant;
	protected SerializedProperty constantValue;
	protected SerializedProperty variable;

	/// <summary>
	/// Options to display in the popup to select constant or variable
	/// </summary>
	protected readonly string[] popupOptions = {"Use Constant", "Use Variable"};

	/// <summary>
	/// Cached style to use to draw the popup button
	/// </summary>
	protected GUIStyle popupStyle;

	protected void LoadProperties(SerializedProperty property){
		useConstant = property.FindPropertyRelative("UseConstant");
		constantValue = property.FindPropertyRelative("ConstantValue");
		variable = property.FindPropertyRelative("Variable");
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		LoadProperties(property);
		return base.GetPropertyHeight(property, label);
	}

	/// <summary>
	/// OnGUI is called for rendering and handling GUI events.
	/// This function can be called multiple times per frame (one call per event).
	/// </summary>
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label){
		if(popupStyle==null){
			popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
			popupStyle.imagePosition = ImagePosition.ImageOnly;
		}

		label = EditorGUI.BeginProperty(position, label, property);
		position = EditorGUI.PrefixLabel(position, label);
		
		EditorGUI.BeginChangeCheck();

		// Calculate rect for configuration button
		Rect btnRect = CalculateButtonRect(position);
		position.xMin = CaclulatePositionXMin(btnRect);

		// Store old indent level and set it to 0... the PrefixLabel takes care of it
		int indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		int result = EditorGUI.Popup(btnRect, useConstant.boolValue ? 0 : 1, popupOptions, popupStyle);

		useConstant.boolValue = (result == 0);

		// Show either varible or constant field
		EditorGUI.PropertyField(position, 
			useConstant.boolValue ? constantValue : variable, 
			GUIContent.none, true);

		if(EditorGUI.EndChangeCheck()){
			property.serializedObject.ApplyModifiedProperties();
		}

		EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty();
	}

	public virtual Rect CalculateButtonRect(Rect position){
		Rect btnRect = new Rect(position);
		btnRect.yMin += popupStyle.margin.top;
		btnRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
		return btnRect;
	}

	public virtual float CaclulatePositionXMin(Rect btnRect){
		return btnRect.xMax;
	}
}

