using UnityEngine;
using System.Collections;

public class MainVesselMovement : MonoBehaviour {

	public string travelDirection = "left";
	public float vesselSpeed = 25f;
	public float maxTravelDistance = 300f;
	private float distanceTraveled = 0f;
	
	// Update is called once per frame
	void Update () {
		// If we pass the max travel distance, move the object to the original location, and repeat movement on next update.
		Vector3 currentPosition = transform.position;
		if(distanceTraveled >= maxTravelDistance || distanceTraveled <= (maxTravelDistance *-1)){
			if(travelDirection == "left"){
				currentPosition.x -= distanceTraveled;
			}
			else{
				currentPosition.x += distanceTraveled;
			}
			distanceTraveled = 0f;
			transform.position = currentPosition;
		}
		else{
			if(travelDirection == "left")
			{
				transform.Translate (Vector2.up * vesselSpeed * Time.deltaTime);
			}
			else
			{
				transform.Translate (Vector2.down * vesselSpeed * Time.deltaTime);
			}
		}
		Vector3 newPosition = transform.position;
		distanceTraveled += newPosition.x - currentPosition.x;
	}
}
