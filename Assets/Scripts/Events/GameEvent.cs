using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GameEvent : ScriptableObject {

#if UNITY_EDITOR
	public IntegerVariable VariableToTest;
	public IntegerReference Amount;
#endif

	private readonly List<GameEventListener> listeners = new List<GameEventListener>();

	public void Raise(){
		for(int i=listeners.Count-1; i>=0; --i){
			listeners[i].OnEventRaised();
		}
	}

	public void RegisterListener(GameEventListener listener){
		if(!listeners.Contains(listener)){ listeners.Add(listener); }
	}

	public bool UnregisterListener(GameEventListener listener){
		return listeners.Remove(listener);
	}
}
