using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    [CreateAssetMenu(menuName = "AI/Actions/Chase")]
    public class ChaseAction : Action {
        public override void Act(StateController controller) {
            Chase(controller);
        }

        private void Chase(StateController controller) {
            //TODO Chase
        }
    }
}
