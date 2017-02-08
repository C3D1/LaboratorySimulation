using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeHandler : MonoBehaviour {

	//private MeshRenderer meshRenderer;
	private float time;
	private GameObject progressCircle; // the progesscirlce, which appears when you're starring on a sensor or action.
	private Animation animationForProgressCircle; // the animation for the progresscircle.
	private GameObject currentCube;
	private GameObject cubeExecuted;
	private bool mode = true;
	private Color green = Color.green;
	private Color white = Color.white;
	private Color magenta = Color.magenta;

	// Use this for initialization
	void Start() {
		time = 0f;
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.X)) {

			User.gazeControlMode = !mode;

			if (mode == true) {
				mode = false;
			} else {
				mode = true;
			}
		}


		if (progressCircle == null) {
			progressCircle = GameObject.FindGameObjectWithTag("ProgressCircle");
			animationForProgressCircle = progressCircle.GetComponentInChildren<Animation>();
			progressCircle.SetActive(false);
		}

		if (User.gazeControlMode == false) {
			if (Cursor.visible == false) {
				Cursor.visible = true;
			}

		} else {
			if (Cursor.visible == true) {
				Cursor.visible = false;
			}

			Vector3 headposition = Camera.main.transform.position; // The current position of the camera.
			Vector3 gazeDirection = Camera.main.transform.forward; // The current direction of the camera.

			string cameraName = Camera.main.gameObject.name;

			// Only the camera of the avatar should open a menu.
			if (cameraName == "FirstPersonCharacter") {

				RaycastHit[] hits;
				bool colliderHit = false;
				hits = Physics.RaycastAll(headposition, gazeDirection, 5f); // Every collision in the direction the avatar looks within 5.

				if (hits.Length > 0) {
					for (int i = 0; i < hits.Length; i++) {
						RaycastHit hit = hits[i]; // Every collider in the direction of the view within the distance of 5f.

						// If the collider is a cube you can interact with.
						if (hit.collider.gameObject.tag == "InteractionCube") {
							// If it isn't the current cube
							if (currentCube != hit.collider.gameObject | currentCube == null) { 
								CubeAction cubeScript = hit.collider.gameObject.GetComponent<CubeAction>();
								if (cubeScript != null && cubeScript.canInteract == true && cubeScript.interactModeActivated == true) {
									IncreaseTimeAndActivateProgessCircle();
									colliderHit = true;
								}
								
								// The user must stay longer than 1 second on the object, to execute the action.
								if (time > 1) {
									currentCube = hit.collider.gameObject;
									time = 0;
									DeactivateProgressCircle();
									Renderer rend = currentCube.GetComponent<Renderer>();
									rend.material.color = green;

									// The last actions which was executed (Not the current one) turns back to its original color.
									if (cubeExecuted != null && cubeExecuted != currentCube) {
										Renderer renderer = cubeExecuted.GetComponent<Renderer>();
										renderer.material.color = Color.blue;
									}
									// Now the last action which was executed, is the current one.
									cubeExecuted = hit.collider.gameObject;

									animationForProgressCircle.Stop();

									hit.collider.gameObject.SendMessage("ExecuteCubeAction"); // Isn't implemented. Just to show that this is the moment, where you can call the individual method to execute an action.
								}
							}
						}
					}
					if (!colliderHit) {
						time = 0;
						DeactivateProgressCircle();
						if (currentCube != null & currentCube != cubeExecuted) {
							Renderer rend = currentCube.GetComponent<Renderer>();
							rend.material.color = white;
						}
					}
				} else {
					time = 0;
					DeactivateProgressCircle();
					if (currentCube != null & currentCube != cubeExecuted) {
						Renderer rend = currentCube.GetComponent<Renderer>();
						rend.material.color = white;
					}
				}
			}

		}
	}

	/// <summary>
	/// Deactivate the progresscircle.
	/// </summary>
	private void DeactivateProgressCircle() {
		progressCircle.SetActive(false);
	}

	/// <summary>
	/// Increases the value of 'time', activates the gameobject "progresscircle" and starts the progresscircle-animation.
	/// </summary>
	private void IncreaseTimeAndActivateProgessCircle() {
		time += Time.deltaTime;
		progressCircle.SetActive(true);
		animationForProgressCircle.Play();
	}
}
