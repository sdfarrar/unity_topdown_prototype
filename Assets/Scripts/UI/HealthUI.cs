﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

	public Sprite fullHeart;
	public Sprite halfHeart;
	public Sprite emptyHeart;

	public IntegerVariable HP;

	private List<Image> hearts;

	private void Start(){
		Image[] images = GetComponentsInChildren<Image>();
		hearts = new List<Image>(images);
	}

	public void OnHealthDamaged(){
		Debug.Log("OnHeathDamaged:" + HP.Value);
		int startIndex = HP.Value / 10;
		for(int i=startIndex; i<hearts.Count; ++i){
			if(i==startIndex){
				if(HP.Value%10 == 0){ 
					hearts[i].sprite = emptyHeart;
				}else{
					hearts[i].sprite = halfHeart;
				}
			}else{
				hearts[i].sprite = emptyHeart;
			}
		}

	}

	public void OnHealthHealed(){
		int index = (int)Mathf.Ceil(HP.Value / 10f);
		for(int i=0; i<index && i<hearts.Count; ++i){
			if(i!=index-1){
				hearts[i].sprite = fullHeart;
			}else{
				// last heart
				if(HP.Value%10==0){
					hearts[i].sprite = fullHeart;
				}else{
					hearts[i].sprite = halfHeart;
				}
			}
		}

	}

}
