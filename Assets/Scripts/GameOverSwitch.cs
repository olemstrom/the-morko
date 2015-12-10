using UnityEngine;
using System.Collections;

public class GameOverSwitch : MonoBehaviour 
{
	
		public Transform player;
		public Transform rayOrig;
		public Transform playerTargetPoint;
		
		public int aggroDistance;
		public int turboMörkö;
		public int frostVariable;
		public AudioClip murina;
		
		public Camera playerCamera;
		
		private NavMeshAgent agent;
		private bool played, killTimer;	
		private float timer;

		public void Start()
		{
			GetComponent<AudioSource> ().clip = murina;
			agent = GetComponent<NavMeshAgent> ();
			agent.destination = player.position;
			agent.speed = turboMörkö;
		}
		
		void Update()
		{

			if(	Vector3.Distance(rayOrig.position, player.position) < 15 && !played){
				GetComponent<AudioSource>().Play();
				played = true;
			}
			
			//Pelaajan ruutu jäätyy, mitä lähempänä mörriä ollaan.
			playerCamera.GetComponent<FrostEffect>().FrostAmount = (frostVariable / Vector3.Distance(rayOrig.position, player.position));
			
			if(Vector3.Distance(rayOrig.position, player.position) < 5 && !killTimer){
				killTimer = true;
			}

			if(killTimer){
				timer += Time.deltaTime;
				if(timer > 2.0f){
					Debug.Log ("NYT");
					Application.LoadLevel (0);
				}
			}

			

		}
}
