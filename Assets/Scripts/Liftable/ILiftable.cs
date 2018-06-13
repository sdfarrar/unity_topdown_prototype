using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILiftable {
	bool OnPickedUp(PlayerController player);
	void OnThrow(Vector3 target);
}
