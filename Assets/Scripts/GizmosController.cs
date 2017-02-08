using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class GizmosController : MonoBehaviour {

	private Vector3 screenPoint;
	private Vector3 offset;
	private GameObject avatar;
	private bool isClicked = false;
	private GameObject[] placementZones;
	private Rigidbody rb;
	private bool allowToPlace = false;
	private bool alreadyPlaced = false;
	private bool canClick = true;	
	private bool firstClick = true; // The first click on a object when the user starts the scene.
	

	// Use this for initialization
	void Start() {
		avatar = GameObject.FindGameObjectWithTag("Player");
		rb = GetComponent<Rigidbody>();
		placementZones = GameObject.FindGameObjectsWithTag("PlacementZone");
		GameObject progressCircle = GameObject.FindGameObjectWithTag("ProgressCircle");
		if (progressCircle != null) {
			progressCircle.SetActive(false);
		}
		// Deactivates all shaders on the placement zones.
		if (placementZones != null) {
			foreach (GameObject item in placementZones) {
				GameObject shader = item.transform.FindChild("Shader").gameObject;
				if (shader != null) {
					if (shader.active == true) {
						shader.SetActive(false);
					}
				}
			}
		}
	}

	// Update is called once per frame
	void Update() {
		// Activates every shader, if the cube is clicked and being dragged through the scene.
		if (isClicked == true) {
			if (placementZones != null) {
				foreach (GameObject item in placementZones) {
					GameObject shader = item.transform.FindChild("Shader").gameObject;
					if (shader != null) {
						if (shader.active == false) {
							shader.SetActive(true);
						}
					}
				}		
			}
		} else { // If the cube is placed again, the shader are deactivated.
			if (placementZones != null) {
				foreach (GameObject item in placementZones) {
					GameObject shader = item.transform.FindChild("Shader").gameObject;
					if (shader != null) {
						if (shader.active == true) {
							shader.SetActive(false);
						}
					}
				}
			}
		}
		// If the cube is clicked and isn't placed. It will drag the cube with you.
		if (isClicked == true & alreadyPlaced != true) {
			if (Vector3.Distance(transform.position, avatar.transform.position) < Mathf.Infinity) {
				Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

				Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
				transform.position = curPosition;
			}
		}
	}

	/// <summary>
	/// Scans the first object under the cube and if it's a placement zone, you're allowed to place to cube.
	/// </summary>
	void FixedUpdate() {
		RaycastHit hit;
		if (alreadyPlaced == false ) {
			Ray ray = new Ray(transform.position, -transform.up); // Get the first object with collider, which is placed below the cube.
			if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
				// If the object is a placement zone, the user is allowed to place the cube.
				if (hit.transform.gameObject.tag == "PlacementZone") { 
					if (allowToPlace == false) {
						allowToPlace = true;						
					}
					canClick = true;
				} else {
					allowToPlace = false;
					canClick = false;
				}
			} 
		}
	}

	/// <summary>
	/// You begin to drag the object with the first click on it.
	/// If the object isn't already placed on a placement zone, you can drop it on one with a second click.
	/// You can only drop it above green placement zones.
	/// </summary>
	void OnMouseDown() {
		isClicked = true;
		if (avatar != null & (canClick == true | firstClick == true)) {
			firstClick = false;
			GetComponent<Renderer>().material.color = Color.gray;
			if (Vector3.Distance(transform.position, avatar.transform.position) < 5) {
				screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

				offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
			}
		}
		// If the cube is already placed you shouldn't be still able to see the green overlay.
		if (allowToPlace == true && alreadyPlaced != true) {
			isClicked = false;
			alreadyPlaced = true;
		} else {
			alreadyPlaced = false;
		}
	}
}