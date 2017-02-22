using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnemySoundScript : MonoBehaviour {

	public AudioClip ZombieDeath1;
	public AudioClip ZombieDeath2;
	public AudioClip ZombieDeath3;
	AudioClip[] deathClips;
	AudioSource ZombieAudio;

	void Awake () {
		ZombieAudio = GetComponent<AudioSource> ();
		deathClips = new AudioClip[3];
		deathClips.SetValue (ZombieDeath1, 0);
		deathClips.SetValue (ZombieDeath2, 1);
		deathClips.SetValue (ZombieDeath3, 2);
		ZombieAudio.clip = deathClips[Random.Range(0,2)];
		ZombieAudio.Play ();
	}

}
