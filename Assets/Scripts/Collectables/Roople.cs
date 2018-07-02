using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Roople : MonoBehaviour, ICollectable {

	public RoopleTemplate template;

	private int value;

	private void OnEnable(){
		GetComponent<SpriteRenderer>().sprite = template.sprite;
	}

	void Start () {
		value = template.value;
	}
	
	public void OnCollect(Inventory inventory){
		inventory.ApplyChangeToWallet(value);
		Destroy(this.gameObject);
	}

}
