using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class Sign : LiftableObject, IInteractable {

	public StringReference Text;

	private const string PLAYER_LAYER = "Player";


	public void OnInteract(PlayerController player) {
		if(CanInteract(player)){
			Debug.Log(Text.Value); //TODO open up dialog... Freeze player?
		}
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
		return player.transform.position.y + player.GetSize().y/2;
	}

	private float GetBottomPosition(){
		return transform.position.y - Hitbox.size.y/2f;
	}
}
