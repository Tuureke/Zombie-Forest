using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public const int maxHealth = 100;
	public int currentHealth = maxHealth;
	public Slider healthSlider;
	public GameObject deadPlayerPrefab;

	PlayerController playerController;                             
	PlayerShoot playerShoot;
	Animator anim;
	AudioSource playerAudio;
	AudioSource heartBeat;
	AudioSource[] AmbientSounds;
	GameManager gameManager;

	public AudioClip pain1Clip;
	public AudioClip pain2Clip;
	public AudioClip pain3Clip;
	public AudioClip pain4Clip;
	public AudioClip pain5Clip;
	public AudioClip pain6Clip;
	public AudioClip pain7Clip;
	public AudioClip pain8Clip;
	AudioClip[] painClips;

	int painClipsIndex = 0;

	public AudioClip deathClip;
	public AudioClip heartBeatSlow;
	public AudioClip heartBeatFast;




	void Awake ()
	{
		gameManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();
		AmbientSounds = GameObject.FindGameObjectWithTag ("GameController").GetComponents<AudioSource> ();
		anim = GetComponent<Animator> ();
		playerAudio = GetComponent <AudioSource> ();
		heartBeat = AmbientSounds [1];
		playerController = GetComponent <PlayerController> ();
		playerShoot = GetComponentInChildren <PlayerShoot> ();
		painClips = new AudioClip[8];
		painClips.SetValue (pain1Clip, 0);
		painClips.SetValue (pain2Clip, 1);
		painClips.SetValue (pain3Clip, 2);
		painClips.SetValue (pain4Clip, 3);
		painClips.SetValue (pain5Clip, 4);
		painClips.SetValue (pain6Clip, 5);
		painClips.SetValue (pain7Clip, 6);
		painClips.SetValue (pain8Clip, 7);
	}

	public void TakeDamage(int amount)
	{
		playerAudio.clip = painClips [painClipsIndex];
		playerAudio.Play ();
		painClipsIndex += 1;
		if (painClipsIndex >= 8) {
			painClipsIndex = 0;
		}
			
		currentHealth -= amount;
		healthSlider.value = currentHealth;

		if (currentHealth > 20) {
			heartBeat.Pause ();
		}


		if ((currentHealth <= 20) && (currentHealth > 10)){
			heartBeat.clip = heartBeatSlow;
			heartBeat.Play ();
		}
			
		if ((currentHealth <= 10) && (currentHealth > 0))
		{
			heartBeat.clip = heartBeatFast;
			heartBeat.Play ();
		}

		if (currentHealth <= 0)
		{
			Die ();
		}

	}

	void Die()
	{
		anim.SetBool ("IsDead", true);
		playerController.enabled = false;
		playerShoot.enabled = false;
		playerAudio.clip = deathClip;
		playerAudio.Play ();
		heartBeat.enabled = false;
		GameObject flashlight = GameObject.FindGameObjectWithTag ("Light");
		GameObject meshrenderer = GameObject.FindGameObjectWithTag("Renderer");
		flashlight.SetActive (false);
		meshrenderer.SetActive (false);
		Instantiate (deadPlayerPrefab, transform.position, transform.rotation);
		gameManager.GameOver ();
	}
		
}

	
		
