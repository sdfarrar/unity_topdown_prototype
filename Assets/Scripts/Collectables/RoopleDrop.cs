using UnityEngine;

public class RoopleDrop : AbstractItemDrop {

    public override void OnCollect(InventoryV2 inventory) {
		Debug.Log("OnCollect");
		inventory.ApplyChangeToWallet(Drop.Quantity);
		Destroy(this.gameObject);
    }

}
