using UnityEngine;
using System.Collections;

public class RotateStar : MonoBehaviour {

	// Update is called once per frame
	public float rotationSpeed = 2f;
	public bool forwardRotation = true;

	void Update () {
		if(forwardRotation == true){
			transform.RotateAround(transform.position, transform.forward, Time.deltaTime * rotationSpeed);
		}
		else{
			transform.RotateAround(transform.position, -transform.forward, Time.deltaTime * rotationSpeed);
		}
	}
}
