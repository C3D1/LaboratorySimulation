using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowHide : MonoBehaviour {
	public bool isShown = true;
	public GameObject theThing;
    public Image loadingCircle;

	// Use this for initialization
	void Start () {
		theThing.SetActive(isShown);
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.F1)) {
			isShown = !isShown;
			theThing.SetActive(isShown);
            
        }
	}
}
