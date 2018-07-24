using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    //TODO maybe a struct would work better here?
    [System.Serializable]
    public class Transition {
        public Decision Decision;
        public State TrueState;
        public State FalseState;
    }
}