using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class OpenMenu : MonoBehaviour {

    private bool menuOpen = false;

    public GameObject floatingMenu;
    public List<GameObject> menu;

	// Use this for initialization
	void Start () {
        // Set the floating menu on inactive.
        floatingMenu.SetActive(false);
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
                foreach (GameObject item in lights)
                {
                    OpenMenu openMenuScript = item.GetComponent<OpenMenu>();
                    if (openMenuScript != null)
                    {
                        openMenuScript.CloseMenu();
                    }
                }
            }

            floatingMenu.SetActive(true);

            menuOpen = true;

            // Start the animation.
            Animation animation = floatingMenu.GetComponent<Animation>();
            if (animation != null)
            {
                animation.wrapMode = WrapMode.Once;
                animation.Play();
            }

            // Set the position of the menu and the rotation of the component "TextMesh".
            if (floatingMenu != null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");

                Vector3 playerPos = player.transform.position;
                Vector3 playerDirection = player.transform.forward;
                Quaternion playerRotation = player.transform.rotation;
                float spawnDistance = 3.0f;
                Vector3 realposition = new Vector3(playerPos.x, floatingMenu.transform.position.y, playerPos.z);

                Vector3 spawnPos = realposition + playerDirection * spawnDistance;

                floatingMenu.transform.position = spawnPos;
                floatingMenu.transform.rotation = playerRotation;

                foreach (GameObject item in menu)
                {
                    TextMesh textMesh = item.GetComponent<TextMesh>();
                    if (textMesh != null)
                    {
                        textMesh.transform.rotation = playerRotation;
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
