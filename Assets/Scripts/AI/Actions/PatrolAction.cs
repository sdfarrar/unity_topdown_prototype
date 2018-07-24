using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    [CreateAssetMenu(menuName = "AI/Actions/Patrol")]
    public class PatrolAction : Action {
        public override void Act(StateController controller) {
            Patrol(controller);
        }

        private void Patrol(StateController controller) {
            Vector3 waypoint = controller.Waypoints[controller.NextWaypoint].position;
            controller.transform.position = Vector3.MoveTowards(controller.transform.position, waypoint, 5f * Time.deltaTime);
            if(waypoint == controller.transform.position){
                controller.NextWaypoint = (controller.NextWaypoint + 1) % controller.Waypoints.Length;
            }
        }
    }
}