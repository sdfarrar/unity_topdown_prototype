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
		if(elapsedTime>explosionDelay*.66f){
			anim.speed = 1.5f;
		}else if(elapsedTime>explosionDelay*.33f){
			anim.speed = 1.25f;
		}
		if(elapsedTime>=explosionDelay){
			anim.speed = 1;
			Explode();
		}
	}

	private void Explode(){
		anim.SetTrigger("Explode");
		Destroy(this.gameObject, 2f);
	}

}
