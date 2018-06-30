using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameEvent))]
public class GameEventEditor : Editor {

	public override void OnInspectorGUI() {
		base.OnInspectorGUI();

		GUI.enabled = Application.isPlaying;

		if(GUILayout.Button("Raise")){ 
			GameEvent ev = target as GameEvent;
			if(ev.VariableToTest!=null){
				ev.VariableToTest.ApplyChange(ev.Amount);
			}
			ev.Raise(); 
		}
	}

}

[CustomEditor(typeof(IntegerGameEvent))]
public class IntegerGameEventEditor : Editor {

	public override void OnInspectorGUI() {
		base.OnInspectorGUI();

		GUI.enabled = Application.isPlaying;

		if(GUILayout.Button("Raise")){ 
			IntegerGameEvent ev = target as IntegerGameEvent;
			ev.Raise(); 
		}
	}

}