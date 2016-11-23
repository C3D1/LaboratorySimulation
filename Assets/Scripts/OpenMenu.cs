using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class OpenMenu : MonoBehaviour {

    private bool menuOpen = false;
    private Vector3 originPositionHexMenu;

    public GameObject floatingMenu;
    public List<GameObject> menu;

	// Use this for initialization
	void Start () {
            originPositionHexMenu = floatingMenu.transform.position;

        // Set the floating menu on inactive.
        floatingMenu.SetActive(false);
	}

    void Awake()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// Open the menu and set its position.
    /// Before that, the method calls on every object with the tag
    /// "Light" the CloseMenu() method.
    /// </summary>
    public void OpenTheMenu()
    {
        if (!IsMenuOpen() && menu != null)
        {
            GameObject[] lights = GameObject.FindGameObjectsWithTag("Light");
            if (lights != null)
            {
                // Closes all the menus from each lamp.
                foreach (GameObject item in lights)
                {
                    OpenMenu openMenuScript = item.GetComponent<OpenMenu>();
                    if (openMenuScript != null)
                    {
                        openMenuScript.CloseMenu();
                    }
                }
            }

            // Opens the menu from the selected lamp.
            floatingMenu.SetActive(true);

            menuOpen = true;

            // Start the animation.
            Animation animation = floatingMenu.GetComponent<Animation>();
            if (animation != null)
            {
                animation.wrapMode = WrapMode.Once;
                animation.Play();
            }

            // Set the position and rotation of the menu and aswell the rotation of the component "TextMesh".
            if (floatingMenu != null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");

                Vector3 avatarPos = player.transform.position;
                Vector3 cameraDirection = Camera.main.transform.forward;
                Quaternion cameraRotation = new Quaternion(0, Camera.main.transform.rotation.y, 0, Camera.main.transform.rotation.w);

                float spawnDistance = 3.0f;
                Vector3 realposition = new Vector3(avatarPos.x, originPositionHexMenu.y, avatarPos.z);

                Vector3 spawnPos = new Vector3(realposition.x + cameraDirection.x * spawnDistance, originPositionHexMenu.y, realposition.z + cameraDirection.z * spawnDistance);
                
                floatingMenu.transform.position = spawnPos;
                floatingMenu.transform.rotation = cameraRotation;

                // Set the rotation of the text equal to the rotation of the maincamera. 
                foreach (GameObject item in menu)
                {
                    TextMesh textMesh = item.GetComponent<TextMesh>();
                    if (textMesh != null)
                    {
                        textMesh.transform.rotation = Camera.main.transform.rotation;
                    }
                }
            }
        }     
    }

    /// <summary>
    /// Return the value of 'menuOpen'.
    /// </summary>
    /// <returns></returns>
    public bool IsMenuOpen()
    {
        return menuOpen;
    }

    /// <summary>
    /// Close the menu.
    /// </summary>
    public void CloseMenu()
    {
        floatingMenu.SetActive(false);
        menuOpen = false;
    }
}
