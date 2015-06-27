using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EvilHealthDisplay : MonoBehaviour {

	GameObject healthBarSlider;  //reference for slider
	public bool isEvilGameOver = false; //flag to see if game is over - might not be needed

	public void SliderController (Collider obj, EvilCat evilCat) {
		// if cat is still alive
		if (!EvilHealth.IsDead && EvilHealth.CurrentHealth > 0) {			
			healthBarSlider = GameObject.FindGameObjectWithTag("EvilSlider");
			Slider slider = healthBarSlider.GetComponent<Slider> ();
			if(obj.gameObject.tag == "Projectile" && slider.value > 0){
				BlinkEvil(evilCat); 
				slider.value -= 10f;  //reduce health
				EvilHealth.UpdateCurrentHealthWhenHit();
			}
		} else {
			isEvilGameOver = true;
			Destroy(this.gameObject);
			
			//EnemyDropsDead();
		}
	}

	void BlinkEvil(EvilCat obj)
	{
		Debug.Log ("Wanna BLINK?");
		StartCoroutine( Blinking (obj, .2f));
	}
	
	IEnumerator Blinking(EvilCat obj, float seconds)
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
