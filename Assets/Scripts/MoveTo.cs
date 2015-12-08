using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public Camera playerCamera;

	private NavMeshAgent agent;
	
	private Stack<Vector3> wpStack = new Stack<Vector3>();
	private List<Vector3> wpList = new List<Vector3>();

	void Start () 
	{
		GetComponent<AudioSource> ().clip = murina;
		agent = GetComponent<NavMeshAgent> ();
		chasingPlayer = false;

		/**
		 *SEURAAVA KOODI VAIN TESTAUSTA VARTEN
		 **/

		wpList.Add (new Vector3 (34.0f, -1.5f, 40.0f));
		wpList.Add (new Vector3(-56.0f, -1.5f, 24.0f));
		wpList.Add (new Vector3(38.0f, -1.5f, -111.0f));
		wpList.Add (new Vector3(-46.0f, -1.5f, -90.0f));

		/**
		 * LOPPUU TÄHÄN 
		 **/

		shuffleWPList();
		refillWPStack();

		agent.destination = wpStack.Peek ();

	}

	void Update()
	{
		//jos tarpeeks lähellä WPtä nii vaihetaan seuraavaan
		if(Vector3.Distance(GetComponent<Transform>().position, agent.destination) < 2 && !chasingPlayer){
		
			if(wpStack.Count == 0){
				shuffleWPList();
				refillWPStack();
			}
			wpList.Add(wpStack.Peek ());
			wpStack.Pop();
			agent.destination = wpStack.Peek ();
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
			playerCamera.GetComponent<FrostEffect>().FrostAmount = (1 / Vector3.Distance(rayOrig.position, player.position));
		}

		if(Physics.Linecast (rayOrig.position, playerTargetPoint.position) && chasingPlayer){
			Debug.Log ("Player lost");
			Debug.DrawLine (rayOrig.position, playerTargetPoint.position, Color.red);
			chasingPlayer = false;
			agent.speed = 4; // perusnopeus
			musicMgr.GetComponent<MusicManager>().playNormal ();

			if(wpStack.Count == 0){
				shuffleWPList();
				refillWPStack();
			}
			wpList.Add(wpStack.Peek ());
			wpStack.Pop ();
			agent.destination = wpStack.Peek ();
			CameraShake.setChase();
			playerCamera.GetComponent<FrostEffect>().FrostAmount = 0.0f;
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

	private void shuffleWPList()
	{
		for(int t = 0; t < wpList.Count; t++){
			Vector3 tmp = wpList[t];
			int r = Random.Range(t, wpList.Count);
			wpList[t] = wpList[r];
			wpList[r] = tmp;
		}
	}

	private void refillWPStack()
	{
		foreach(Vector3 v in wpList){
			wpStack.Push (v);
		}
		wpList.Clear ();
	}
}
