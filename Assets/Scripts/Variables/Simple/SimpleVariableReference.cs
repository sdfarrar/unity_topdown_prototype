
public abstract class SimpleVariableReference<T> : VariableReference {

	public bool UseConstant = true;
	public T ConstantValue;

	public SimpleVariableReference(){}

	public SimpleVariableReference(T value) {
		UseConstant = true;
		ConstantValue = value;
	}

	public abstract Variable<T> GetVariable();

	public T Value {
		get { return UseConstant ? ConstantValue : GetVariable().Value; }
	}

	public static implicit operator T(SimpleVariableReference<T> reference) {
		return reference.Value;
	}

}
