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
            //Quaternion from = controller.Eyes.transform.rotation;
            //Quaternion to = (from.eulerAngles.z==180)
            //    ? Quaternion.Euler(0, 0, 90)
            //    : Quaternion.Euler(0, 0, 270);
            //float step = 180 * Time.deltaTime;
            //controller.Eyes.transform.rotation = Quaternion.RotateTowards(from, to, step);
            
            bool elapsed = controller.CheckIfCountDownElapsed(.5f);
            //if(elapsed){
            //    controller.Eyes.transform.rotation = to;
            //}
            return elapsed;
        }
    }
}