using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Variables/String List")]
public class StringListVariable : Variable<List<string>> {

    public override void ApplyChange(List<string> change) { throw new System.NotImplementedException(); }
    public override void ApplyChange(Variable<List<string>> change) { throw new System.NotImplementedException(); }

}