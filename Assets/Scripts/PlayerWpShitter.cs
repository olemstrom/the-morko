using UnityEngine;
using System.Collections;

public class PlayerWpShitter : MonoBehaviour
{
	private float alku;
	private Transform playerPos;
	private bool isChasing;

	// Use this for initialization
	void Start ()
	{
		isChasing = false;
		playerPos = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!isChasing) {
			//Tähä tulee waypointtien luominen ja pudottelu ja lisääminen listaan.
		}
	}
}

