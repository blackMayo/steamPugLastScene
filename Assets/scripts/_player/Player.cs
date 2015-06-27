using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	//DO WHATEVER IS NEEDED HERE FOR THE PLAYER TO JUMP ON PLATFORMS OR WHATEVER

	PlayerHealthDisplay healthDisplay;
	
	// Use this for initialization
	void Start () {
		this.gameObject.AddComponent<PlayerHealthDisplay>();
		healthDisplay = this.GetComponent<PlayerHealthDisplay>();

	}
	
	// Update is called once per frame
	void Update () {
		healthDisplay.HelmetsTextController ();	
	}

	// player was hit
	void OnTriggerEnter(Collider obj) {
		healthDisplay.SliderController (obj, this);
	}
}
