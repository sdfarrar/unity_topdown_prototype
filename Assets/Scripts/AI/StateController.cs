using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public class StateController : MonoBehaviour {

        public State CurrentState;
        //public EnemyStats enemyStats;
        public Transform Eyes;
        //public State RemainState;

        public Transform WaypointsParent;
        [HideInInspector] public Transform[] Waypoints;
        [HideInInspector] public int NextWaypoint;
        [HideInInspector] public Transform ChaseTarget;
        [HideInInspector] public float TimeInState;

        private bool aiActive;

        void Awake () {
            aiActive = true;
            Waypoints = WaypointsParent.transform.Cast<Transform>().ToArray(); // GetComponentsInChildren returns WaypointsParent + children so we do this instead
        }

        //TODO may not even need this
        public void SetupAI(bool isAiActive, List<Transform> waypoints){
            Debug.Log("SetupAI");
            //Waypoints = waypoints;
            aiActive = isAiActive;
        }
        
        // Update is called once per frame
        void Update () {
            if(!aiActive){ return; }
            CurrentState.UpdateState(this);            
        }

        public void TransitionToState(State nextState){
            //if(nextState==RemainState){ return; }
            CurrentState = nextState;
            OnExitState();
        }

        public bool CheckIfCountDownElapsed(float duration){
            TimeInState += Time.deltaTime;
            return (TimeInState >= duration);
        }

        private void OnExitState(){
            TimeInState = 0;
        }

    }
}