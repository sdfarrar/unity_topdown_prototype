using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Bush : MonoBehaviour, IDamageable, ILiftable {
public class Bush : LiftableObject, IDamageable {

	public int Health = 1;

    public void TakeDamage(DamageDealer damageDealer) {
		Debug.Log("taking damage from " + damageDealer.gameObject.name);
		Health -= damageDealer.DamageAmount.Value;

		if(Health<=0){
			Destroy(this.gameObject);
		}
    }

    protected override bool CanPickUp(PlayerController player) { return true; }
}
