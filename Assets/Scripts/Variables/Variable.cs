using UnityEngine;

public abstract class Variable<T> : ScriptableObject {

#if UNITY_EDITOR
	[Multiline]
	public string Description = "";
#endif

	public T Value;

	public void SetValue(T v){
		Value = v;
	}

	public void SetValue(Variable<T> v){
		Value = v.Value;
	}

	public abstract void ApplyChange(T change);
	public abstract void ApplyChange(Variable<T> change);

}
