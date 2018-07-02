using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneResetter : MonoBehaviour {

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			string currentSceneName = SceneManager.GetSceneAt(0).name;
			SceneManager.LoadScene(currentSceneName);
		}
	}

}
