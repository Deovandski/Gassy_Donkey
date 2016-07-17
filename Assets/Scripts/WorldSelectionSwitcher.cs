using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorldSelectionSwitcher : MonoBehaviour {


	// Swipe Detection
	private float minSwipeLength = 5.0f;
	private Vector2 firstPressPos;
	private Vector2 secondPressPos;
	private Vector2 currentSwipe;
	private Vector2 firstClickPos;
	private Vector2 secondClickPos;
	private bool swipeLeft = false;
	private bool swipeRight = false;
	private float AfterSwipeWaitTime = 0.3f;
	private float currentTime = 0;
	private bool disableSwipeDetection = false;

	// Planet movimentation
	private Transform[] planets;
	public int planetsCount;
	private int currentPlanetIndex = 0;

	// Planet Text Name
	private Text planetTextName;


	void Start() {
		planets = GetComponentsInChildren<Transform>();
		planetsCount = transform.childCount;
		planetTextName = GameObject.Find("Selected Planet Text").GetComponent<Text>();

		if(planets == null || planetsCount < 0 || planetTextName == null){
			Debug.LogError("WoldSelectionSwitcher Initialization Issue.");
		}
		else{
			updatePlanetTextName();
		}
	}

	void Update() {
		if(disableSwipeDetection == true){
			currentTime -= Time.deltaTime;
			if(currentTime <= 0){
				currentTime = 0f;
				disableSwipeDetection = false;
			}
		}
		else{
			DetectSwipe();
			if(swipeLeft == true){
				movePlanets(false);
				swipeLeft = false;
				currentTime = AfterSwipeWaitTime;
				disableSwipeDetection = true;
			}
			else if(swipeRight == true){
				movePlanets(true);
				swipeRight = false;
				currentTime = AfterSwipeWaitTime;
				disableSwipeDetection = true;
			}
		}
	}


	void movePlanets(bool swipeRight){
		if(swipeRight == true && currentPlanetIndex == 0){
			// Do nothing
		}
		else if(swipeRight == true && currentPlanetIndex > 0){
			foreach (Transform child in planets) {
				var currentPosition = child.position;
				currentPosition.x += 50;
				child.position = currentPosition;
			}
			currentPlanetIndex--;
		}
		else if((planetsCount - 1) > currentPlanetIndex){
			foreach (Transform child in planets) {
				var currentPosition = child.position;
				currentPosition.x -= 50;
				child.position = currentPosition;
			}
			currentPlanetIndex++;
		}
		//Debug.Log(planetsCount.ToString() + " | " + currentPlanetIndex.ToString());
		updatePlanetTextName();
	}

	void updatePlanetTextName(){
		switch (currentPlanetIndex)
		{
			case 0:
				planetTextName.text = "Mercury";
			break;
			case 1:
				planetTextName.text = "Venus";
			break;
			case 2:
				planetTextName.text = "Earth";
			break;
			case 3:
				planetTextName.text = "Mars";
			break;
			case 4:
				planetTextName.text = "Jupiter";
			break;
			case 5:
				planetTextName.text = "Saturn";
			break;
			case 6:
				planetTextName.text = "Uranus";
			break;
			case 7:
				planetTextName.text = "Neptune";
			break;
			default:
				planetTextName.text = "---";
			break;
		}
	}

	void DetectSwipe() {

		if (Input.touches.Length > 0) {

			var t = Input.GetTouch(0);

			if (t.phase == TouchPhase.Began) {
				firstPressPos = new Vector2(t.position.x, t.position.y);
			}

			if (t.phase == TouchPhase.Ended) {

				secondPressPos = new Vector2(t.position.x, t.position.y);
				currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

				//Make sure it was a legit swipe, not a tap
				if (currentSwipe.magnitude < minSwipeLength) {
					//this was a tap, not a swipe
					return;
				}
				currentSwipe.Normalize();
			}
		}
		else {

			if (Input.GetMouseButtonDown(0)) {
				firstClickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			}

			if (Input.GetMouseButtonUp (0)){
				secondClickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				currentSwipe = new Vector3(secondClickPos.x - firstClickPos.x, secondClickPos.y - firstClickPos.y);

				//Make sure it was a legit swipe, not a click
				if (currentSwipe.magnitude < minSwipeLength) {
					//this was a click, not a swipe
					return;
				}
				currentSwipe.Normalize();
			}
		}
		//Swipeup
		if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
			// Do not act upon Swipe Up
		}
		//Swipedown
		else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
			// Do not act upon Swipe Down
		} 
		//Swipeleft
		else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
			swipeLeft = true;
			currentSwipe.x = 0;
			currentSwipe.y = 0;
		} 
		//Swiperight
		else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
			swipeRight = true;
			currentSwipe.x = 0;
			currentSwipe.y = 0;
		}
	}
}