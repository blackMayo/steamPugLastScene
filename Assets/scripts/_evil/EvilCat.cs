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

	GameObject player;

	public GameObject hairballPrefab;
	
	protected void Start() {
		this.gameObject.AddComponent<EvilHealthDisplay>();

		AddVectorsToList ();
		CalculateNewPosition ();

		// this is just used to instantiate the health of the evil cat as soon as the evil cat itself is instatiated
		evilHealth = EvilHealth.Instance;

		StartCoroutine(ShootAtPlayer());
		StartCoroutine (MoveCat ());
	}
	
	protected void Update() {


	}

	IEnumerator ShootAtPlayer ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		Debug.Log ("SHOOT!");

		transform.LookAt(player.transform);

		GameObject hairballInstance;
		hairballInstance = (GameObject)Instantiate(hairballPrefab, transform.position, transform.rotation);
		hairballInstance.name = "HairBall";

		Rigidbody hairballRbInstance;
		hairballRbInstance = hairballInstance.GetComponent<Rigidbody>();
		const int SHOOTING_FORCE = 1500;
		hairballRbInstance.AddForce(transform.forward * SHOOTING_FORCE);


		Debug.Log ("Wait 5 secs before moving on");
		yield return new WaitForSeconds(5.0f);
	}
	
	IEnumerator MoveCat()
	{
		MoveCatToNewPosition ();
		Debug.Log ("MOVED! Wait 5 secs before shooting");
		yield return new WaitForSeconds(5.0f);
	}

	void MoveCatToNewPosition ()
	{
		CalculateNewPosition ();
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
		Debug.Log ("StartPosition = " + startPos + " Endposition = " + endPos);
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
		var ladder = GameObject.Find ("Quad_Ladder").transform.position;
		positionsList.Add (new Vector3(ladder.x, ladder.y + 1.17f, ladder.z)); // Ladder

		var hay = GameObject.Find ("Hay_B").transform.position;
		positionsList.Add (new Vector3(hay.x, hay.y + 1.17f, hay.z)); //Hay

		var table = GameObject.Find ("Plane_Table").transform.position;
		positionsList.Add (new Vector3(table.x, table.y + 1.17f, table.z)); // Table

		var barrel = GameObject.Find ("Plane_Barrel").transform.position;
		positionsList.Add (new Vector3(barrel.x, barrel.y + 1.17f, barrel.z)); // Barrel

		var floorCage = GameObject.Find ("Quad_FloorCage").transform.position;
		positionsList.Add (new Vector3(floorCage.x, floorCage.y + 1.17f, floorCage.z)); // FloorCage

		var pillarD1 = GameObject.Find ("Quad_PillarD1").transform.position;
		positionsList.Add (new Vector3(pillarD1.x, pillarD1.y + 1.17f, pillarD1.z)); // Pillar Back-Front

		var pillarD2 = GameObject.Find ("Quad_PillarD2").transform.position;
		positionsList.Add (new Vector3(pillarD2.x, pillarD2.y + 1.17f, pillarD2.z)); // Pillar Back-Back

		var pillarD = GameObject.Find ("Quad_PillarD").transform.position;
		positionsList.Add (new Vector3(pillarD.x, pillarD.y + 1.17f, pillarD.z)); // Pillar Front-Back
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