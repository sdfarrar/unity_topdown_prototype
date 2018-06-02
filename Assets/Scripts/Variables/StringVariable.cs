using UnityEditor;
using UnityEngine;

[CreateAssetMenu()]
public class StringVariable : ScriptableObject {

#if UNITY_EDITOR
	[Multiline]
	public string Description = "";
#endif

	public string Value;

	public void SetValue(string v){
		Value = v;
	}

	public void SetValue(StringVariable v){
		Value = v.Value;
	}

	public void ApplyChange(string change){
		Value += change;
	}

	public void ApplyChange(StringVariable change){
		Value += change.Value;
	}
	
}
