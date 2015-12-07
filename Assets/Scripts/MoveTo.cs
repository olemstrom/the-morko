using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour 
{
	public Transform player;
	public Transform rayOrig;
	public Transform playerTargetPoint;

	private bool chasingPlayer;
	public int aggroDistance;
	public int turboMörkö;
	public AudioClip murina;
	public GameObject musicMgr;

	private NavMeshAgent agent;

	private WPNode targetWP;

	void Start () 
	{
		GetComponent<AudioSource> ().clip = murina;
		agent = GetComponent<NavMeshAgent> ();
		chasingPlayer = false;
		targetWP = new WPNode();

		/**
		 *SEURAAVA KOODI VAIN TESTAUSTA VARTEN
		 **/
		WPNode nex0 = new WPNode(new Vector3 (34.0f, -1.5f, 40.0f));
		WPNode nex1 = new WPNode(new Vector3(-56.0f, -1.5f, 24.0f));
		WPNode nex2 = new WPNode(new Vector3(38.0f, -1.5f, -111.0f));
		WPNode nex3 = new WPNode(new Vector3(-46.0f, -1.5f, -90.0f));

		nex0.setNext (nex1);
		nex1.setNext(nex2);
		nex2.setNext (nex3);
		nex3.setNext (nex0);

		targetWP = nex0;

		/**
		 * LOPPUU TÄHÄN 
		 **/

		agent.destination = targetWP.getWaypoint();

	}

	void Update()
	{
		//jos tarpeeks lähellä WPtä nii vaihetaan seuraavaan
		if(Vector3.Distance(GetComponent<Transform>().position, agent.destination) < 2){
			targetWP = targetWP.getNext ();
			agent.destination = targetWP.getWaypoint();
		}

		if(playerIsAggroable() && !chasingPlayer){
			Debug.Log ("Player in sight!");
			GetComponent<AudioSource>().Play();
			musicMgr.GetComponent<MusicManager>().playChase();
			chasingPlayer = true;
			agent.speed = turboMörkö;
			agent.destination = player.position;
			CameraShake.setChase();
		}

		if(chasingPlayer){
			agent.destination = player.position;
		}

		if(Physics.Linecast (rayOrig.position, playerTargetPoint.position) && chasingPlayer){
			Debug.Log ("Player lost");
			Debug.DrawLine (rayOrig.position, playerTargetPoint.position, Color.red);
			chasingPlayer = false;
			agent.speed = 4; // perusnopeus
			musicMgr.GetComponent<MusicManager>().playNormal ();
			targetWP = targetWP.getNext ();
			agent.destination = targetWP.getWaypoint();
			CameraShake.setChase();
		}
	}

	// Testaa näkeekö mörkö pelaajan ja onko etäisyys tarpeeksi pieni
	private bool playerIsAggroable()
	{
		if(!Physics.Linecast (rayOrig.position, playerTargetPoint.position) && !chasingPlayer && Vector3.Distance(rayOrig.position, player.position) <= aggroDistance){
			return true;
		} else {
			return false;
		}
	}
}
