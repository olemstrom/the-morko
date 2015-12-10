using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public Button playButton;
	public Button quitButton;


	// Use this for initialization
	void Start ()
	{
		playButton = playButton.GetComponent<Button>();
		quitButton = quitButton.GetComponent<Button>();
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void StartGame()
	{
		Application.LoadLevel(1);
	}
}
