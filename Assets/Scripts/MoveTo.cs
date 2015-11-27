using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour 
{
	public Transform goal;
	public Transform rayOrig;
	public Transform playerTargetPoint;

	private bool chasePlayed;
	public int aggroDistance;
	public int turboMörkö;
	public AudioClip murina;
	public GameObject musicMgr;

	private NavMeshAgent agent;
	private Vector3 distance_v;

	void Start () 
	{
		GetComponent<AudioSource> ().clip = murina;
		agent = GetComponent<NavMeshAgent> ();
		agent.destination = goal.position;
		chasePlayed = false;
		distance_v = new Vector3();
	}

	void Update()
	{
		agent.destination = goal.position;

		if(playerIsAggroable()){
			Debug.Log ("Player in sight!");
			GetComponent<AudioSource>().Play();
			musicMgr.GetComponent<MusicManager>().playChase();
			chasePlayed = true;
			agent.speed = turboMörkö;
		} 

		if(Physics.Linecast (rayOrig.position, playerTargetPoint.position) && chasePlayed){
			Debug.Log ("Player lost");
			Debug.DrawLine (rayOrig.position, playerTargetPoint.position, Color.red);
			chasePlayed = false;
			agent.speed = 4; // perusnopeus
			musicMgr.GetComponent<MusicManager>().playNormal ();
		}
	}

	// Testaa näkeekö mörkö pelaajan ja onko etäisyys tarpeeksi pieni
	private bool playerIsAggroable()
	{
		if(!Physics.Linecast (rayOrig.position, playerTargetPoint.position) && !chasePlayed && Vector3.Distance(rayOrig.position, goal.position) <= aggroDistance){
			return true;
		} else {
			return false;
		}
	}
}
