using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour 
{
	public Transform goal;
	private bool chasePlayed;
	public int aggroDistance;
	public int turboMörkö;
	public AudioClip murina;
	public GameObject musicMgr;

	void Start () 
	{
		GetComponent<AudioSource> ().clip = murina;
		NavMeshAgent agent = GetComponent<NavMeshAgent> ();
		agent.destination = goal.position;
		chasePlayed = false;
	}

	void Update()
	{
		NavMeshAgent agent = GetComponent<NavMeshAgent> ();
		agent.destination = goal.position;

		if (Vector3.Distance(agent.transform.position, goal.position) <= aggroDistance && !chasePlayed){
			GetComponent<AudioSource>().Play();
			musicMgr.GetComponent<MusicManager>().playChase();
			chasePlayed = true;
			agent.speed = turboMörkö;
		}

		if (Vector3.Distance (agent.transform.position, goal.position) > aggroDistance && chasePlayed) {
			chasePlayed = false;
			agent.speed = 2; // perusnopeus
			musicMgr.GetComponent<MusicManager>().playNormal ();
			//Pitäskö tää viel fade in?
		}
	}
}
