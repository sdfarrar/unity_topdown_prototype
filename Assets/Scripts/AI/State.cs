using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AI {
    [CreateAssetMenu(menuName = "AI/State")]
    public class State : ScriptableObject {
        public Action[] Actions;
        public Transition[] Transitions;
        public Color GizmoColor = Color.gray;

        public void UpdateState(StateController controller){
            DoActions(controller);
            CheckTransitions(controller);
        }

        private void DoActions(StateController controller){
            for(int i=0; i<Actions.Length; ++i){
                Actions[i].Act(controller);
            }
        }

        private void CheckTransitions(StateController controller){
            for(int i=0; i<Transitions.Length; ++i){
                bool decisionSucceeded = Transitions[i].Decision.Decide(controller);
                if(decisionSucceeded){
                }else{

                }
                State toState = (decisionSucceeded) ? Transitions[i].TrueState : Transitions[i].FalseState;
                controller.TransitionToState(toState);
            }
        }

    }
}