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
            // Creating the avatar in the scene.
            GameObject avatarPrefab = Resources.Load("FPSController", typeof(GameObject)) as GameObject;
			avatarPrefab.transform.position = new Vector3(-14.31f, 0.9f, -3.22f);
			Instantiate(avatarPrefab);      
        }

        allLamps = GameObject.FindGameObjectsWithTag("Lamp");

    }
	
	// Update is called once per frame
	void Update () {
        GameObject avatar = GameObject.FindGameObjectWithTag("Player");

        // Places the avatar to a position in the scene.
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            foreach (GameObject item in allLamps)
            {
                item.SetActive(lampVisibility);
            }
            SwitchLampVisibility();
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
