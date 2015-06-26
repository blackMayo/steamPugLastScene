using UnityEngine;
using System.Collections;

public class EvilBasicFire : MonoBehaviour {
	
	public GameObject projectile;
	public Transform target;


	public float turretSpeed;
	public float fireRate;
	public float fireBallHeight;
	public GameObject fireBall;
	public float range;
	float distance;
	
	// Use this for initialization
	void Start () {
		
	}


	// cat fires a new projectile on each position
	void Update () {
		
		if (Input.GetButtonDown ("Fire2")) {

			//TODO
			GameObject projectileInstance;
			projectileInstance = (GameObject)Instantiate (projectile, transform.position, transform.rotation);
			projectileInstance.name = "HairBall";
			
			Rigidbody projectileRbInstance;
			projectileRbInstance = projectileInstance.GetComponent<Rigidbody> ();

			//	projectileRbInstance.velocity = transform.TransformDirection(20, 0, 0);

			Vector3 relativePos = target.position - transform.position;
			Quaternion rotation = Quaternion.LookRotation (relativePos); 
			rotation.x = 0;
			rotation.z = 0;
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * turretSpeed);
			
			//Fire at player when in range.
			distance = Vector3.Distance (transform.position, target.position);
			
			//if (distance < range) { TODO fire if cat on new position
				LaunchFireBall ();
			//}    
		}
	}
		
	void LaunchFireBall()
	{
		Vector3 position = new Vector3(transform.position.x, transform.position.y + fireBallHeight, transform.position.z);
		Instantiate(fireBall, position, transform.rotation);
	}
		
}
