using UnityEngine;

[CreateAssetMenu(menuName="Variables/Float")]
public class FloatVariable : Variable<float> {
 
  public override void ApplyChange(float change) { Value += change; }
  public override void ApplyChange(Variable<float> change) { Value += change.Value; }

}
