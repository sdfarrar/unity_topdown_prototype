using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class IntegerGameEvent : ScriptableObject {

	public IntegerReference Amount;

	private readonly List<IntegerGameEventListener> listeners = new List<IntegerGameEventListener>();

	public void Raise(){
		for(int i=listeners.Count-1; i>=0; --i){
			listeners[i].OnEventRaised();
		}
	}

	public void RegisterListener(IntegerGameEventListener listener){
		if(!listeners.Contains(listener)){ listeners.Add(listener); }
	}

	public bool UnregisterListener(IntegerGameEventListener listener){
		return listeners.Remove(listener);
	}
}
