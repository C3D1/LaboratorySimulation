using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CloseMenu : MonoBehaviour {

	// Private Attributes
	private float time;
	private Color colorInactive;
	private bool preventReclose = true;
	private Image loadingCircle; 

	// Public Attributes
	public GameObject sensor;
	public float closingDelay = 2f;


	// Use this for initialization
	void Start() {
		TextMesh textMesh = GetComponent<TextMesh>();
		if (textMesh != null) {
			textMesh.text = "Close";
		}
		// If no loading circle was set, load the default
		if (loadingCircle == null) {
            GameObject loadingCircleObject = GameObject.FindGameObjectWithTag("LoadingCircle");
            loadingCircle = loadingCircleObject.GetComponent<Image>();
        }

	}

	void Awake() {
		Renderer renderer = GetComponent<Renderer>();
		colorInactive = renderer.material.color;
	}

	// Update is called once per frame
	void Update() { }

	/// <summary>
	/// Set the time on 0 seconds, whenever the collider hits the Cube.
	/// </summary>
	/// <param name="col"></param>
	void OnTriggerEnter(Collider col) {
        // Check if the Collider is the Object with the tag 'GazeReach. 
        if (col == GameObject.FindGameObjectWithTag("Gaze").GetComponent<Collider>()) {
			time = 0;
		}
	}


	/// <summary>
	/// Call the function 'Close()' if the collider rests more then 'closingDelay' on the Cube.
	/// </summary>
	/// <param name="col"></param>
	void OnTriggerStay(Collider col) {
		// Check if the Collider is the Object with the tag 'GazeReach. 
		if (col == GameObject.FindGameObjectWithTag("Gaze").GetComponent<Collider>() && preventReclose == false) {
			time += Time.deltaTime;
			loadingCircle.fillAmount += Time.deltaTime * (1 / closingDelay);
			Renderer rend = GetComponent<Renderer>();
			rend.material.color = Color.red;

			if (time > closingDelay) {
				loadingCircle.fillAmount = 0f;
				time = 0;
				rend.material.color = Color.yellow;
				Close(); 
			}
		}
	}

    /// <summary>
    /// Change the Color back to 'colorInactive'.
    /// Set the values back to the standards.
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerExit(Collider col) {
		Renderer rend = GetComponent<Renderer>();
		Debug.Log("OnTriggerExit");
		rend.material.color = colorInactive;

		loadingCircle.fillAmount = 0f;
		time = 0;
		preventReclose = false;

	}

    /// <summary>
	/// Close the Menu.
	/// </summary>
	private void Close()
    {
        if (sensor != null)
        {
            LampeEinschalten lampMenu = sensor.GetComponent<LampeEinschalten>();
            if (lampMenu != null)
            {
                preventReclose = true;
                lampMenu.CloseMenu();
            }
        }
    }
}
