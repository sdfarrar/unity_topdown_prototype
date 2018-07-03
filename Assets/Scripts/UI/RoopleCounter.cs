using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoopleCounter : AbstractCounter {

	public Wallet Wallet;

    protected override void UpdateText() {
		CountText.text = Wallet.CurrentAmount < 10 ? "0"+Wallet.CurrentAmount : ""+Wallet.CurrentAmount;
    }
}
