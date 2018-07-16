using UnityEngine;

public class RoopleDrop : AbstractItemDrop {

    public override void OnCollect(Inventory inventory) {
		Debug.Log("OnCollect");
		inventory.ApplyChangeToWallet(Drop.Quantity);
		Destroy(this.gameObject);
    }

}
