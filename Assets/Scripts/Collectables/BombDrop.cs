using UnityEngine;

public class BombDrop : AbstractItemDrop {

    public override void OnCollect(Inventory inventory) {
		inventory.ApplyBombCountChange(Drop.Quantity);
		Destroy(this.gameObject);
    }

}
