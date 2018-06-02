using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameEvent))]
public class GameEventEditor : Editor {

	public override void OnInspectorGUI() {
		base.OnInspectorGUI();

		GUI.enabled = Application.isPlaying;

		if(GUILayout.Button("Raise")){ 
			GameEvent ev = target as GameEvent;
			ev.VariableToTest.ApplyChange(ev.Amount);
			ev.Raise(); 
		}
	}

}
