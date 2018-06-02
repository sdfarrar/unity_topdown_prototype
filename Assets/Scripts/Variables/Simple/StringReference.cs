
[System.Serializable]
public class StringReference : SimpleVariableReference<string> {
	[UnityEngine.SerializeField]
	public StringVariable Variable; // Must be named "Variable" for PropertyDrawer to work

	public override Variable<string> GetVariable(){ return Variable; }
}