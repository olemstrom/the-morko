using UnityEngine;
using System.Collections;

public class PlayerLight : MonoBehaviour {
	
	public Light flashlight;
	public float lightIncrement, lightDecrement;
	public float maxLight;
	private bool flashlightOn = false;
	//private float timeLeft = 30.0f;

	// Use this for initialization
	void Start () {
		flashlight.intensity = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (1)) {
			if(flashlightOn){
				flashlightOn = false;
			} else {
				flashlightOn = true;
			}
		}
		adjustLight ();
	}

	public void adjustLight()
	{
		if (flashlightOn && flashlight.intensity < maxLight) {
			flashlight.intensity += lightIncrement;
		}

		if (!flashlightOn && flashlight.intensity > 0) {
			flashlight.intensity -= lightDecrement;
		}
	}
}