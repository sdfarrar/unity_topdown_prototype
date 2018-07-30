using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    [CreateAssetMenu(menuName="AI/Decisions/Look")]
    public class LookDecision : Decision {

        public override bool Decide(StateController controller) {
            bool targetVisible = Look(controller);
            return targetVisible;
        }

        private bool Look(StateController controller) {
            return controller.Eyes.visibleTargets.Count!=0;
        }

    }
}