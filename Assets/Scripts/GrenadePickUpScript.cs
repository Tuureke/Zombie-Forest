using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePickUpScript : MonoBehaviour {

	GameObject playerGameObject; 

	void Awake ()
	{
		playerGameObject = GameObject.FindGameObjectWithTag ("Player");
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject == playerGameObject)
		{
			GameManager.numberOfGrenades += 1;
			Destroy (gameObject);
		}
	}

}
