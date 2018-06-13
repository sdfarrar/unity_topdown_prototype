using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TreasureChest : MonoBehaviour, IInteractable {

	public Sprite ClosedImage;
	public Sprite OpenedImage;
	public bool closed = true;

	public GameObject Contents;

	private new SpriteRenderer renderer;

	private void Start(){
		renderer = GetComponent<SpriteRenderer>();
		UpdateSprite();
	}

	void OnEnable() {
		UpdateSprite();
	}

	public void OnInteract(PlayerController player){
		if(!closed){ return; } // already looted

		Debug.Log(gameObject.name + " was interacted with");
		closed = false;
		UpdateSprite();
		//TODO attempt to add contents to player
		//if player cannot hold, reset to closed
	}

	private void UpdateSprite(){
		if(renderer==null){ return; }
		renderer.sprite = (closed) ? ClosedImage : OpenedImage;
	}

}
