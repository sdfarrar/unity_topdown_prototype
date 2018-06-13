using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : LiftableObject {

	public ScriptableObject RequiredItem;

    protected override bool CanPickUp(PlayerController player) {
		if(RequiredItem==null/* || player.HasItem(RequiredItem)*/){ return false; } //TODO uncomment when we have some sort of inventory working
		return true;
    }

}
