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
		CurrentState = State.Patrolling;
        Waypoints = WaypointsParent.transform.Cast<Transform>().ToArray(); // GetComponentsInChildren returns WaypointsParent + children so we do this instead
        UpdateFacing();
	}
	
	void Update() {
        GetComponentInChildren<SpriteRenderer>().color = Color.white;//TODO remove

        CurrentState = Look();
        switch(CurrentState){
            case State.Patrolling: Patrol(); break;
            case State.Scanning: Scan(); break;
            case State.Chasing: Chase(); Attack(); break;
        }
	}

    private State Look() {
        if(Eyes.visibleTargets.Count==0){ return CurrentState; }
        target = Eyes.LastTarget;
        return State.Chasing;
    }

    private void Patrol() {
        FromWaypointIndex %= Waypoints.Length;
        int toWaypointIndex = (FromWaypointIndex+1) % Waypoints.Length;
        Vector3 waypoint = Waypoints[toWaypointIndex].position;
        Character.transform.position = Vector3.MoveTowards(Character.transform.position, waypoint, Stats.MoveSpeed * Time.deltaTime);
        if(waypoint == Character.transform.position){
            ComputeNextWaypoint();
            CurrentState = State.Scanning;
            rotateLeft = true;
            rotateRight = false;
            elapsedRotation = 0;
        }
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
            UpdateFacing();
        }
    }

    private void UpdateFacing() {
        int toWaypointIndex = (FromWaypointIndex+1) % Waypoints.Length;
        Vector3 heading = Waypoints[toWaypointIndex].transform.position - Character.transform.position;
        Vector3 direction = heading.normalized;
        lastFacingAngle = facingAngle;
        facingAngle = Vector2.SignedAngle(Vector2.up, direction); // TODO should we limit this to multiples of 90?
        Eyes.transform.localRotation = Quaternion.Euler(0, 0, facingAngle);
    }

    private void Chase() {
        Transform target = Eyes.LastTarget;
        if(target==null){ return; }
        Character.transform.position = Vector3.MoveTowards(Character.transform.position, target.position, Stats.MoveSpeed * Time.deltaTime);
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
        Eyes.transform.localRotation = Quaternion.Lerp(from, to, t);
        return Eyes.transform.localRotation.eulerAngles.z==to.eulerAngles.z;
    }

    private bool RotateRight() {
        Quaternion from = Eyes.transform.localRotation;
        float angle = facingAngle + 91f;
        Quaternion to = Quaternion.Euler(0, 0, angle);
        float t = elapsedRotation/rotationSpeed;
        elapsedRotation += Time.deltaTime;
        Eyes.transform.localRotation = Quaternion.Lerp(from, to, t);
        return Eyes.transform.localRotation.eulerAngles.z==to.eulerAngles.z;
    }

    private bool RotateCenter() {
        Quaternion from = Eyes.transform.localRotation;
        Quaternion to = Quaternion.Euler(0, 0, facingAngle);
        float t = elapsedRotation/rotationSpeed;
        elapsedRotation += Time.deltaTime;
        Eyes.transform.localRotation = Quaternion.Lerp(from, to, t);
        return Eyes.transform.localRotation.eulerAngles.z==to.eulerAngles.z;
    }

}
