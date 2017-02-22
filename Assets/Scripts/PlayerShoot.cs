using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
	public int damagePerShot = 20;                  // The damage inflicted by each bullet.
	public float timeBetweenBullets = 0.15f;        // The time between each shot.
	public float timeBetweenGrenades = 1f;        // The time between each grenade.
	public float range = 100f;                      // The distance the gun can fire.


	float timer;                                    // A timer to determine when to fire.
	Ray shootRay = new Ray();                       // A ray from the gun end forwards.
	RaycastHit shootHit;                            // A raycast hit to get information about what was hit.
	int shootableMask;                              // A layer mask so the raycast only hits things on the shootable layer.
	ParticleSystem gunParticles;                    
	LineRenderer gunLine;                           
	AudioSource gunAudio;                           
	Light gunLight;
	Animator anim;
	float effectsDisplayTime = 0.2f;                // The proportion of the timeBetweenBullets that the effects will display for.
	public GameObject grenadePrefab;
	public GameObject bloodPrefab;
	public AudioClip shootClip;
	public AudioClip grenadePinPullClip;

	void Awake ()
	{
		shootableMask = LayerMask.GetMask ("Shootable");
		gunParticles = GetComponent<ParticleSystem> ();
		gunLine = GetComponent <LineRenderer> ();
		gunAudio = GetComponent<AudioSource> ();
		gunLight = GetComponent<Light> ();
		anim = GetComponentInParent <Animator>();
	}


	void Update ()
	{
		anim.SetBool ("IsShooting", false);
		timer += Time.deltaTime;

		if(Input.GetMouseButton(0) && timer >= timeBetweenBullets && Time.timeScale != 0)
		{
			gunAudio.clip = shootClip;
			Shoot ();
		}

		if(Input.GetMouseButtonDown(1) && timer >= timeBetweenGrenades && Time.timeScale != 0 && GameManager.numberOfGrenades != 0)
		{
			gunAudio.clip = grenadePinPullClip;
			gunAudio.Play ();
		}
	
		if(Input.GetMouseButtonUp(1) && timer >= timeBetweenGrenades && Time.timeScale != 0 && GameManager.numberOfGrenades != 0)
		{
			ThrowGrenade();
		}
			
		// If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for
		if(timer >= timeBetweenBullets * effectsDisplayTime)
		{
			DisableEffects ();
		}


	}


	public void DisableEffects ()
	{
		// Disable the line renderer and the light.
		gunLine.enabled = false;
		gunLight.enabled = false;
	}


	void Shoot ()
	{
		anim.SetBool ("IsShooting", true);

		// Reset the timer.
		timer = 0f;

		// Play the gun shot audioclip.
		gunAudio.Play ();

		// Enable the lights.
		gunLight.enabled = true;

		// Stop the particles from playing if they were, then start the particles.
		gunParticles.Stop ();
		gunParticles.Play ();

		// Enable the line renderer and set it's first position to be the end of the gun.
		gunLine.enabled = true;
		gunLine.SetPosition (0, transform.position);

		// Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
		shootRay.origin = transform.position;
		shootRay.direction = transform.forward;

		// Perform the raycast against gameobjects on the shootable layer and if it hits something...
		if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
		{
			// Try and find an EnemyHealth script on the gameobject hit.
			EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();

			// If the EnemyHealth component exist...
			if(enemyHealth != null)
			{
				// ... the enemy should take damage.
				enemyHealth.TakeDamage (damagePerShot);
				Transform enemyTransform = enemyHealth.GetComponentInParent<Transform> ();
				Vector3 offset = new Vector3 (0, 1, 0);
				Vector3 enemyPosition = enemyTransform.position + offset;
				var blood = (GameObject)Instantiate (bloodPrefab, enemyPosition, enemyTransform.rotation);
				Destroy (blood, 1f);
			}

			// Set the second position of the line renderer to the point the raycast hit.
			gunLine.SetPosition (1, shootHit.point);
		}
		// If the raycast didn't hit anything on the shootable layer...
		else
		{
			// ... set the second position of the line renderer to the fullest extent of the gun's range.
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}
			
	}

	void ThrowGrenade()
	{
		GameManager.numberOfGrenades -= 1;
		timer = 0f;
		var grenade = (GameObject)Instantiate (grenadePrefab, gameObject.transform.position, gameObject.transform.rotation);
		grenade.GetComponent<Rigidbody> ().velocity = grenade.transform.forward * 20;
	}
}
