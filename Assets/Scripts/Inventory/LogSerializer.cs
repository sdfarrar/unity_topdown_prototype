using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LogSerializer : Serializer {

	private string m_json;

    public override T Deserialize<T>(object key, T target) {
		JsonUtility.FromJsonOverwrite (m_json, target);
		return target;
    }

    public override void Serialize(object key, object data) {
		string json = JsonUtility.ToJson(data);
		Debug.Log(json);
		m_json = json;
    }

}
