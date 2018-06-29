using UnityEngine;

[CreateAssetMenu(menuName="Variables/Texture")]
public class TextureVariable : Variable<Texture> {

    public override void ApplyChange(Texture change) { throw new System.NotImplementedException(); }
    public override void ApplyChange(Variable<Texture> change) { throw new System.NotImplementedException(); }

}