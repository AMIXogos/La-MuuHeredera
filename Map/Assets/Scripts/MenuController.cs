using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public Canvas ExitMenu;
	public Button startText;
	public Button exitText;

	// Use this for initialization
	void Start () 

	{
		ExitMenu = ExitMenu.GetComponent<Canvas>();
		startText = startText.GetComponent<Button>();
		exitText = exitText.GetComponent<Button>();
		ExitMenu.enabled = false;
	}

	public void ExitPress()

	{
	    ExitMenu.enabled = true;
		startText.enabled = false;
		exitText.enabled = false; 
	}

	public void NoPress()

	{
		ExitMenu.enabled = false;
		startText.enabled = true;
		exitText.enabled = true;
	}

	public void StartLevel()

	{
		Application.LoadLevel (1);
	}

	public void ExitGame()

	{				
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
		
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
