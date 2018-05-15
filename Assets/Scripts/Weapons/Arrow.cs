using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Arrow : MonoBehaviour {

	public float speed = 10f;
	public Vector3 direction;

	void Update () {
		transform.position += direction * speed * Time.deltaTime;
	}

}
