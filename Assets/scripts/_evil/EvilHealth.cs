using UnityEngine;
using System.Collections;

public class EvilHealth : MonoBehaviour{
	
	private static EvilHealth _instance;
	
	static int maxHealth;
	static int currentHealth;
	static int healthDecrease = 10;
	public static bool isDead;	
	
	public static EvilHealth Instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<EvilHealth>();
				
				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(_instance.gameObject);
				
				SetMaxHealth (100);
				SetCurrentHealth (100);
				isDead = false;
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
	
	public static void UpdateCurrentHealthWhenHit() {
		currentHealth -= healthDecrease;
		
		if (currentHealth < 0) {
			isDead = true;
			
			// code for what comes after death
		}
	}
	
	public static bool IsDead {
		get {
			return isDead;
		}
	}
	
	public static void setIsDead(bool isDead) {
		isDead = isDead;
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
