using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour 
{
	public Transform player;
	public Transform rayOrig;
	public Transform playerTargetPoint;
	
	public int aggroDistance;
	public int turboMörkö;
	public int frostVariable;
	public AudioClip murina;
	public GameObject musicMgr;

	public Camera playerCamera;

	private NavMeshAgent agent;

	public void Init() {
		GetComponent<AudioSource> ().clip = murina;
		agent = GetComponent<NavMeshAgent> ();
		GlobalVariables.isChasing = false;
		
		WPSet.shuffleWPList();
		
		agent.destination = WPSet.wpList[WPSet.index];
	}

	void Update()
	{
		Debug.DrawLine (rayOrig.position, playerTargetPoint.position, Color.green);
		Debug.DrawLine (rayOrig.position, player.position, Color.green);
		
		//jos tarpeeks lähellä WPtä nii vaihetaan seuraavaan
		if(Vector3.Distance(GetComponent<Transform>().position, agent.destination) < 1 && !GlobalVariables.isChasing){
		
			if(WPSet.index == WPSet.wpList.Count){
				WPSet.shuffleWPList();
			}
			agent.destination = WPSet.wpList[WPSet.index];
			WPSet.index++;
		}

		// Tää tapahtuu kun pelaaja on tarpeeks lähellä aggrottavaksi, eikä hiippaile takana
		if(playerIsAggroable()){
			Debug.Log ("Player in sight!");
			GetComponent<AudioSource>().Play();
			musicMgr.GetComponent<MusicManager>().playChase();

			//Tää erikseen, että kamera-efekti asetetaan vaan kerran
			CameraShake.setChase();

			GlobalVariables.isChasing = true;
			agent.speed = turboMörkö;
			agent.destination = player.position;
		}

		// Kun pelaaja katoo
		if(Physics.Linecast (rayOrig.position, playerTargetPoint.position) && GlobalVariables.isChasing){
			Debug.Log ("Player lost");
			Debug.DrawLine (rayOrig.position, playerTargetPoint.position, Color.red);
			GlobalVariables.isChasing = false;
			agent.speed = 4; // perusnopeus
			musicMgr.GetComponent<MusicManager>().playNormal ();

			if(WPSet.index == WPSet.wpList.Count){
				WPSet.shuffleWPList();
			}
			//mennään pelaajan viimeiseen olinpaikkaan
			agent.destination = player.position;

			WPSet.index++;
			CameraShake.setChase();
		} else if (GlobalVariables.isChasing){
			agent.destination = player.position;
		}

		//Pelaajan ruutu jäätyy, mitä lähempänä mörriä ollaan.
		playerCamera.GetComponent<FrostEffect>().FrostAmount = (frostVariable / Vector3.Distance(rayOrig.position, player.position));
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
