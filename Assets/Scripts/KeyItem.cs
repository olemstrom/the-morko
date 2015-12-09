using UnityEngine;
using System.Collections;

public class KeyItem : MonoBehaviour
{
	void OnTriggerEnter(Collider collider)
	{

		if(collider.gameObject.name == "Player"){
			Destroy (gameObject);
			GlobalVariables.keyCount++;
		}
	}
}

