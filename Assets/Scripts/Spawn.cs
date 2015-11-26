using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public GameObject enemy;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawner", 3f, 3f);
	}

	void Spawner ()
	{
		// Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
		Instantiate (enemy, new Vector3(-8.6f, 0.0f, 42.0f), new Quaternion(0.0f, 180.0f, 0.0f, 0.0f));
	}

}