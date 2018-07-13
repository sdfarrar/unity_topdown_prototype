using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public abstract class AbstractItemDrop : MonoBehaviour, ICollectable {

	[SerializeField]
	public ItemDropTemplate Drop;

#if UNITY_EDITOR
	public bool AutoUpdateSprite;
#endif

	public abstract void OnCollect(InventoryV2 inventory);

}
