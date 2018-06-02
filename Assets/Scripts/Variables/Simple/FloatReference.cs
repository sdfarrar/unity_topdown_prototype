
[System.Serializable]
public class FloatReference : SimpleVariableReference<float> {
	[UnityEngine.SerializeField]
	public FloatVariable Variable; // Must be named "Variable" for PropertyDrawer to work

	public override Variable<float> GetVariable(){ return Variable; }
}
