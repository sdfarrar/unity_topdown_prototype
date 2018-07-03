using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameEventListener))]
public abstract class AbstractCounter : MonoBehaviour {

	protected Text CountText;

	private void Awake () {
		CountText = GetComponentInChildren<Text>();
	}

	private void OnEnable(){
		UpdateText();
	}

	public void OnInventoryChanged(){
		UpdateText();
	}

	protected abstract void UpdateText();

}
