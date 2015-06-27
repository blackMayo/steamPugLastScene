using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	
	private static PlayerHealth _instance;
	
	static int maxHealth;
	static int currentHealth;
	static int healthDecrease = 10;
	static int helmets;
	public static bool playerIsDead;	
	
	public static PlayerHealth Instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<PlayerHealth>();
				
				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(_instance.gameObject);
				
				SetMaxHealth (100);
				SetCurrentHealth (100);
				int life = HoldInformations.GetLife ();
				SetHelmets(life);
				playerIsDead = false;
			}
			
			return _instance;
		}
	}
	
	// +++++++++++++++ PUBLIC METHODS +++++++++++++++ 
	public static int MaxHealth {
		get {
			return maxHealth;
		}
	}
	
	public static int CurrentHealth {
		get {
			return currentHealth;
		}
	}

	public static int GetHelmets {
		get {
			return helmets;
		}
	}
	
	public static void UpdateCurrentHealthWhenHit() {
		currentHealth -= healthDecrease;
		
		if (currentHealth <= 0) {
			playerIsDead = true;
			
			// code for what comes after death
		}
	}
	
	public static bool IsDead {
		get {
			return playerIsDead;
		}
	}
	
	public static void setIsDead(bool isDead) {
		playerIsDead = isDead;
	}

	public static void SetHelmets (int life)
	{
		helmets = life;
	}
	
	// +++++++++++++++ PRIVATE METHODS +++++++++++++++
	// which will be primarily used inside this class
	static void SetMaxHealth(int maxHealthAmount) {
		maxHealth += maxHealthAmount;
	}
	
	
	static void SetCurrentHealth(int currentHealthAmount) {
		currentHealth += currentHealthAmount;
		
		if (currentHealth > maxHealth) {
			currentHealth = maxHealth;
		}
	}
}
