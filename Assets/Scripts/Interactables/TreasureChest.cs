using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class TreasureChest : MonoBehaviour, IInteractable {

	public Sprite ClosedImage;
	public Sprite OpenedImage;
	public bool closed = true;

	public GameObject Contents;

	private new SpriteRenderer renderer;
	private BoxCollider2D Hitbox;

	private void Start(){
		renderer = GetComponent<SpriteRenderer>();
		Hitbox = GetComponent<BoxCollider2D>();
		UpdateSprite();
	}

	void OnEnable() {
		UpdateSprite();
	}

	public bool OnInteract(PlayerController player){
		if(!closed){ return false; } // already looted
		if(!CanInteract(player)){ return false; } // not below chest

		Debug.Log(gameObject.name + " was interacted with");
		closed = false;
		UpdateSprite();
		//TODO attempt to add contents to player
		//if player cannot hold, reset to closed
		return true;
	}

	private void UpdateSprite(){
		if(renderer==null){ return; }
		renderer.sprite = (closed) ? ClosedImage : OpenedImage;
	}

	private bool CanInteract(PlayerController player) {
		// if player is below the chest, return true
		return GetPlayerTopPosition(player) < GetBottomPosition();
	}

	private float GetPlayerTopPosition(PlayerController player){
		return player.transform.position.y + player.GetSize().y/2;
	}

	private float GetBottomPosition(){
		return transform.position.y - Hitbox.size.y/2f;
	}
}
