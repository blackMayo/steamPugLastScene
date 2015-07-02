using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class EvilCat : MonoBehaviour
{
	EvilHealth evilHealth;
	EvilBasicFire fire;
	MoveCat move;

	float timer = 0.0f;
	float timerMax = 3.0f;
	bool enemyHasMoved = false;

	protected void Start() {
		// this is just used to instantiate the health of the evil cat (class with static methods) as soon as the evil cat itself is instatiated
		this.gameObject.AddComponent<EvilHealthDisplay>();
		evilHealth = EvilHealth.Instance;

		this.gameObject.AddComponent<EvilBasicFire>();
		fire = this.GetComponent<EvilBasicFire>();
		fire.ShootAtPlayer ();

		this.gameObject.AddComponent<MoveCat>();
		move = this.GetComponent<MoveCat>();
		move.MoveCatToNewPosition ();
	}
	
	protected void Update() {
		timer += Time.deltaTime;
		if (timer >= timerMax) {
			fire.ShootAtPlayer ();
			timer = 0.0f;
		}
		if (timer == 0.0f) {
			move.MoveCatToNewPosition ();
		}
	}

	// cat was hit
	void OnTriggerEnter(Collider obj) {
		EvilHealthDisplay healthDisplay = this.GetComponent<EvilHealthDisplay>();
		healthDisplay.SliderController (obj, this);
	}
}