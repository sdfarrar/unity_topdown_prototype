using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darknut : MonoBehaviour {

    public EnemyStats Stats;
    public Transform Character;
    public Animator Animator;
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
        ScanCenter,
        ScanLeft,
        ScanRight,
        Chasing,
    }

	void Awake() {
        animatorController = new AnimatorController(Animator);
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
            case State.ScanLeft: 
            case State.ScanRight:
            case State.ScanCenter: Scan(); break;
            case State.Chasing: velocity = Chase(); Attack(); break;
        }
        Character.transform.Translate(velocity);
        animatorController.Update(velocity, CurrentState);
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
            CurrentState = State.ScanLeft;
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
            CurrentState = State.ScanRight;
            rotateLeft = false;
            rotateRight = true;
            elapsedRotation = 0;
            return;
        }
        if(rotateRight && RotateRight()){
            CurrentState = State.ScanCenter;
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

        private Vector2 lastMovementDirection;

        public AnimatorController(Animator anim) {
            this.anim = anim;
        }

        internal void Update(Vector2 velocity, State state) {
            anim.transform.localScale = new Vector3(1, 1, 1);

            bool isMoving = (velocity.x!=0 || velocity.y!=0);
            anim.SetBool("IsMoving", isMoving);

            bool isScanning = (state==State.ScanLeft || state==State.ScanRight || state==State.ScanCenter);
            anim.SetBool("IsScanning", isScanning);

            // Only update if we're moving so we can determine what dirction to use when Idle
            if(isMoving){
                anim.SetFloat("MoveX", velocity.x);
                anim.SetFloat("MoveY", velocity.y);
                FlipScale(velocity);
                lastMovementDirection = velocity;
            }else{
                FlipScale(lastMovementDirection);
            }
            if(isScanning){
                // Left:-1 Right:1 Center:0
                int scanDir = state==State.ScanLeft ? -1 : state==State.ScanRight ? 1 : 0;
                anim.SetFloat("ScanDir", scanDir);
            }
        }

        void FlipScale(Vector2 movement){
            if(SnapTo90(movement)==Vector2.left){
                anim.transform.localScale = new Vector3(-1, 1, 1);
            }else{
                anim.transform.localScale = new Vector3(1, 1, 1);
            }
        }


        // Works fine but in instances where the movement flip flops quickly, it can look bad.
        // Some sort of buffer would work good here
        private Vector2 SnapTo90(Vector2 movement){
            if(movement.x==1 || movement.y==1 || movement.x==-1 || movement.y==-1){ return movement; } // already at some 90 degree

            if(movement.x >= 0 && movement.y >= 0){ // right and up
                return AbsCompareVector2Components(movement) ? Vector2.right : Vector2.up;
            }else if(movement.x >= 0 && movement.y <= 0){ // right and down
                return AbsCompareVector2Components(movement) ? Vector2.right : Vector2.down;
            }else if(movement.x <= 0 && movement.y <= 0){ // left and down
                return AbsCompareVector2Components(movement) ? Vector2.left : Vector2.down;
            }else if(movement.x <= 0 && movement.y >= 0){ // left and up
                return AbsCompareVector2Components(movement) ? Vector2.left : Vector2.up;
            }
            Debug.LogWarning("SnapTo90: Missed some condition. Direction: " + movement);
            return movement;
        }

        private bool AbsCompareVector2Components(Vector2 v){
            return Mathf.Abs(v.x) >= Mathf.Abs(v.y);
        }

    }
}