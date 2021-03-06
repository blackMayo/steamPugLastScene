﻿using UnityEngine;
using System.Collections;

public class BasicFire : MonoBehaviour {

	//set the prefab of the Projectile here
	public GameObject projectilePrefab;

	GameObject evilCat;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		evilCat = GameObject.FindGameObjectWithTag ("Evil");
		transform.LookAt(evilCat.transform);

		if (Input.GetButtonDown ("Fire1")) { //press left MouseButton to Shoot
			
			GameObject projectileInstance;
			projectileInstance = (GameObject)Instantiate(projectilePrefab, transform.position, transform.rotation);
			projectileInstance.name = "Projectile";
			
			Rigidbody projectileRbInstance;
			projectileRbInstance = projectileInstance.GetComponent<Rigidbody>();
			const int SHOOTING_FORCE = 2000;
			projectileRbInstance.AddForce(transform.forward * SHOOTING_FORCE);
		}
	
	}
}
