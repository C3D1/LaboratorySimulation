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

			Vector3 headposition = Camera.main.transform.position;
			Vector3 gazeDirection = Camera.main.transform.forward;

			string cameraName = Camera.main.gameObject.name;

			// Only the camera of the avatar should open a menu.
			if (cameraName == "FirstPersonCharacter") {

				RaycastHit[] hits;
				bool colliderHit = false;
				hits = Physics.RaycastAll(headposition, gazeDirection, 5f); // Every collision in the direction the avatar looks with a distance of 5.

				if (hits.Length > 0) {
					for (int i = 0; i < hits.Length; i++) {
						RaycastHit hit = hits[i]; // Every collider in the direction of the view within the distance of 5f.

						// If the collider is an action and not current one, which just was executed.
						if (hit.collider.gameObject.tag == "InteractionCube") {
							if (currentCube != hit.collider.gameObject | currentCube == null) {
								// If you just opened the menu, you need to look away from it first.
								CubeAction cubeScript = hit.collider.gameObject.GetComponent<CubeAction>();
								if (cubeScript != null && cubeScript.canInteract == true && cubeScript.interactModeActivated == true) {
									IncreaseTimeAndActivateProgessCircle();
									colliderHit = true;
								}
								
								//ActivateRenderer(hit);
								if (time > 1) {
									currentCube = hit.collider.gameObject;
									time = 0;
									DeactivateProgressCircle();
									Renderer rend = currentCube.GetComponent<Renderer>();
									rend.material.color = green;

									if (cubeExecuted != null && cubeExecuted != currentCube) {
										Renderer renderer = cubeExecuted.GetComponent<Renderer>();
										renderer.material.color = white;
									}

									cubeExecuted = hit.collider.gameObject;

									animationForProgressCircle.Stop();

									hit.collider.gameObject.SendMessage("ExecuteCubeAction");
								}
							}
						}
					}
					if (!colliderHit) {
						//meshRenderer.enabled = false;
						time = 0;
						DeactivateProgressCircle();
						if (currentCube != null & currentCube != cubeExecuted) {
							Renderer rend = currentCube.GetComponent<Renderer>();
							rend.material.color = white;
						}
					}
				} else {
					time = 0;
					//meshRenderer.enabled = false;
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
	/// Deactivate the gameobject "progressCircle"
	/// </summary>
	private void DeactivateProgressCircle() {
		progressCircle.SetActive(false);
	}

	/// <summary>
	/// Increases the value of 'time', activates the gameobject "progresscircle" and start the progresscircle-animation.
	/// </summary>
	private void IncreaseTimeAndActivateProgessCircle() {
		time += Time.deltaTime;
		progressCircle.SetActive(true);
		animationForProgressCircle.Play();
	}
}
