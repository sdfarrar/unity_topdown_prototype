using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour {

	private IEnumerable<Weapon> weapons;
	private Weapon activeWeapon;

	public PlayerInput PlayerInput;
	//TODO we need to listen for OnPlayerCollected event so we can change the inventory
	// the collector script on the Player should raise this event when an item is collected
	public Inventory PlayerInventory;

	public LayerMask Attackables;
	public LayerMask Interactables;
	public LayerMask Grabables;

	public int currentWeaponSlot;
	private int lastWeaponSlot;

	private Vector2 direction;
	private PlayerMovement movement;

	private CircleCollider2D Hitbox;
	private Vector2 HitboxSize;
	private BoxCollider2D InteractiveArea;

	private Transform LiftPosition;
	private ILiftable LiftedObject;

	private Collider2D[] ColliderBuffer = new Collider2D[8];
	private Collider2D[] InteractablesBuffer = new Collider2D[4];
	private Collider2D[] LiftablesBuffer = new Collider2D[4];

	public PlayerState State;

	void Start () {
		direction = Vector2.up;
		weapons = GetComponentsInChildren<Weapon>();
		movement = GetComponent<PlayerMovement>();
		Hitbox = GetComponent<CircleCollider2D>();
		HitboxSize = new Vector2(Hitbox.radius, Hitbox.radius);
		InteractiveArea = transform.Find("InteractiveArea").GetComponent<BoxCollider2D>();
		LiftPosition = transform.Find("LiftPosition");
		//ChangeWeapon();
	}
	
	void Update () {
		PlayerInput.Update();
		CheckChangeWeaponInput();

		switch(State){
			case PlayerState.EMPTY: HandleEmpty(); break;
			case PlayerState.CARRY: HandleCarrying(); break;
			case PlayerState.INTERACT: HandleInteracting();	break;
			case PlayerState.GRAB: HandleGrabbing(); break;
			case PlayerState.ATTACK: HandleAttacking();	break;
		}
	}

	public Vector2 GetSize(){
		return HitboxSize;
	}

	public Vector3 GetHitboxPosition(){
		return Hitbox.transform.position + new Vector3(Hitbox.offset.x, Hitbox.offset.y, 0);
	}

	public Transform GetLiftTransform(){
		return LiftPosition;
	}

	private void HandleEmpty(){
		if(PlayerInput.InteractPressed){
			if(Interact()){
				State = PlayerState.INTERACT;
			}else if(LiftObject()){
				State = PlayerState.CARRY;
			}else if(Grab()){
				State = PlayerState.GRAB;
			}
		//}else if(PlayerInput.InteractHeldDown && Grab()){
		}
	}

	private void HandleCarrying(){
		if(PlayerInput.InteractPressed && LiftedObject!=null){
			ThrowObject();
			State = PlayerState.EMPTY;
		}

	}

	private void HandleInteracting(){
		if(PlayerInput.InteractHeldDown){
			State = PlayerState.GRAB;
		}else{
			State = PlayerState.EMPTY;
		}
	}

	private void HandleGrabbing(){
		if(!PlayerInput.InteractHeldDown){
			State = PlayerState.EMPTY;
			movement.enabled = true;
			return;
		}
		//TODO do grab things
	}

	private void HandleAttacking(){

	}

	private void Attack() {
		//if(State.carrying){
		//	//Drop object?
		//}else{
		//	//Attack with sword
		//	//int count = Physics2D.OverlapCircleNonAlloc(transform.position, 1, ColliderBuffer, Attackables);
		//	//Debug.Log("we hit: " + count + " things");
		//	//activeWeapon.PrimaryAttack(movement.GetFacing());
		//}
	}

	private void ThrowObject() {
		Vector3 throwDistance = new Vector2(3, 3);
		Vector3 target = movement.GetFacing() * throwDistance;
		LiftedObject.OnThrow(target);
		LiftedObject = null;
	}

	private bool Interact() {
		IInteractable interactable = GetClosest<IInteractable>(InteractablesBuffer, Interactables);
		if(interactable==null){	return false; }
		return interactable.OnInteract(this);
	}

	private bool LiftObject() {
		ILiftable liftable = GetClosest<ILiftable>(LiftablesBuffer, Interactables);
		if(liftable==null || !liftable.OnPickedUp(this)){ return false; } 

		LiftedObject = liftable;
		return true;
	}

	private bool Grab(){
		Transform obj = GetClosest<Transform>(InteractablesBuffer, Grabables);
		if(obj==null){	return false; }
		movement.enabled = false;
		return true;
	}

	private T GetClosest<T>(Collider2D[] buffer, LayerMask layerMask){
		int count = Physics2D.OverlapBoxNonAlloc(InteractiveArea.transform.position, InteractiveArea.size, 360, buffer, layerMask);
		ClosestObject<T> closest = new ClosestObject<T>{ angle=360 };
		
		for(int i=0; i<count; ++i){
			if(InteractablesBuffer[i]==null){ continue; }
			Transform found = InteractablesBuffer[i].transform;
			Vector3 facingdir = movement.GetFacing();
			float angle = Vector2.Angle(facingdir, (found.position - transform.position).normalized);
			if(angle>45f || angle>closest.angle){ continue; }
			closest.angle = angle;
			closest.obj = found.GetComponent<T>();
		}
		return closest.obj;
	}

	private void CheckChangeWeaponInput(){
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			currentWeaponSlot = 0;
		}else if(Input.GetKeyDown(KeyCode.Alpha2)){
			currentWeaponSlot = 1;
		}else if(Input.GetKeyDown(KeyCode.Alpha3)){
			currentWeaponSlot = 2;
		}
		if(currentWeaponSlot!=lastWeaponSlot){
			ChangeWeapon();
		}

	}

	private void ChangeWeapon(){
		activeWeapon = weapons.Where( weapon => weapon.GetIndex()==currentWeaponSlot ).First();
		activeWeapon.Activate();
		lastWeaponSlot = currentWeaponSlot;
	}

	struct ClosestObject<T> {
		public float angle;
		public T obj;
	}

	public enum PlayerState {
		EMPTY,
		ATTACK,
		CARRY,
		INTERACT,
		GRAB
	}

#if UNITY_EDITOR
	void OnDrawGizmosSelected()	{
		if(Input.GetKey(KeyCode.Keypad5)){
			Handles.color = Color.red;
			Handles.DrawWireDisc(transform.position, Vector3.forward, 1);
		}
	}
#endif

}
