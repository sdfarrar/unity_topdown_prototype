using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BombBag : Weapon
{
	public GameObject bombPrefab;

    public override void PrimaryAttack(Vector2 direction) {
		Vector3 offset = direction;
		Instantiate(bombPrefab, transform.position+offset, Quaternion.identity);
    }

    public override void AlternateAttack(Vector2 direction) {
        PrimaryAttack(direction);
    }

    public override int GetIndex() {
		return 2;
    }

}
