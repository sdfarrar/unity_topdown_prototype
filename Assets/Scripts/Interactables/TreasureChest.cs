using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class TreasureChest : MonoBehaviour, IInteractable {

	public Sprite ClosedImage;
	public Sprite OpenedImage;
	public bool closed = true;

	public TreasureChestContents Contents;

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

		closed = false;
		UpdateSprite();
		AddItemToInventory(player.PlayerInventory);
		if(!Contents.Text.Equals("")){
			//TODO do ui text stuff
			Debug.Log("TreasureChest text: " + Contents.Text);
		}
		return true;
	}

	private void UpdateSprite(){
		if(renderer==null){ return; }
		renderer.sprite = (closed) ? ClosedImage : OpenedImage;
	}

	private void AddItemToInventory(Inventory inventory){
        //TODO should check if this returns false in the event item does not fit in inventory
        Contents.AddToInventory(inventory);
	}

	// if player is below the chest, return true
	private bool CanInteract(PlayerController player) {
		return GetPlayerTopPosition(player) < GetBottomPosition();
	}

	private float GetPlayerTopPosition(PlayerController player){
		return player.GetHitboxPosition().y + player.GetSize().y/2;
	}

	private float GetBottomPosition(){
		return transform.position.y - Hitbox.size.y/2f;
	}
}
