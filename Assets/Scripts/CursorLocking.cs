using UnityEngine;
using System.Collections;

public class CursorLocking : MonoBehaviour
{

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked; 
		Cursor.visible = false;
	}
	
	void Update()
	{
		if (Input.GetKeyDown("escape")){
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
}

