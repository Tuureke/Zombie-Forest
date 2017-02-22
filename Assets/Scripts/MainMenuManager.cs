using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class MainMenuManager : MonoBehaviour {

	GameObject[] Canvases;
	GameObject MainMenuCanvas;
	GameObject ControlsCanvas;
	GameObject CreditsCanvas;

	void Start()
	{
		Canvases = GameObject.FindGameObjectsWithTag ("Canvas");
		//MainMenuCanvas = Canvases [0];
		ControlsCanvas = Canvases [1];
		CreditsCanvas = Canvases [2];
		BackToMainMenu ();
	}

	public void StartGame()
	{
		SceneManager.LoadSceneAsync ("Game");
	}

	public void ShowControls()
	{
		ControlsCanvas.SetActive(true);
	}

	public void ShowCredits()
	{
		CreditsCanvas.SetActive(true);
	}
		

	public void BackToMainMenu()
	{
		ControlsCanvas.SetActive (false);
		CreditsCanvas.SetActive (false);
	}

	public void Quit()
	{
		#if UNITY_EDITOR 
		EditorApplication.isPlaying = false;
		#else 
		Application.Quit();
		#endif
	}
}
