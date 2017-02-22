using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

	public float radius = 10.0f;
	public float explosiveDelay = 3.0f;
	public GameObject explosionPrefab;

	void Update () {
		explosiveDelay -= Time.deltaTime;
		if (explosiveDelay <= 0) {
			explode ();
		}
	}

	void explode()
	{
		Vector3 grenadePosition = transform.position;
		var explosion = (GameObject)Instantiate (explosionPrefab, grenadePosition, transform.rotation);
		Collider[] colliders = Physics.OverlapSphere (grenadePosition, radius);
		foreach (Collider hit in colliders) {
			EnemyHealth eh = hit.GetComponent<EnemyHealth> ();
			PlayerHealth ph = hit.GetComponent<PlayerHealth> ();
			if (eh) {
				eh.TakeDamage (100);
			}
			if (ph) {
				ph.TakeDamage (20);
			}
		}
		Destroy (explosion, 2f);
		Destroy (gameObject);
	}
	
}
