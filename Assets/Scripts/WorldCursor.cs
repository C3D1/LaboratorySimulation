using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class WorldCursor : MonoBehaviour
{

    //private MeshRenderer meshRenderer;
    private float time;
    private bool justOpened = false;
    private GameObject progressCircle; // the progesscirlce, which appears when you're starring on a sensor or action.
    private Animation animationForProgressCircle; // the animation for the progresscircle.
    private GameObject currentAction;
    private float sensorOpeningDelay; // default value is 1 sec.
    private float actionExecuteDelay; // default value is 1 sec.
	private bool teleportMode = false;
	private GameObject[] teleportationBases;

	// Use this for initialization
	void Start()
    {
        //meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
        time = 0f;
		teleportationBases = GameObject.FindGameObjectsWithTag("TeleportBase");
		if (teleportationBases != null) {
			foreach (GameObject item in teleportationBases) {
				item.SetActive(false);
			}
		}    
    }

    // Update is called once per frame
    /// <summary>
    /// Update the position and check if the raycast hit something.
    /// If they raycast hit a sensor and stay on its collider for over a second,
    /// it will open the menu.
    /// If they raycast hit a menupoint and stay on its collider for over a second,
    /// it will call the method to execute the action.
    /// </summary>
    void Update()
    {
        if (progressCircle == null)
        {
            progressCircle = GameObject.FindGameObjectWithTag("ProgressCircle");
            animationForProgressCircle = progressCircle.GetComponentInChildren<Animation>();
            progressCircle.SetActive(false);
        }

		if (teleportationBases != null) {
			if (Input.GetKeyDown(KeyCode.T)) {
				User.teleportMode = !teleportMode;
				if (teleportMode == true) {
					teleportMode = false;
				} else {
					teleportMode = true;
				}
				foreach (GameObject item in teleportationBases) {
					item.SetActive(teleportMode);
				}
			}
		}

        Vector3 headposition = Camera.main.transform.position;
        Vector3 gazeDirection = Camera.main.transform.forward;

        string cameraName = Camera.main.gameObject.name;

        // Only the camera of the avatar should open a menu.
        if (cameraName == "FirstPersonCharacter")
        {
            RaycastHit[] hits;
            bool colliderHit = false;
			if (teleportMode == true) {
				hits = Physics.RaycastAll(headposition, gazeDirection, 12f); // Every collision in the direction the avatar looks with a distance of 7.
			} else {
				hits = Physics.RaycastAll(headposition, gazeDirection, 7f); // Every collision in the direction the avatar looks with a distance of 7.
			}

            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    RaycastHit hit = hits[i]; // Every collider in the direction of the view within the distance of 5f.

                    if (hit.collider.gameObject.tag == "Light")
                    {
                        if (!hit.collider.gameObject.GetComponent<OpenMenu>().IsMenuOpen())
                        {
                            IncreaseTimeAndActivateProgessCircle();
                            colliderHit = true;
                            //ActivateRenderer(hit);
                            if (time > sensorOpeningDelay)
                            {
                                time = 0;
                                justOpened = true;
                                DeactivateProgressCircle();
                                hit.collider.gameObject.SendMessage("OpenTheMenu");
                            }
                        }
                    }
                    // If the collider is an action and not current one, which just was executed.
                    else if ((hit.collider.gameObject.tag == "Action" | hit.collider.gameObject.tag == "ActionClose") & hit.collider.gameObject != currentAction)
                    {
                        // If you just opened the menu, you need to look away from it first.
                        if (justOpened == false)
                        {
                            IncreaseTimeAndActivateProgessCircle();
                            colliderHit = true;
                            //ActivateRenderer(hit);
                            if (time > actionExecuteDelay)
                            {
                                time = 0;
                                DeactivateProgressCircle();

                                // The Close-Action shouldn't be action which you've just executed,
                                // because when you close a menu and open another you should can close it again.
                                if (hit.collider.gameObject.tag == "ActionClose")
                                {
                                    currentAction = null;
                                }
                                else
                                {
                                    currentAction = hit.collider.gameObject;
                                }

                                hit.collider.gameObject.SendMessage("SelectActionToExecute");
                            }
                        }
                    } else if (hit.collider.gameObject.tag == "TeleportBase" && User.teleportMode == true) {
						IncreaseTimeAndActivateProgessCircle();
						colliderHit = true;
						if (time > 1) {
							time = 0;
							DeactivateProgressCircle();

							GameObject avatar = GameObject.FindGameObjectWithTag("Player");
							if (avatar != null) {
								avatar.transform.position = new Vector3(hit.collider.transform.position.x, avatar.transform.position.y, hit.collider.gameObject.transform.position.z);
							}
						}
					} 
                }
                if (!colliderHit)
                {
                    //meshRenderer.enabled = false;
                    time = 0;
                    DeactivateProgressCircle();
                }
            }
            else
            {
                time = 0;
                justOpened = false;
                currentAction = null;
                //meshRenderer.enabled = false;
                DeactivateProgressCircle();
            }
        }
    }

    /// <summary>
    /// Set the value of 'sensorOpeningDelay'.
    /// </summary>
    /// <param name="delay"></param>
    public void SetSensorOpeningDelay(float delay)
    {
        sensorOpeningDelay = delay;
    }

    /// <summary>
    /// Set the value of 'actionExecuteDelay'.
    /// </summary>
    /// <param name="delay"></param>
    public void SetActionExecuteDelay(float delay)
    {
        actionExecuteDelay = delay;
    }


    /// <summary>
    /// Deactivate the gameobject "progressCircle"
    /// </summary>
    private void DeactivateProgressCircle()
    {
        progressCircle.SetActive(false);
    }

    /// <summary>
    /// Increases the value of 'time', activates the gameobject "progresscircle" and start the progresscircle-animation.
    /// </summary>
    private void IncreaseTimeAndActivateProgessCircle()
    {
        time += Time.deltaTime;
        progressCircle.SetActive(true);
        animationForProgressCircle.Play();
    }
}
