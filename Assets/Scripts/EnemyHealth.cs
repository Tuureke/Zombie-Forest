using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public const int maxHealth = 100;
	public int currentHealth = maxHealth;
	public int scoreValue = 100;
	public GameObject deadEnemyPrefab;

	public void TakeDamage(int amount)
	{
		currentHealth -= amount;
		if (currentHealth <= 0)
		{
			GameManager.score += scoreValue;
			Instantiate (deadEnemyPrefab, transform.position, transform.rotation);
			Destroy(gameObject);
		}

	}
}
