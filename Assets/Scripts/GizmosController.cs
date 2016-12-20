using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class GizmosController : MonoBehaviour {

	private Vector3 screenPoint;
	private Vector3 offset;
	private GameObject avatar;

	// Use this for initialization
	void Start() {
		avatar = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update() {

	}

	void OnMouseDown() {
		if (avatar != null) {
			if (Vector3.Distance(transform.position, avatar.transform.position) < 3) {
				Debug.Log("A1");
				screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

				offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
			}
		}
	}

	void OnMouseDrag() {
		if (Vector3.Distance(transform.position, avatar.transform.position) < 3) {
			Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

			Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
			transform.position = curPosition;
		}
	}
}