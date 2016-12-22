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
	private bool canNotClick = false;
	private bool firstClick = true;
	

	// Use this for initialization
	void Start() {
		avatar = GameObject.FindGameObjectWithTag("Player");
		rb = GetComponent<Rigidbody>();
		placementZones = GameObject.FindGameObjectsWithTag("PlacementZone");
		GameObject progressCircle = GameObject.FindGameObjectWithTag("ProgressCircle");
		if (progressCircle != null) {
			progressCircle.SetActive(false);
		}
	}

	// Update is called once per frame
	void Update() {
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
		} else {
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

		if (isClicked == true & alreadyPlaced != true) {
			if (Vector3.Distance(transform.position, avatar.transform.position) < Mathf.Infinity) {
				Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

				Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
				transform.position = curPosition;
			}
		} else {

		}

	}

	void FixedUpdate() {
		RaycastHit hit;
		if (alreadyPlaced == false ) {
			Ray ray = new Ray(transform.position, -transform.up);
			if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
				if (hit.transform.gameObject.tag == "PlacementZone") {
					if (allowToPlace == false) {
						allowToPlace = true;						
					}
					GetComponent<Renderer>().material.color = Color.green;
					canNotClick = false;
				} else {
					GetComponent<Renderer>().material.color = Color.gray;
					allowToPlace = false;
					canNotClick = true;
				}
			} 
		}
	}

	void OnMouseDown() {
		isClicked = true;
		if (avatar != null & (canNotClick == false | firstClick == true)) {
			firstClick = false;
			GetComponent<Renderer>().material.color = Color.gray;
			if (Vector3.Distance(transform.position, avatar.transform.position) < 5) {
				screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

				offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
			}
		}

		if (allowToPlace == true && alreadyPlaced != true) {
			alreadyPlaced = true;
		} else {
			alreadyPlaced = false;
		}
	}
}