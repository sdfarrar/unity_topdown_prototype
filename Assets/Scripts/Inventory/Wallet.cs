using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Inventory/Wallet")]
public class Wallet : ScriptableObject {

	public int CurrentAmount;
	public IntegerReference MaxAmount;

	public void ApplyChange(int delta){
		int value = Mathf.Clamp(CurrentAmount+delta, 0, MaxAmount.Value);
		CurrentAmount = value;
	}

}
