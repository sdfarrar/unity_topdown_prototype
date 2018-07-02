using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsUI : MonoBehaviour {

	public RectTransform BombsTransform;

	public InventoryItem Bomb;

	private Text BombCountText;

	private void Awake () {
		if(BombsTransform!=null){
			BombCountText = BombsTransform.GetComponentInChildren<Text>();
		}
	}

	private void OnEnable(){
		UpdateText();
	}

	public void OnInventoryChanged(){
		UpdateText();
	}

	private void UpdateText(){
		BombCountText.text = Bomb.Quantity < 10 ? "0"+Bomb.Quantity : ""+Bomb.Quantity;
	}

}
