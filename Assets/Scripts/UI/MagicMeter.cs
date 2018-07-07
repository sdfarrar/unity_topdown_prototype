using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class MagicMeter : MonoBehaviour {

	public Image SolidImage;
	public Image TopImage;

	public IntegerVariable MagicAmount;
	public IntegerVariable MagicAmountMax;

	private float maxPositionY = 67f;
	private float minPositionY = -65f;

	private void Start(){
		UpdateMeter();
	}

	public void OnMagicAmountChanged(){
		UpdateMeter();
	}

	private void UpdateMeter(){
		if(SolidImage==null || TopImage==null){ Debug.LogWarning("Image reference(s) == null!"); return; }
		float percentage = SolidImage.fillAmount = (float)MagicAmount.Value / (float)MagicAmountMax.Value;

		float displacement = Mathf.Lerp(minPositionY, maxPositionY, percentage);
		Vector3 topPosition = TopImage.transform.localPosition;
		TopImage.transform.localPosition = new Vector3(topPosition.x, displacement, topPosition.z);
	}
}
