using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TestEvilCat : MonoBehaviour
{
	
	Vector3 startPos;
	Vector3 endPos;
	EvilHealth evilHealth;
	
	protected void Start() {
		startPos = transform.position;
		endPos = transform.position;
		evilHealth = EvilHealth.Instance;
	}
	
	protected void Update() {
		
	}
	
	GameObject healthBarSlider;  //reference for slider
	public bool isGameOver = false; //flag to see if game is over
	
	// cat was hit
	void OnTriggerEnter(Collider obj) {
	
		// if cat is still alive
		if (!EvilHealth.IsDead && EvilHealth.CurrentHealth > 0 ) {
			BlinkEnemy(this); 

			healthBarSlider = GameObject.FindGameObjectWithTag("EvilSlider");
			Slider slider = healthBarSlider.GetComponent<Slider> ();
			if(obj.gameObject.tag == "Projectile" && slider.value > 0){
				slider.value -= 10f;  //reduce health
				EvilHealth.UpdateCurrentHealthWhenHit();
			}
		} else {
			isGameOver = true;
			Destroy(this.gameObject);

			//EnemyDropsDead();
		}
	}

	void BlinkEnemy(TestEvilCat obj)
	{
		StartCoroutine( Blink (obj, .2f));
	}
	
	IEnumerator Blink(TestEvilCat obj, float seconds)
	{
		float duration = 8;
		while (duration > 0f) {
			obj.transform.GetComponent<Renderer>().enabled = !obj.transform.GetComponent<Renderer>().enabled;

			yield return new WaitForSeconds (seconds); 
			duration --;
		}
		obj.transform.GetComponent<Renderer> ().enabled = true;
	}
}