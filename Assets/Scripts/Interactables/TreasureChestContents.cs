using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Drops/Chest Contents")]
public class TreasureChestContents : ScriptableObject {

	public ItemDropTemplate Drop;
	public int InitialCount;
	public bool IsKeyItem;
	public string Text;

}
