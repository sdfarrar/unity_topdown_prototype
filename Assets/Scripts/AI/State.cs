using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AI {
    [CreateAssetMenu(menuName = "AI/State")]
    public class State : ScriptableObject {
        public Action[] Actions;
        public Transition[] Transitions;
        public Color GizmoColor = Color.gray;

        //TODO probably add OnStateEnter and OnStateExit methods

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
                if(Transitions[i].Decision==null){ continue; }

                bool decisionSucceeded = Transitions[i].Decision.Decide(controller);
                State toState = (decisionSucceeded) ? Transitions[i].TrueState : Transitions[i].FalseState;
                controller.TransitionToState(toState);
            }
        }

    }
}