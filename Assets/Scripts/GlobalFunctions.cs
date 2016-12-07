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
            Instantiate(avatarPrefab);
            avatarPrefab.transform.position.Set(-50f, 0.9f, -3f);
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

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (avatar != null)
            {
                Vector3 position = new Vector3(-3.471f, 0.903f, 5.42f);
                avatar.transform.position = position;
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (avatar != null)
            {
                Vector3 position = new Vector3(-6.561f, 0.903f, 23.209f);
                avatar.transform.position = position;
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (avatar != null)
            {
                Vector3 position = new Vector3(-15.81003f, 0.903f, 1.41f);
                avatar.transform.position = position;
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (avatar != null)
            {
                Vector3 position = new Vector3(-3.318f, 0.903f, 22.09f);
                avatar.transform.position = position;
            }
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
