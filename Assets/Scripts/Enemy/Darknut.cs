using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darknut : MonoBehaviour {

    public EnemyStats Stats;
    public Transform Character;
    public FieldOfView Eyes;

    public Transform WaypointsParent;
    public bool CyclicalWaypoints;
    [HideInInspector] public Transform[] Waypoints;
    private int FromWaypointIndex;

    public State CurrentState;

    private AnimatorController animatorController;

    //private Vector2 velocity;
    private Vector2 direction;

    private Transform target;
    private float facingAngle;
    private float lastFacingAngle;

    private bool rotateLeft, rotateRight;
    private float elapsedRotation;
    private float rotationSpeed = 1f;

    public enum State {
        Patrolling,
        Scanning,
        Chasing,
    }

	void Awake() {
        animatorController = new AnimatorController(GetComponentInChildren<Animator>());
		CurrentState = State.Patrolling;
        Waypoints = WaypointsParent.transform.Cast<Transform>().ToArray(); // GetComponentsInChildren returns WaypointsParent + children so we do this instead
        UpdateFacing();
	}
	
	void Update() {
        GetComponentInChildren<SpriteRenderer>().color = Color.white;//TODO remove

        Vector2 velocity = Vector2.zero;

        CurrentState = Look();
        switch(CurrentState){
            case State.Patrolling: velocity = Patrol(); break;
            case State.Scanning: Scan(); break;
            case State.Chasing: velocity = Chase(); Attack(); break;
        }
        Character.transform.Translate(velocity);
        animatorController.Update(velocity, direction);
	}

    private State Look() {
        if(Eyes.visibleTargets.Count==0){ return CurrentState; }
        target = Eyes.LastTarget;
        return State.Chasing;
    }

    private Vector2 Patrol() {
        FromWaypointIndex %= Waypoints.Length;
        int toWaypointIndex = (FromWaypointIndex+1) % Waypoints.Length;
        Vector3 waypoint = Waypoints[toWaypointIndex].position;
        Vector3 newPos = Vector3.MoveTowards(Character.transform.position, waypoint, Stats.MoveSpeed * Time.deltaTime);
        if(waypoint == Character.transform.position){
            ComputeNextWaypoint();
            CurrentState = State.Scanning;
            rotateLeft = true;
            rotateRight = false;
            elapsedRotation = 0;
        }
        return newPos - Character.transform.position;
    }

    private void ComputeNextWaypoint(){
        ++FromWaypointIndex;
        if(!CyclicalWaypoints && FromWaypointIndex>=Waypoints.Length-1){
            FromWaypointIndex = 0;
            System.Array.Reverse(Waypoints); // Probably ineffecient.
        }

    }

    private void Scan(){
        if(rotateLeft && RotateLeft()){
            rotateLeft = false;
            rotateRight = true;
            elapsedRotation = 0;
            return;
        }
        if(rotateRight && RotateRight()){
            rotateLeft = rotateRight = false;
            elapsedRotation = 0;
            return;
        }
        if(!rotateLeft && !rotateRight && RotateCenter()){
            CurrentState = State.Patrolling;
            elapsedRotation = 0;
            UpdateFacing();
        }
    }

    private void UpdateFacing() {
        int toWaypointIndex = (FromWaypointIndex+1) % Waypoints.Length;
        Vector3 heading = Waypoints[toWaypointIndex].transform.position - Character.transform.position;
        direction = heading.normalized;
        lastFacingAngle = facingAngle;
        facingAngle = Vector2.SignedAngle(Vector2.up, direction); // TODO should we limit this to multiples of 90?
        Eyes.transform.localRotation = Quaternion.Euler(0, 0, facingAngle);
    }

    private Vector2 Chase() {
        Transform target = Eyes.LastTarget;
        if(target==null){ return Vector2.zero; }
        Vector3 newPos = Vector3.MoveTowards(Character.transform.position, target.position, Stats.MoveSpeed * Time.deltaTime);
        return newPos - Character.transform.position;
    }

    private void Attack() {
        Vector3 origin = Eyes.transform.position;


        Collider2D collider = Physics2D.OverlapCircle(origin, Stats.AttackRange, Eyes.targetMask);
        if(collider==null){ return; }

        //if(controller.CheckIfCountDownElapsed(Stats.AttackRate)){
            GetComponentInChildren<SpriteRenderer>().color = Color.red;//TODO remove
        //}

    }

    private bool RotateLeft() {
        Quaternion from = Eyes.transform.localRotation;
        float angle = facingAngle - 89f;
        Quaternion to = Quaternion.Euler(0, 0, angle);
        float t = elapsedRotation/rotationSpeed;
        elapsedRotation += Time.deltaTime;
        //Eyes.transform.localRotation = Quaternion.Lerp(from, to, t);
        Eyes.transform.localRotation = to;
        return elapsedRotation >= rotationSpeed;
    }

    private bool RotateRight() {
        Quaternion from = Eyes.transform.localRotation;
        float angle = facingAngle + 91f;
        Quaternion to = Quaternion.Euler(0, 0, angle);
        float t = elapsedRotation/rotationSpeed;
        elapsedRotation += Time.deltaTime;
        //Eyes.transform.localRotation = Quaternion.Lerp(from, to, t);
        Eyes.transform.localRotation = to;
        return elapsedRotation >= rotationSpeed;
    }

    private bool RotateCenter() {
        Quaternion from = Eyes.transform.localRotation;
        Quaternion to = Quaternion.Euler(0, 0, facingAngle);
        float t = elapsedRotation/rotationSpeed;
        elapsedRotation += Time.deltaTime;
        //Eyes.transform.localRotation = Quaternion.Lerp(from, to, t);
        Eyes.transform.localRotation = to;
        return elapsedRotation >= rotationSpeed;
    }


    private class AnimatorController {
        Animator anim;

        public AnimatorController(Animator anim) {
            this.anim = anim;
        }

        internal void Update(Vector2 velocity, Vector2 direction) {
            anim.transform.localScale = new Vector3(1, 1, 1);
            SetAnimTrigger(direction);
            if(velocity==Vector2.zero){
                IdleOnFirstFrame();
            }else{
                anim.speed = 1;
            }

        }

        void IdleOnFirstFrame(){
            // Reset state and freeze on first frame
            int hash = anim.GetCurrentAnimatorStateInfo(0).fullPathHash;
            anim.Play(hash, 0, 0);
            anim.speed = 0;
        }

        //void Idle(Vector2 direction) {
        //    anim.speed = 0;
        //    if(direction==Vector2.up){
        //        anim.SetTrigger("MoveUp");
        //    }else if(direction==Vector2.down){
        //        anim.SetTrigger("MoveDown");
        //    }else if(direction==Vector2.right){
        //        anim.SetTrigger("MoveRight");
        //    }else if(direction==Vector2.left){
        //        anim.SetTrigger("MoveRight");
        //        anim.transform.localScale = new Vector3(-1, 1, 1);
        //    }
        //    int hash = anim.GetCurrentAnimatorStateInfo(0).fullPathHash;
        //    anim.Play(hash, 0, 0);
        //}

        //void Moving(Vector2 direction) {
        //    anim.speed = 1;
        //    if(direction==Vector2.up){
        //        anim.SetTrigger("MoveUp");
        //    }else if(direction==Vector2.down){
        //        anim.SetTrigger("MoveDown");
        //    }else if(direction==Vector2.right){
        //        anim.SetTrigger("MoveRight");
        //    }else if(direction==Vector2.left){
        //        anim.SetTrigger("MoveRight");
        //        anim.transform.localScale = new Vector3(-1, 1, 1);
        //    }
        //}

        void SetAnimTrigger(Vector2 direction){
            if(direction==Vector2.up){
                anim.SetTrigger("MoveUp");
            }else if(direction==Vector2.down){
                anim.SetTrigger("MoveDown");
            }else if(direction==Vector2.right){
                anim.SetTrigger("MoveRight");
            }else if(direction==Vector2.left){
                anim.SetTrigger("MoveRight");
                anim.transform.localScale = new Vector3(-1, 1, 1);
            }
        }

    }
}