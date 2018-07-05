using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class MagicMeter : MonoBehaviour {

	public Image SolidImage;
	public Image TopImage;

	private float maxPositionY = 67f;
	private float minPositionY = -65f;
	private float distanceBetweenPositions; 

	void Start () {
		distanceBetweenPositions = Mathf.Abs(maxPositionY) + Mathf.Abs(minPositionY);
	}
	
	//TODO Move to eventlistener callback
	void Update () {
		if(SolidImage==null || TopImage==null){ return; }
		//TODO set SolidImage.fillAmount == magic percentage
		float fillAmount = SolidImage.fillAmount;
		distanceBetweenPositions = Mathf.Abs(maxPositionY) + Mathf.Abs(minPositionY); //TODO remove
		float displacement = minPositionY + (distanceBetweenPositions*fillAmount);
		Vector3 topPosition = TopImage.transform.localPosition;
		TopImage.transform.localPosition = new Vector3(topPosition.x, displacement, topPosition.z);
	}
}
