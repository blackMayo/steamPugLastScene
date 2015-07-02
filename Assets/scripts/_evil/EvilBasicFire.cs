using UnityEngine;
using System.Collections;

public class EvilBasicFire : MonoBehaviour {

	GameObject player;

	public GameObject hairballPrefab;

	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShootAtPlayer() {
		StartCoroutine(Shoot());
	}

	IEnumerator Shoot ()
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
		
		
		Debug.Log ("Wait 0.5 secs before moving on");
		yield return new WaitForSeconds(0.5f);
	}
}
