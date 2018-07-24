using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI{
    [CreateAssetMenu(menuName="AI/Decisions/ActiveState")]
    public class ActiveStateDecision : Decision {
        public override bool Decide(StateController controller) {
            bool chaseTargetIsActive = controller.ChaseTarget.gameObject.activeSelf;
            return chaseTargetIsActive;
        }
    }
}