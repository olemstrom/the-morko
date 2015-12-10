using UnityEngine;
using System.Collections;

public class KeyItem : MonoBehaviour
{
	public Texture aTexture;
	private Texture tmp;

	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.name == "Player"){
			gameObject.GetComponent<MeshRenderer>().enabled = false;
			gameObject.GetComponent<MeshCollider>().enabled = false;
			GlobalVariables.keyCount++;
			GlobalVariables.keyChase = true;
		}
	}

	void Start()
	{
		tmp = aTexture;
		aTexture = null;
	}

	void Update()
	{
		if(GlobalVariables.keyFound ())
			aTexture = tmp;
	}

	void OnGUI()
	{
		//Piirretään täs tyhjää kunnes avain löytyy ja tekstuuriks asetetaan tmpst poimittu avain
		GUI.DrawTexture(new Rect(10, 10, 60, 60), aTexture, ScaleMode.StretchToFill, true, 10.0F);
	}
}

