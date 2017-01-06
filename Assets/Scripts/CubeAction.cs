using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAction : MonoBehaviour {

	public bool canInteract;

	private bool activated = false;
	private GameObject shaderCube;
	private Color startColor;

	// Use this for initialization
	void Start () {
		shaderCube = transform.GetChild(0).gameObject;
		shaderCube.SetActive(false);
		startColor = GetComponent<Renderer>().material.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.C)) {
			if (canInteract == true) {
				shaderCube.SetActive(!activated);

				if (activated == true) {
					activated = false;
				} else {
					activated = true;
				}
			}
		} 
	}

	public void ExecuteCubeAction() {
		// EXECUTE ACTION
	}

	void OnMouseEnter() {
		if (User.gazeControlMode == false) {
			Debug.Log("A1");
			GetComponent<Renderer>().material.color = Color.magenta;
		}
	}

	void OnMouseExit() {
		if (User.gazeControlMode == false) {
			Debug.Log("B1");
			GetComponent<Renderer>().material.color = startColor;
		}
	}

	void OnMouseDown() {
		GetComponent<Renderer>().material.color = Color.green;
	}
}
