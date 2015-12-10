using UnityEngine;
using System.Collections;

public class PlayerLose : MonoBehaviour
{

	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.name == "Player"){
			Debug.Log("MÖRKÖ SAI PELAAJAN KIINNI");
			CameraShake.setChaseNo();
			Application.LoadLevel(2);
		}
	}
}

