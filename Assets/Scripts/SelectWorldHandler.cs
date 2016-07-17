using UnityEngine;
using System.Collections;

public class SelectWorldHandler : MonoBehaviour {

    private GameObject currentCamera;
    private GameObject worldSelectionObject;
    private GameObject mainMenuObject;
	private bool isViewingWorldSection = false;

    void Start ()
    {
		currentCamera = GameObject.Find("Main Camera");
        worldSelectionObject = GameObject.Find("Select World");
        mainMenuObject = GameObject.Find("Main Menu");
		if(currentCamera == null || worldSelectionObject == null){
			Debug.LogError("SelectWorldHandler Initialization Issue.");
		}
    }

	public void viewWorldSelection () {
		// Copy Current Camera Position to an auxiliary variable.
		Vector3 cameraPosition = currentCamera.transform.position;
		// Transfer worldSelectionObject.trasnform.y into the auxiliary variable
		cameraPosition.y = worldSelectionObject.transform.position.y;
		// Set the aux variable into the camera position.
		currentCamera.transform.position = cameraPosition;
		isViewingWorldSection = true;
	}

	// Similar to the the viewWorldSelection
	public void viewMainMenu () {
		Vector3 cameraPosition = currentCamera.transform.position;
		cameraPosition.y = mainMenuObject.transform.position.y;
		currentCamera.transform.position = cameraPosition;
		isViewingWorldSection = false;
	}
	
    void OnGUI() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
			if(isViewingWorldSection){
				viewMainMenu();
			}
		}
    }
}