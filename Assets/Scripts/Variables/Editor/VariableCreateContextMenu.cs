using UnityEngine;
using UnityEditor;

public class VariableCreateContextMenu {

	[MenuItem("Assets/Create/Variables/Integer")]
	static void CreateIntegerVariable(){ CreateVariable<IntegerVariable>("IntegerVariable"); }

	[MenuItem("Assets/Create/Variables/Float")]
	static void CreateFloatVariable(){ CreateVariable<FloatVariable>("FloatVariable"); }

	[MenuItem("Assets/Create/Variables/String")]
	static void CreateStringVariable(){ CreateVariable<StringVariable>("StringVariable"); }

	[MenuItem("Assets/Create/Variables/StringList")]
	static void CreateStringListVariable(){ CreateVariable<StringListVariable>("StringListVariable"); }

	private static void CreateVariable<T>(string placeholderFilename) where T : ScriptableObject {
		T asset = ScriptableObject.CreateInstance<T>();
		// setup asset if needed

		string path = AssetDatabase.GetAssetPath(Selection.activeObject);
		path += "/" + placeholderFilename + ".asset";
		ProjectWindowUtil.CreateAsset(asset, path);

	}

}
