using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerController : MonoBehaviour {

	private IEnumerable<Weapon> weapons;
	private Weapon activeWeapon;

	public PlayerInput PlayerInput;

	public LayerMask Attackables;
	public LayerMask Interactables;

	public int currentWeaponSlot;
	private int lastWeaponSlot;

	private Vector2 direction;
	private PlayerMovement movement;

	private BoxCollider2D Hitbox;
	private BoxCollider2D InteractiveArea;

	private Transform LiftPosition;

	private Collider2D[] ColliderBuffer = new Collider2D[8];
	private Collider2D[] InteractablesBuffer = new Collider2D[4];
	private Collider2D[] LiftablesBuffer = new Collider2D[4];

	private PlayerState CurrentState, PreviousState;
	public PlayerState State; //temp

	void Start () {
		direction = Vector2.up;
		weapons = GetComponentsInChildren<Weapon>();
		movement = GetComponent<PlayerMovement>();
		Hitbox = GetComponent<BoxCollider2D>();
		InteractiveArea = transform.Find("InteractiveArea").GetComponent<BoxCollider2D>();
		LiftPosition = transform.Find("LiftPosition");
		ChangeWeapon();
	}
	
	void Update () {
		PlayerInput.Update();
		CheckChangeWeaponInput();

		if(PlayerInput.AttackPressed){
			Attack();
		}else if(PlayerInput.InteractPressed){
			if(State.carrying){
				ThrowObject();
			}else{
				Interact();
				LiftObject();
			}
		}

		PreviousState = CurrentState;
	}

	public Vector2 GetSize(){
		return Hitbox.size;
	}

	public Transform GetLiftTransform(){
		return LiftPosition;
	}

	private void Attack() {
		if(State.carrying){
			//Drop object?
		}else{
			//Attack with sword
			//int count = Physics2D.OverlapCircleNonAlloc(transform.position, 1, ColliderBuffer, Attackables);
			//Debug.Log("we hit: " + count + " things");
			//activeWeapon.PrimaryAttack(movement.GetFacing());
		}
	}

	private void ThrowObject() {
		State.carrying = false;
		if(State.liftedObject==null){ return; } // shouldnt happen 
		Vector3 throwDistance = new Vector2(3, 3);
		Vector3 target = movement.GetFacing() * throwDistance;
		State.liftedObject.OnThrow(target);
		State.liftedObject = null;
	}

	private void Interact() {
		if(State.interacting){ return; }

		IInteractable interactable = GetClosest<IInteractable>(InteractablesBuffer, Interactables);
		if(interactable==null){
			State.interacting = false;
			return;
		}
		interactable.OnInteract(this);
		//State.interacting = true; //TODO uncomment when we figure things out
	}

	private void LiftObject() {
		if(State.carrying){ return; }

		ILiftable liftable = GetClosest<ILiftable>(LiftablesBuffer, Interactables);
		if(liftable==null){ return; } 

		if(liftable.OnPickedUp(this)){
			State.carrying = true;
			State.liftedObject = liftable;
		}
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

	[System.Serializable]
	public struct PlayerState {
		public bool idle;
		public bool attacking;
		public bool interacting;

		public bool carrying;
		public ILiftable liftedObject;

		public void Reset(){
			idle = false;
			attacking = false;
			carrying = false;
			interacting = false;
			liftedObject = null;
		}
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
