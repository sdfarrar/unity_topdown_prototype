using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TransitionEffect))]
public class TransitionEffectEditor : Editor {

	// Transition drawer variables
	bool m_TransitionFoldout;
	SerializedProperty m_TransitionMaterialProp;
	SerializedProperty m_TransitionTextureProp;
	SerializedProperty m_ResetTransitionTextureProp;
	SerializedProperty m_DefaultTransitionTextureProp;
	SerializedProperty m_CurveProp;
	SerializedProperty m_TransitionDurationProp;

	readonly GUIContent m_TransitionContent = new GUIContent("Transition Settings");
	readonly GUIContent m_TransitionMaterialContent = new GUIContent("Transition Material");
	readonly GUIContent m_TransitionTextureContent = new GUIContent("Transition Texture");
	readonly GUIContent m_ResetTransitionTextureContent = new GUIContent("Reset Texture");
	readonly GUIContent m_DefaultTransitionTextureContent = new GUIContent("Default Texture");
	readonly GUIContent m_CurveContent = new GUIContent("Curve");
	readonly GUIContent m_TransitionDurationContent = new GUIContent("Duration");

	// Flash drawer variables
	bool m_FlashFoldout;
	SerializedProperty m_FlashBeforeTransitionProp;
	SerializedProperty m_FlashesProp;
	SerializedProperty m_FlashTimeProp;
	SerializedProperty m_FlashColorProp;

	readonly GUIContent m_FlashContent = new GUIContent("Flash Settings");
	readonly GUIContent m_FlashBeforeTransitionContent = new GUIContent("Enable Flash");
	readonly GUIContent m_FlashesContent = new GUIContent("Flash Count");
	readonly GUIContent m_FlashTimeContent = new GUIContent("Flash Duration");
	readonly GUIContent m_FlashColorContent = new GUIContent("Flash Color");


	void OnEnable() {
		m_FlashBeforeTransitionProp = serializedObject.FindProperty("FlashBeforeTransition");
		m_FlashesProp = serializedObject.FindProperty("Flashes");
		m_FlashTimeProp = serializedObject.FindProperty("FlashTime");
		m_FlashColorProp = serializedObject.FindProperty("FlashColor");

		m_TransitionMaterialProp = serializedObject.FindProperty("TransitionMaterial");
		m_TransitionTextureProp = serializedObject.FindProperty("TransitionTexture");
		m_ResetTransitionTextureProp = serializedObject.FindProperty("ResetTransitionTexture");
		m_DefaultTransitionTextureProp = serializedObject.FindProperty("DefaultTransitionTexture");
		m_CurveProp = serializedObject.FindProperty("curve");
		m_TransitionDurationProp = serializedObject.FindProperty("transitionTime");
	}
	
	public override void OnInspectorGUI() {
		serializedObject.Update();

		EditorGUILayout.BeginVertical(GUI.skin.box);
		EditorGUI.indentLevel++;

		m_TransitionFoldout = EditorGUILayout.Foldout(m_TransitionFoldout, m_TransitionContent);
		if(m_TransitionFoldout){
			EditorGUILayout.PropertyField(m_TransitionMaterialProp, m_TransitionMaterialContent);
			EditorGUILayout.PropertyField(m_TransitionTextureProp, m_TransitionTextureContent);
			EditorGUILayout.PropertyField(m_ResetTransitionTextureProp, m_ResetTransitionTextureContent);
			if(m_ResetTransitionTextureProp.boolValue){
				EditorGUILayout.PropertyField(m_DefaultTransitionTextureProp, m_DefaultTransitionTextureContent);
			}
			EditorGUILayout.PropertyField(m_CurveProp, m_CurveContent);
			EditorGUILayout.PropertyField(m_TransitionDurationProp, m_TransitionDurationContent);
		}

		EditorGUI.indentLevel--;
		EditorGUILayout.EndVertical();

		EditorGUILayout.BeginVertical(GUI.skin.box);
		EditorGUI.indentLevel++;

		m_FlashFoldout = EditorGUILayout.Foldout(m_FlashFoldout, m_FlashContent);
		if(m_FlashFoldout){
			EditorGUILayout.PropertyField(m_FlashBeforeTransitionProp, m_FlashBeforeTransitionContent);
			EditorGUILayout.PropertyField(m_FlashesProp, m_FlashesContent);
			EditorGUILayout.PropertyField(m_FlashTimeProp, m_FlashTimeContent);
			EditorGUILayout.PropertyField(m_FlashColorProp, m_FlashColorContent);
		}

		EditorGUI.indentLevel--;
		EditorGUILayout.EndVertical();

		serializedObject.ApplyModifiedProperties();
	}



}
