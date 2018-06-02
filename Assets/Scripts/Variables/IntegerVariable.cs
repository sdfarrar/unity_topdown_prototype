using UnityEditor;
using UnityEngine;

[CreateAssetMenu()]
public class IntegerVariable : ScriptableObject {

#if UNITY_EDITOR
	[Multiline]
	public string Description = "";
#endif

	public int Value;

	public void SetValue(int v){
		Value = v;
	}

	public void SetValue(IntegerVariable v){
		Value = v.Value;
	}

	public void ApplyChange(int change){
		Value += change;
	}

	public void ApplyChange(IntegerVariable change){
		Value += change.Value;
	}
	
}
