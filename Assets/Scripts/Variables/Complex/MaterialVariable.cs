using UnityEngine;

[CreateAssetMenu(menuName="Variables/Material")]
public class MaterialVariable : Variable<Material> {

    public override void ApplyChange(Material change) { throw new System.NotImplementedException(); }
    public override void ApplyChange(Variable<Material> change) { throw new System.NotImplementedException(); }

}