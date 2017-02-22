using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour {

	public AudioMixer audioMixer;

	public void SetSFXLvl(float sfxLvl)
	{
		audioMixer.SetFloat("SFX", sfxLvl);
	}

	public void SetMusicLvl (float musicLvl)
	{
		audioMixer.SetFloat ("Music", musicLvl);
	}

	public void SetMasterLvl (float masterLvl)
	{
		audioMixer.SetFloat ("Master", masterLvl);
	}

	public void SetAmbienceLvl (float ambienceLvl)
	{
		audioMixer.SetFloat ("Ambience", ambienceLvl);
	}

	public void SetVoiceLvl (float voiceLvl)
	{
		audioMixer.SetFloat ("Voice", voiceLvl);
	}
}
