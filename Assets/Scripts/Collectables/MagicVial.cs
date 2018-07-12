using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
[ExecuteInEditMode]
public class MagicVial : MonoBehaviour, ICollectable {

	public MagicDrop Template;
	public IntegerVariable MagicAmount;
	public IntegerReference MaxMagicAmount;

	public UnityEvent OnCollectEvent;

	private void Start(){
		if(Template!=null){
			GetComponent<SpriteRenderer>().sprite = Template.Sprite;
		}
	}

    public void OnCollect(InventoryV2 inventory) {
		int amount = Mathf.Clamp(MagicAmount.Value + Template.Quantity.Value, 0, MaxMagicAmount.Value);
		MagicAmount.SetValue(amount);
		OnCollectEvent.Invoke();
		Destroy(this.gameObject);
    }
}
