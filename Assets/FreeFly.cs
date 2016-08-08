using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

using System.Collections;


//flyspeed is multiplied by the current Axis value, ranging from 0 to 1, thus guaranteeing a smooth transition
///////between being stationary and moving.
//
//NB!!! This script does not include mouse input. To be able to look around with your mouse, add the Mouse Look script from Unity's default camera control scripts!
//
//The E and Q buttons can be used to move up and down with more ease.
///////The movement is done relative to your current rotation.
//This script is built so that it would be very easy to mess around with and improve or change - have a go!
//For using Debug.Log, a function has already been created for you. This should keep things more tidied up.
//Note: Debug text is not shown outside Unity's editor, thus not appearing in a standalone build
//You can toggle between defaultCam and Fly Cam with the default key, F12. The switching is done in the switchCamera function.

/*
////Feel free to use this code for whatever project you might need it for.
////Crediting me is not required.
////Have fun and good luck with your games!
*/

public class FreeFly : MonoBehaviour {
	[SerializeField] private UnityStandardAssets.Characters.FirstPerson.MouseLook mMouseLook;

	public GameObject defaultCam;
    public GameObject playerObject;

	private Camera mCamera;
	float flySpeed = 0.2f;
    bool isEnabled, shift = false, ctrl = false;
    float accelerationAmount = 3f;
    float accelerationRatio = 1f;
    float slowDownRatio = 0.5f;

	// Use this for initialization
	void Start () {
		mCamera = this.GetComponent<Camera>();
		mMouseLook.Init(transform, mCamera.transform);
		isEnabled = true;
        switchCamera(); // switch to the default camera which is the player
    }
	
	// Update is called once per frame
	void Update () {
		if (isEnabled) {
			mMouseLook.LookRotation(transform, mCamera.transform);

			if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) {
				shift = true;
				flySpeed *= accelerationRatio;
			}
			if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift)) {
				shift = false;
				flySpeed /= accelerationRatio;
			}
			if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl)) {
				ctrl = true;
				flySpeed *= slowDownRatio;
			}
			if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl)) {
				ctrl = false;
				flySpeed /= slowDownRatio;
			}
			if (Input.GetAxis("Vertical") != 0) {
				transform.Translate(-defaultCam.transform.forward * flySpeed * Input.GetAxis("Vertical"));
			}
			if (Input.GetAxis("Horizontal") != 0) {
				transform.Translate(-defaultCam.transform.right * flySpeed * Input.GetAxis("Horizontal"));
			}
			if (Input.GetKey(KeyCode.E)) {
				transform.Translate(defaultCam.transform.up * flySpeed * 0.5f);
			} else if (Input.GetKey(KeyCode.Q)) {
				transform.Translate(-defaultCam.transform.up * flySpeed * 0.5f);
			}
		}
        if (Input.GetKeyDown(KeyCode.F12)) { 
            switchCamera();
        }
        if (Input.GetKeyDown(KeyCode.M)) {
            playerObject.transform.position = transform.position; //Moves the player to the flycam's position. Make sure not to just move the player's camera.
        }

    }


    void switchCamera() {
        if (! isEnabled) {   //means it is currently disabled. code will enable the flycam. you can NOT use 'enabled' as boolean's name.
            transform.position = defaultCam.transform.position; //moves the flycam to the defaultcam's position
            defaultCam.GetComponent<Camera>().enabled = false;
            this.GetComponent<Camera>().enabled = true;
            isEnabled = true;
        } else if (isEnabled) {         //if it is not disabled, it must be enabled. the function will disable the freefly camera this time.
            this.GetComponent<Camera>().enabled = false;
            defaultCam.GetComponent<Camera>().enabled = true;
            isEnabled = false;
        }
    }
}
