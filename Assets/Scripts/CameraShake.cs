using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	public GameObject playerCamera;
	private static bool chasemode = false;
	
	// Update is called once per frame
	void Update () 
	{
		if(chasemode){
			float shakeX = Random.Range(0.1f, -0.1f);
			float shakeY = Random.Range(0.1f, -0.1f);
			float shakeZ = Random.Range(0.1f, -0.1f);

			playerCamera.transform.position += new Vector3(shakeX, shakeY, shakeZ);
		}
	}

	public static void setChase()
	{
			chasemode = true;
	}

	public static void setChaseNo()
	{
		chasemode = false;
	}

}
