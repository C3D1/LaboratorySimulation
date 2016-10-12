using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalFunctions : MonoBehaviour {

    private GameObject[] allLamps;
    private bool lampVisibility = false;


	// Use this for initialization
	void Start () {
        if (User.offlinemode == true)
        {
            GameObject avatarPrefab = Resources.Load("FPSController", typeof(GameObject)) as GameObject;
            Instantiate(avatarPrefab);
        }

        allLamps = GameObject.FindGameObjectsWithTag("Lamp");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            foreach (GameObject item in allLamps)
            {
                item.SetActive(lampVisibility);
            }
            SwitchLampVisibility();
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {

        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {

        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {

        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {

        }

        if (Input.GetKeyDown(KeyCode.Keypad5))
        {

        }
    }

    /// <summary>
    /// Set the value of 'switchLampVisibility' to its opposite.
    /// </summary>
    private void SwitchLampVisibility()
    {
        if (lampVisibility == true)
        {
            lampVisibility = false; 
                
        }
        else
        {
            lampVisibility = true;
        }
    }
}
