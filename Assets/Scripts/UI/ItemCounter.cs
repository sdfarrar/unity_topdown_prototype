using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCounter : AbstractCounter {

	public IntegerVariable Count;

    protected override void UpdateText() {
		CountText.text = Count.Value < 10 ? "0"+Count.Value : ""+Count.Value;
    }
}
