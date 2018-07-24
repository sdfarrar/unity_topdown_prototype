using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI{
    [CreateAssetMenu(menuName="AI/Decisions/Scan")]
    public class ScanDecision : Decision {
        public override bool Decide(StateController controller) {
            bool noEnemyInSight = Scan(controller);
            return noEnemyInSight;
        }

        private bool Scan(StateController controller) {
            //TODO stop moving, turn eyes, get time from controller
            return controller.CheckIfCountDownElapsed(.5f);
        }
    }
}