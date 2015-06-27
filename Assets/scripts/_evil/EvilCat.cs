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
		this.gameObject.AddComponent<EvilHealthDisplay>();

		AddVectorsToList ();
		CalculateNewPosition ();
		Debug.Log ("StartPosition = " + startPos + " Endposition = " + endPos);

		// this is just used to instantiate the health of the evil cat as soon as the evil cat itself is instatiated
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
		//float ypsilon = startPos.y;
		newPositionFromList = CalculatePositionFromList ();
//		if (newPositionFromList.y == ypsilon) {
//			endPos = startPos;
//		}
		if (newPositionFromList == startPos) {
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
		positionsList.Add (new Vector3(10.5f, 1.7f, 0.4f)); //Hay
		positionsList.Add (new Vector3(11f, 5.2f, -0.9f)); // Table
		positionsList.Add (new Vector3(11.76f, 6.27f, 0.95f)); // Barrel
		positionsList.Add (new Vector3(12f, 9.4f, -0.315f)); // Ladder
		positionsList.Add (new Vector3(12.3f, 12.3f, -1.3544f)); // FloorCage
		positionsList.Add (new Vector3(12.4f, 13.45f, 1.904f)); // Pillar Back-Front
		positionsList.Add (new Vector3(13f, 18.15f, 1.53f)); // Pillar Back-Back
		positionsList.Add (new Vector3(12.4f, 25.2f, -2.5f)); // Pillar Front-Back
	}
	
	Vector3 CalculatePositionFromList () {
		var rand = new System.Random();
		int index = rand.Next(positionsList.Count);
		Vector3 pos = positionsList [index];
		return pos;
	}

	// cat was hit
	void OnTriggerEnter(Collider obj) {
		EvilHealthDisplay healthDisplay = this.GetComponent<EvilHealthDisplay>();
		healthDisplay.SliderController (obj, this);
	}
}