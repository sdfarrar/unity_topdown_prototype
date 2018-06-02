using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Roople : MonoBehaviour, ICollectable {

	public RoopleTemplate template;

	private int value;

	void Start () {
		value = template.value;
		GetComponent<SpriteRenderer>().sprite = template.sprite;
	}
	
	public void OnCollect(){
		Destroy(this.gameObject);
	}

}
