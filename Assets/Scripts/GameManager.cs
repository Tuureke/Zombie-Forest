using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour {

	public static int score;
	public Text scoreText;
	public static int numberOfGrenades;
	public Text numberOfGrenadesText;
	GameObject player;
	PlayerHealth playerHealth;
	public GameObject enemy;                // The enemy prefab to be spawned.
	public float spawnTime = 2f;            // How long between each spawn.
	public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from.
	public GameObject eliteEnemy;
	public float eliteSpawnTime = 20f;
	public GameObject mutantEnemy;
	public float mutantSpawnTime = 10f;
	public GameObject grenadePickUp;
	public float pickUpSpawnTime = 10f; 
	public AudioMixerSnapshot paused;
	public AudioMixerSnapshot unpaused;
	GameObject canvas;
	GameObject gameOverText;
	Text gameOverScoreText;

	void Awake ()
	{
		gameOverText = GameObject.Find ("Game Over Text");
		gameOverScoreText = GameObject.Find ("Game Over Score Text").GetComponent<Text>();
		gameOverText.SetActive (false);
		canvas = GameObject.Find ("Pause Canvas");
		canvas.SetActive(false);
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
		score = 0;
		numberOfGrenades = 3;
		InvokeRepeating ("SpawnEnemies", spawnTime, spawnTime);
		InvokeRepeating ("SpawnMutantEnemies", mutantSpawnTime, mutantSpawnTime);
		InvokeRepeating ("SpawnEliteEnemies", eliteSpawnTime, eliteSpawnTime);
		InvokeRepeating ("SpawnPickUps", pickUpSpawnTime, pickUpSpawnTime);
	}
	
	void Update ()
	{
		scoreText.text = "Score: " + score;
		numberOfGrenadesText.text = "Grenades: " + numberOfGrenades;
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Pause();
		}
	}

	void SpawnEnemies ()
	{
		// If the player has no health left, exit the function.
		if(playerHealth.currentHealth <= 0f)
		{
			return;
		}

		// Find a random index between zero and one less than the number of spawn points.
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	}

	void SpawnEliteEnemies ()
	{
		// If the player has no health left, exit the function.
		if(playerHealth.currentHealth <= 0f)
		{
			return;
		}

		// Find a random index between zero and one less than the number of spawn points.
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		Instantiate (eliteEnemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	}

	void SpawnMutantEnemies ()
	{
		// If the player has no health left, exit the function.
		if(playerHealth.currentHealth <= 0f)
		{
			return;
		}

		// Find a random index between zero and one less than the number of spawn points.
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		Instantiate (mutantEnemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	}

	void SpawnPickUps ()
	{
		// If the player has no health left, exit the function.
		if(playerHealth.currentHealth <= 0f)
		{
			return;
		}

		// Find a random index between zero and one less than the number of spawn points.
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		Instantiate (grenadePickUp, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	}

	public void Pause()
	{
		canvas.SetActive (!canvas.activeSelf);
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		Lowpass ();

	}

	void Lowpass()
	{
		if (Time.timeScale == 0)
		{
			paused.TransitionTo(.01f);
		}

		else

		{
			unpaused.TransitionTo(.01f);
		}
	}

	public void Quit()
	{
		#if UNITY_EDITOR 
		EditorApplication.isPlaying = false;
		#else 
		Application.Quit();
		#endif
	}

	public void GameOver()
	{
		gameOverText.SetActive (true);
		gameOverScoreText.text = "YOUR SCORE WAS: " + score;
		scoreText.enabled = false;
	}

	public void Replay()
	{
		SceneManager.LoadScene ("Game");
	}

	public void MainMenu()
	{
		SceneManager.LoadScene ("Main Menu");
	}
}
