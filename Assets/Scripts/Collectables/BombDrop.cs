﻿using UnityEngine;

public class BombDrop : AbstractItemDrop {

    public override void OnCollect(InventoryV2 inventory) {
		inventory.ApplyBombCountChange(Drop.Quantity);
		Destroy(this.gameObject);
    }

}
