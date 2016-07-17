using UnityEngine;
using System.Collections;

public class FakePlanetMotion : MonoBehaviour {

	// Update is called once per frame
	public float motionSpeed = 1f;
	private float maxRotationTime = 10f;
	private float currentRotationTime = 0f;
	private bool forwardRotation = true;
	
	void Update () {
		if(currentRotationTime < maxRotationTime){
			if(forwardRotation == true){
				transform.RotateAround(transform.position, transform.forward, Time.deltaTime * motionSpeed);
				currentRotationTime += Time.deltaTime;
			}
			else{
				transform.RotateAround(transform.position, -transform.forward, Time.deltaTime * motionSpeed);
				currentRotationTime += Time.deltaTime;
			}
		}
		else{
			forwardRotation = !forwardRotation;
			currentRotationTime = 0f;
		}
	}
}
