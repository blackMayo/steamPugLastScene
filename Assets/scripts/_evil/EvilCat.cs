using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class EvilCat : MonoBehaviour
{
	float lerpTime = 1f;
	float currentLerpTime;
	List<Vector3> positionsList = new List<Vector3> ();
	
	Vector3 newPositionFromList;
	
	Vector3 startPos;
	Vector3 endPos;

	EvilHealth evilHealth;
	
	protected void Start() {
		AddVectorsToList ();
		startPos = transform.position;
		endPos = CalculatePositionFromList();
		evilHealth = EvilHealth.Instance;

	}
	
	protected void Update() {
		//reset when we press spacebar
		// after shooting - insert FireHairBall in here!
		if (Input.GetKeyDown(KeyCode.Space)) {
			CalculateNewPosition ();
		}
		
		IncrementTimer ();
		
		//lerp!
		float perc = currentLerpTime / lerpTime;
		
		var newPosition = Vector3.Lerp (startPos, endPos, perc);
		transform.position = newPosition;
		
	}
	
	void CalculateNewPosition ()
	{
		currentLerpTime = 0f;
		startPos = transform.position;
		float ypsilon = startPos.y;
		newPositionFromList = CalculatePositionFromList ();
		if (newPositionFromList.y == ypsilon) {
			endPos = startPos;
		}
		else {
			endPos = newPositionFromList;
		}	
	}
	
	void IncrementTimer ()
	{
		//increment timer once per frame
		currentLerpTime += Time.deltaTime;
		if (currentLerpTime > lerpTime) {
			currentLerpTime = lerpTime;
		}
	}
	
	void AddVectorsToList ()
	{
		positionsList.Add (new Vector3(0.0f, 0.0f, 0.0f));
		positionsList.Add (new Vector3(3.0f, 4.0f, 0.0f));
		positionsList.Add (new Vector3(-1.0f, 6.0f, 0.0f));
		positionsList.Add (new Vector3(0.0f, 9.0f, 0.0f));
		positionsList.Add (new Vector3(1.0f, 14.0f, 0.0f));
		positionsList.Add (new Vector3(-2.0f, 18.0f, 0.0f));
		positionsList.Add (new Vector3(1.0f, 20.0f, 0.0f));
		positionsList.Add (new Vector3(-3.0f, 23.0f, 0.0f));
	}
	
	Vector3 CalculatePositionFromList () {
		var rand = new System.Random();
		int index = rand.Next(positionsList.Count);
		Vector3 pos = positionsList [index];
		return pos;
	}

	GameObject healthBarSlider;  //reference for slider
	public bool isGameOver = false; //flag to see if game is over
	
	// cat was hit
	void OnTriggerEnter(Collider obj) {
		
		// if cat is still alive
		if (EvilHealth.CurrentHealth > 0 && !EvilHealth.IsDead) {
			BlinkEvilHit(this); 
			
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
	
	void BlinkEvilHit(EvilCat obj)
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