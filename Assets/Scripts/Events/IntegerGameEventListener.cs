using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class IntegerUnityEvent : UnityEvent<int> {}

public class IntegerGameEventListener : MonoBehaviour {

	[Tooltip("Event to register with")]
	public IntegerGameEvent Event;

	[Tooltip("Response to invoke when Event is raised")]
	public IntegerUnityEvent Response;
	
	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	private void OnEnable()	{
		Event.RegisterListener(this);
	}

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	private void OnDisable() {
		Event.UnregisterListener(this);
	}

	public void OnEventRaised(){
		Response.Invoke(Event.Amount.Value);
	}

}
