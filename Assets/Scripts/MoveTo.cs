using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour 
{
	public Transform player;
	public Transform rayOrig;
	public Transform playerTargetPoint;
	
	public int aggroDistance;
	public int turboMörkö;
	public AudioClip murina;
	public GameObject musicMgr;

	public Camera playerCamera;

	private NavMeshAgent agent;

	void Start () 
	{
		GetComponent<AudioSource> ().clip = murina;
		agent = GetComponent<NavMeshAgent> ();
		GlobalVariables.isChasing = false;

		/**
		 *SEURAAVA KOODI VAIN TESTAUSTA VARTEN
		 **/

		WPSet.wpList.Add (new Vector3 (34.0f, -1.5f, 40.0f));
		WPSet.wpList.Add (new Vector3(-56.0f, -1.5f, 24.0f));
		WPSet.wpList.Add (new Vector3(38.0f, -1.5f, -111.0f));
		WPSet.wpList.Add (new Vector3(-46.0f, -1.5f, -90.0f));

		/**
		 * LOPPUU TÄHÄN 
		 **/

		WPSet.shuffleWPList();
		WPSet.refillWPStack();

		agent.destination = WPSet.wpStack.Peek ();

	}

	void Update()
	{
		//jos tarpeeks lähellä WPtä nii vaihetaan seuraavaan
		if(Vector3.Distance(GetComponent<Transform>().position, agent.destination) < 2 && !GlobalVariables.isChasing){
		
			if(WPSet.wpStack.Count == 0){
				WPSet.shuffleWPList();
				WPSet.refillWPStack();
			}
			WPSet.wpList.Add(WPSet.wpStack.Peek ());
			WPSet.wpStack.Pop();
			agent.destination = WPSet.wpStack.Peek ();
		}

		// Tää tapahtuu kun pelaaja on tarpeeks lähellä aggrottavaksi, eikä hiippaile takana
		if(playerIsAggroable() && !GlobalVariables.isChasing){
			Debug.Log ("Player in sight!");
			GetComponent<AudioSource>().Play();
			musicMgr.GetComponent<MusicManager>().playChase();
			GlobalVariables.isChasing = true;
			agent.speed = turboMörkö;
			agent.destination = player.position;
			CameraShake.setChase();
		}

		// Jos pelaajaa jahdataan, nii tää tapahtuu
		if(Physics.Linecast (rayOrig.position, playerTargetPoint.position) && GlobalVariables.isChasing){
			Debug.Log ("Player lost");
			Debug.DrawLine (rayOrig.position, playerTargetPoint.position, Color.red);
			GlobalVariables.isChasing = false;
			agent.speed = 4; // perusnopeus
			musicMgr.GetComponent<MusicManager>().playNormal ();

			if(WPSet.wpStack.Count == 0){
				WPSet.shuffleWPList();
				WPSet.refillWPStack();
			}
			WPSet.wpList.Add(WPSet.wpStack.Peek ());
			WPSet.wpStack.Pop ();
			agent.destination = WPSet.wpStack.Peek ();
			CameraShake.setChase();
		}

		//Pelaajan ruutu jäätyy, mitä lähempänä mörriä ollaan.
		playerCamera.GetComponent<FrostEffect>().FrostAmount = (4 / Vector3.Distance(rayOrig.position, player.position));
	}

	// Testaa näkeekö mörkö pelaajan ja onko etäisyys tarpeeksi pieni
	private bool playerIsAggroable()
	{
		if(!Physics.Linecast (rayOrig.position, playerTargetPoint.position) && !GlobalVariables.isChasing && Vector3.Distance(rayOrig.position, player.position) <= aggroDistance){
			return true;
		} else {
			return false;
		}
	}
}
