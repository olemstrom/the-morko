using UnityEngine;
using System.Collections;

public class PlayerWin : MonoBehaviour {

	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.name == "Player" &&  GlobalVariables.keyFound()){
			Debug.Log ("PLAYER WIN");
		}
	}
}
