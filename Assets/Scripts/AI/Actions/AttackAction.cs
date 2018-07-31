using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AI{
    [CreateAssetMenu(menuName="AI/Actions/Attack")]
    public class AttackAction : Action {
        public override void Act(StateController controller) {
            Attack(controller);
        }

        private void Attack(StateController controller) {
            float attackRate = 0.5f;

            Vector3 origin = controller.Eyes.transform.position;

            controller.GetComponent<SpriteRenderer>().color = Color.white;//TODO remove

            //int hits = Physics2D.OverlapCircleNonAlloc(origin, attackRange, controller.collisionEntities, controller.Eyes.targetMask);
            Collider2D collider = Physics2D.OverlapCircle(origin, controller.EnemyStats.AttackRange, controller.Eyes.targetMask);
            if(collider==null){ return; }

            if(controller.CheckIfCountDownElapsed(controller.EnemyStats.AttackRate)){
                controller.GetComponent<SpriteRenderer>().color = Color.red;//TODO remove
            }

        }
    }
}