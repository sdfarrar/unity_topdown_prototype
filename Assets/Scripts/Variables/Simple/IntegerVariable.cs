using UnityEngine;

public class IntegerVariable : Variable<int> {

    public override void ApplyChange(int change) { Value += change; }
    public override void ApplyChange(Variable<int> change) { Value += change.Value; }

}