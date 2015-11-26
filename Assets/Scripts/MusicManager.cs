using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip chaseMusic;
	public AudioClip ambienceMusic;

	// Use this for initialization
	void Start () 
	{
		GetComponent<AudioSource>().clip = ambienceMusic;
		GetComponent<AudioSource> ().Play ();
		GetComponent<AudioSource> ().loop = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void playChase()
	{
		GetComponent<AudioSource> ().clip = chaseMusic;
		GetComponent<AudioSource> ().Play ();
		GetComponent<AudioSource> ().loop = true;
	}

	public void playNormal()
	{
		GetComponent<AudioSource> ().clip = ambienceMusic;
		GetComponent<AudioSource> ().Play();
		GetComponent<AudioSource> ().loop = true;
	}
}
