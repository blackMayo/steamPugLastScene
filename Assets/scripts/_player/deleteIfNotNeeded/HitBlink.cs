using UnityEngine;
using System.Collections;

public class HitBlink : MonoBehaviour {
	
	float duration = 2f;
	float blinkTime = 0.23f;
	
	public void StartBlinking(GameObject obj) {
		StartCoroutine(DoBlinks(obj));
	}
	
	public IEnumerator DoBlinks(GameObject obj) {
		while (duration > 0f) {
			duration -= Time.deltaTime;
			
			//toggle renderer
			obj.transform.GetComponent<Renderer>().enabled = !obj.transform.GetComponent<Renderer>().enabled;
			
			//wait for a bit
			yield return new WaitForSeconds(blinkTime);
		}
		Debug.Log ("Done??");
		//make sure renderer is enabled when we exit
		obj.transform.GetComponent<Renderer>().enabled = true;
	}
}