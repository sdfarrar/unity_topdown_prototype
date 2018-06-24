using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class Sign : LiftableObject, IInteractable {

	public StringReference Text;

	public bool OnInteract(PlayerController player) {
		if(!CanInteract(player)){ return false; }

		Debug.Log(Text.Value); //TODO open up dialog... Freeze player?
		return true;
	}

	private bool CanInteract(PlayerController player) {
		// if player is below the sign, return true
		return GetPlayerTopPosition(player) < GetBottomPosition();
	}

	protected override bool CanPickUp(PlayerController player){
		// if player is not below the sign, return true
		return GetPlayerTopPosition(player) >= GetBottomPosition();
	}

	private float GetPlayerTopPosition(PlayerController player){
		return player.GetHitboxPosition().y + player.GetSize().y/2;
	}

	private float GetBottomPosition(){
		return transform.position.y - Hitbox.size.y/2f;
	}
}
