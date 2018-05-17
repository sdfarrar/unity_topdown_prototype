using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {


	public float explosionDelay = 1f;
	private float elapsedTime;

	private Animator anim;

	private void Start() {
		anim = GetComponent<Animator>();
	}

	private void Update() {
		elapsedTime+=Time.deltaTime;
		if(elapsedTime>=explosionDelay){
			Explode();
		}
	}

	private void Explode(){
		anim.SetTrigger("Explode");
		Destroy(this.gameObject, 2f);
	}

}
