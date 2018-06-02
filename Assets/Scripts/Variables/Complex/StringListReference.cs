using System.Collections.Generic;

[System.Serializable]
public class StringListReference : SimpleVariableReference<List<string>> {
	[UnityEngine.SerializeField]
	public StringListVariable Variable; // Must be named "Variable" for PropertyDrawer to work

	public override Variable<List<string>> GetVariable(){ return Variable; }
}