using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public abstract class LiftableObject : MonoBehaviour, ILiftable {

	private const string PLAYER_LAYER = "Player";
	private const int SORTING_ORDER = 100;

	private bool alreadyLifted = false;
	private const float ThrowTime = 0.25f;
	private float elpased;

	protected BoxCollider2D Hitbox;
	protected new SpriteRenderer renderer;

	protected abstract bool CanPickUp(PlayerController player);

    void Awake() {
		Hitbox = GetComponent<BoxCollider2D>();
		renderer = GetComponent<SpriteRenderer>();
	}
	

    public bool OnPickedUp(PlayerController player) {
		if(alreadyLifted){ return false; }
		if(!CanPickUp(player)){ return false; }

		alreadyLifted = true;
		Hitbox.isTrigger = true;
		UpdateTransform(player.GetLiftTransform());
		UpdateRenderer();
		return true;
    }

    public void OnThrow(Vector3 distance) {
		Vector3 target = transform.position + distance;
		Vector3 startPosition = transform.position;
		UpdateTransform(null);
		StartCoroutine(ThrowTo(startPosition, target));
    }

	private void UpdateTransform(Transform newParent){
		transform.SetParent(newParent);
		transform.localPosition = Vector3.zero;
	}

	private void UpdateRenderer(){
		renderer.sortingLayerName = PLAYER_LAYER;
		renderer.sortingOrder = SORTING_ORDER;
	}

	private IEnumerator ThrowTo(Vector3 startPosition, Vector3 target){
		elpased = 0;
		while(elpased <= ThrowTime){
			float delta = elpased/ThrowTime;
			transform.position = Vector3.Lerp(startPosition, target, delta);
			elpased += Time.deltaTime;
			yield return null;
		}
		// do break animation if one exists
		Destroy(this.gameObject);
	}
}
