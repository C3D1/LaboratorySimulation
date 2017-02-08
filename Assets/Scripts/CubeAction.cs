using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAction : MonoBehaviour {

	public bool canInteract;
	public bool interactModeActivated = false;

	private GameObject shaderCube;
	private Color startColor;

	// Use this for initialization
	void Start () {

		startColor = GetComponent<Renderer>().material.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.I)) {
			// Only if the object can interact.
			if (canInteract == true) {
				if (interactModeActivated == true) { 
					GetComponent<Renderer>().material.color = startColor; // Turn the objects back to their original color.
					interactModeActivated = false;
				} else { 
					GetComponent<Renderer>().material.color = Color.blue; // Turn all objects, which can interact, blue.
					interactModeActivated = true;
				}
			}
		} 
	}

	public void ExecuteCubeAction() {
		// EXECUTE ACTION
	}

	// The cube should turn back to it's original color.
	void OnMouseExit() {
		if (User.gazeControlMode == false & canInteract == true) {
			if (interactModeActivated == false) {
				GetComponent<Renderer>().material.color = startColor;
			} else {
				GetComponent<Renderer>().material.color = Color.blue;
			}
		}
	}

	// Turns the cube into green. You've activated the "action" with a click on the cube.
	void OnMouseDown() {
		// Only if the gaze control mode isn't activated, the object is allowed to interact and the interact mode is activated.
		if (User.gazeControlMode == false & interactModeActivated == true && canInteract == true) {
			GetComponent<Renderer>().material.color = Color.green;
		}
	}
}
