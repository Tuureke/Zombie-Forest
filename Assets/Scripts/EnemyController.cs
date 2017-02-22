using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

		public float timeBetweenAttacks = 1f;     // The time in seconds between each attack.
		public int attackDamage = 10;               // The amount of health taken away per attack.
		bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
		float timer;                                // Timer for counting up to the next attack.
		public AudioClip zombieEatAudioClip;
		public AudioClip zombieAttackAudioClip1;
		public AudioClip zombieAttackAudioClip2;
		public AudioClip zombieAttackAudioClip3;
		AudioClip[] attackClips;
		int attackClipsIndex = 0;
		public GameObject bloodPrefab;
		
		Transform player;               
		GameObject playerGameObject; 	
		PlayerHealth playerHealth;      
		EnemyHealth enemyHealth;        
		UnityEngine.AI.NavMeshAgent nav;               
		Animator anim;
		AudioSource zombieAudio;
		


		void Awake ()
		{
			player = GameObject.FindGameObjectWithTag ("Player").transform;
			playerGameObject = GameObject.FindGameObjectWithTag ("Player");
			playerHealth = player.GetComponent <PlayerHealth> ();
			enemyHealth = GetComponent <EnemyHealth> ();
			nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
			anim = GetComponent <Animator> ();
			zombieAudio = GetComponent<AudioSource> ();
			attackClips = new AudioClip[3];
			attackClips.SetValue (zombieAttackAudioClip1, 0);
			attackClips.SetValue (zombieAttackAudioClip2, 1);
			attackClips.SetValue (zombieAttackAudioClip3, 2);
		}

		void OnTriggerEnter (Collider other)
		{
			if(other.gameObject == playerGameObject)
			{
				playerInRange = true;
			}
		}

		void OnTriggerExit (Collider other)
		{
			// If the exiting collider is the player...
			if(other.gameObject == playerGameObject)
			{
				// ... the player is no longer in range.
				playerInRange = false;
			}
		}

		void Update ()
		{
			anim.SetBool ("IsRunning", false);
			anim.SetBool ("IsAttacking", false);
			
			timer += Time.deltaTime;
			
			
			//if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
		if (enemyHealth.currentHealth > 0) {
			// set the destination of the nav mesh agent to the player.
			anim.SetBool ("IsRunning", true);
			nav.SetDestination (player.position);

			// If the timer exceeds the time between attacks, the player is in range and this enemy is alive
			if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0) {
				Attack ();
			}
				
		} 
		else {
			nav.enabled = false;
		}
			
		}

		void Attack ()
		{
			// Reset the timer.
			timer = 0f;
			// Start attack animation
			anim.SetBool ("IsAttacking", true);
			if (playerHealth.currentHealth > 0) {
				zombieAudio.clip = attackClips [attackClipsIndex];
				zombieAudio.Play ();
				attackClipsIndex++;
				if (attackClipsIndex >= 3) {
					attackClipsIndex = 0;
				}
				playerHealth.TakeDamage (attackDamage);
			} 
			else {
				timeBetweenAttacks = 6f;
				zombieAudio.clip = zombieEatAudioClip;
				zombieAudio.Play ();
				var blood = (GameObject)Instantiate (bloodPrefab, transform.position, transform.rotation);
				Destroy (blood, 6f);
			}
		}
}
