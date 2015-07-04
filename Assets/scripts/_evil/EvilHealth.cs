using UnityEngine;
using System.Collections;

public class EvilHealth : MonoBehaviour{
	
	private static EvilHealth _instance;
	
	static int maxHealth;
	static int currentHealth;
	static int healthDecrease = 10;

	public static EvilHealth Instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<EvilHealth>();
				
				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(_instance.gameObject);

				//let EvilCat live a little longer than the pug
				SetMaxHealth (145);
				SetCurrentHealth (145);
			}
			
			return _instance;
		}
	}
	
	// +++++++++++++++ PUBLIC METHODS +++++++++++++++ 
	
	public static int CurrentHealth {
		get {
			LogCurrentHealth ();
			return currentHealth;
		}
		set {
			currentHealth -= value;
		}
	}

	// +++++++++++++++ PRIVATE METHODS +++++++++++++++
	// which will be primarily used inside this class
	static void SetMaxHealth(int maxHealthAmount) {
		maxHealth += maxHealthAmount;

		Debug.Log ("MaxHealth = " + maxHealth);
	}
	
	static void SetCurrentHealth(int currentHealthAmount) {
		currentHealth += currentHealthAmount;
		
		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}
		LogCurrentHealth ();
	}

	private static void LogCurrentHealth ()
	{
		Debug.Log ("CurrentHealth = " + currentHealth);
	}
}
