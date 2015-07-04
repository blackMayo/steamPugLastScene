using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class EvilCat : MonoBehaviour
{
	EvilHealthDisplay healthDisplay;
	EvilHealth evilHealth;
	EvilBasicFire fire;
	MoveCat move;

	float shootTimer = 0.0f;
	float shootTimerMax = 3.0f;
	float moveTimer = 0.0f;
	float moveTimerMax = 3.0f;

	protected void Start() {
		// this is just used to instantiate the health of the evil cat (class with static methods) as soon as the evil cat itself is instatiated
		evilHealth = EvilHealth.Instance;

		this.gameObject.AddComponent<EvilHealthDisplay>();
		healthDisplay = this.GetComponent<EvilHealthDisplay>();

		this.gameObject.AddComponent<EvilBasicFire>();
		fire = this.GetComponent<EvilBasicFire>();
		//fire.ShootAtPlayer ();

		this.gameObject.AddComponent<MoveCat>();
		move = this.GetComponent<MoveCat>();
	}
	
	protected void Update() {

		//find out how long it takes for cat to lerp
		//add it to the shoot seks
		//make nested ifs decreasing the max values or sth like this

		/*
		shootTimer += DeltaTime ();
		if (shootTimer >= shootTimerMax) {
			fire.ShootAtPlayer ();
			shootTimer = 0.0f;
		}
		if (shootTimer == 0.0f) {
			move.MoveCatToNewPosition ();
		}*/

		if (EvilHealth.CurrentHealth < 5) {
			Debug.Log("I am Dead");
			Destroy (this.gameObject);
		}

	}

	static float DeltaTime ()
	{
		return Time.deltaTime;
	}

	// cat was hit
	void OnTriggerEnter(Collider obj) {
		if (EvilHealth.CurrentHealth > 0) {
			healthDisplay.UpdateSliderController (obj, this);
		}
	}
}