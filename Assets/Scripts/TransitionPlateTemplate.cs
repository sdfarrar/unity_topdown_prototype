using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Transition Plate", menuName="Transition Plate")]
public class TransitionPlateTemplate : ScriptableObject {

	public Color SpriteColor;
	public Texture2D TransitionTexture;
#if UNITY_EDITOR
	public string DebugText;
#endif

}
