using UnityEngine;

[CreateAssetMenu(menuName="Variables/String")]
public class StringVariable : Variable<string> {

    public override void ApplyChange(string change) { Value += change; }
    public override void ApplyChange(Variable<string> change) { Value += change.Value; }

}