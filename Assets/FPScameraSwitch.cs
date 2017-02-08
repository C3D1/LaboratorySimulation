using UnityEngine;
using System.Collections;

public class FPScameraSwitch : Bolt.EntityEventListener<IAvatarState> {
	public Camera fpsCamera;
	public bool cameraActive = true;
	public bool cursorToggleAllowed = true;

    private bool togglePressed = false;

	// Use this for initialization
	void Start () {
        if (User.offlinemode != true)
        {
            cameraActive = true;
            if (entity.isOwner)
            {
                fpsCamera.GetComponent<Camera>().enabled = true;
            }
            else
            {
                fpsCamera.GetComponent<Camera>().enabled = false;
            }
        }
        else
        {
            fpsCamera.GetComponent<Camera>().enabled = true;
        }
	}


	private void OnEnable() {
		if (cursorToggleAllowed) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}

	// Update is called once per frame
	void Update () {
        if (User.offlinemode == true)
        {
            Camera lCamera;
            Vector3 startPos;

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                cameraActive = false;
                lCamera = fpsCamera.GetComponent<Camera>();
                lCamera.enabled = false;
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                cameraActive = true;
                lCamera = fpsCamera.GetComponent<Camera>();
                lCamera.enabled = true;
            }

            if (cursorToggleAllowed)
            {
                if (Input.GetKey(KeyCode.Escape))
                {
                    if (!togglePressed)
                    {
                        togglePressed = true;
                        if (Cursor.lockState == CursorLockMode.Locked)
                        {
                            Cursor.lockState = CursorLockMode.None;
                        }
                        else
                        {
                            Cursor.lockState = CursorLockMode.Locked;
                        }
                        Cursor.visible = !Cursor.visible;
                    }
                }
                else togglePressed = false;
            }
            else
            {
                togglePressed = false;
                Cursor.visible = false;
            }
        }
        else
        {
            if (entity.isOwner)
            {
                Camera lCamera;
                Vector3 startPos;

                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    cameraActive = false;
                    lCamera = fpsCamera.GetComponent<Camera>();
                    lCamera.enabled = false;
                }

                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    cameraActive = true;
                    lCamera = fpsCamera.GetComponent<Camera>();
                    lCamera.enabled = true;
                }

                if (cursorToggleAllowed)
                {
                    if (Input.GetKey(KeyCode.Escape))
                    {
                        if (!togglePressed)
                        {
                            togglePressed = true;
                            if (Cursor.lockState == CursorLockMode.Locked)
                            {
                                Cursor.lockState = CursorLockMode.None;
                            }
                            else
                            {
                                Cursor.lockState = CursorLockMode.Locked;
                            }
                            Cursor.visible = !Cursor.visible;
                        }
                    }
                    else togglePressed = false;
                }
                else
                {
                    togglePressed = false;
                    Cursor.visible = false;
                }
            }
        }
    }
}
