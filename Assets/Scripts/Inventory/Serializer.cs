using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Serializer : ScriptableObject {

	public abstract void Serialize(object key, object data);
	public abstract T Deserialize<T>(object key, T target);

}
