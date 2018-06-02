
[System.Serializable]
public class IntegerReference : SimpleVariableReference<int> {
	[UnityEngine.SerializeField]
	public IntegerVariable Variable; // Must be named "Variable" for PropertyDrawer to work

	public override Variable<int> GetVariable(){ return Variable; }
}