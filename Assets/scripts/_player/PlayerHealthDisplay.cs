using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviour {

	//set the prefab of the Player here
	public GameObject playerPrefab;

	GameObject healthBarSlider;  //reference for slider
	public bool isPlayerGameOver = false; //flag to see if game is over - might not be needed

	PlayerHealth playerHealth;
	int helmets;

	void Start () {		
		// this is just used to instantiate the health of the player as soon as the player itself is instatiated
		playerHealth = PlayerHealth.Instance;
		helmets = PlayerHealth.GetHelmets;	
	}
	
	public void SliderController (Collider obj, Player player) {
		if (!isPlayerGameOver) {
			// if player is still alive
			if (!PlayerHealth.IsDead && PlayerHealth.CurrentHealth > 0) {	
				healthBarSlider = GameObject.FindGameObjectWithTag ("PlayerSlider");
				Slider slider = healthBarSlider.GetComponent<Slider> ();
				if (obj.gameObject.tag == "HairBall" && slider.value > 0) {
					BlinkPlayer (player); 
					slider.value -= 10f;  //reduce health
					PlayerHealth.UpdateCurrentHealthWhenHit ();
				}
			} else {
				//PlayerDropsDead();
				Destroy (this.gameObject);

				PlayerHealth.SetHelmets (helmets - 1);
				//instatiate a new Player Prefab
				if (PlayerHealth.GetHelmets >= 0) {
					GameObject playerPrefabInstance;
					playerPrefabInstance = (GameObject)Instantiate (playerPrefab, transform.position, transform.rotation);
					playerPrefabInstance.name = "Player";
				} else {
					isPlayerGameOver = true;
					// TODO play Animation that Player is dead and GAME OVER --> go to first scene
				}

			}
		}
	}
	
	void BlinkPlayer(Player obj)
	{
		Debug.Log ("Wanna BLINK?");
		StartCoroutine( Blinking (obj, .2f));
	}
	
	IEnumerator Blinking(Player obj, float seconds)
	{
		float duration = 8;
		while (duration > 0f) {
			obj.transform.GetComponent<Renderer>().enabled = !obj.transform.GetComponent<Renderer>().enabled;
			
			yield return new WaitForSeconds (seconds); 
			duration --;
		}
		obj.transform.GetComponent<Renderer> ().enabled = true;
	}


	GameObject helmetsText; //reference for text representing amount of helmets

	public void HelmetsTextController () {
		helmetsText = GameObject.FindGameObjectWithTag ("HelmetsUIText");
		Text text = helmetsText.GetComponent<Text> ();

		text.text = helmets.ToString();
	}
}
