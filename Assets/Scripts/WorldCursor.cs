using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorldCursor : MonoBehaviour
{

    private MeshRenderer meshRenderer;
    private float time;
    private bool justOpened = false;

    public Image loadingCircle;
    private float sensorOpeningDelay;
    private float actionExecuteDelay;

    // Use this for initialization
    void Start()
    {
        meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
        time = 0f;
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
        Vector3 headposition = Camera.main.transform.position;
        Vector3 gazeDirection = Camera.main.transform.forward;

        string cameraName = Camera.main.gameObject.name;

        if (cameraName == "FirstPersonCharacter")
        {
            RaycastHit[] hits;
            bool colliderHit = false;
            hits = Physics.RaycastAll(headposition, gazeDirection, 5f);

            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    RaycastHit hit = hits[i];

                    if (hit.collider.gameObject.tag == "Light")
                    {
                        if (!hit.collider.gameObject.GetComponent<OpenMenu>().IsMenuOpen())
                        {
                            IncreaseTimeAndFillAmount(1);
                            colliderHit = true;
                            ActivateRenderer(hit);
                            if (time > sensorOpeningDelay)
                            {
                                time = 0;
                                justOpened = true;
                                SetFillAmountToZero();
                                hit.collider.gameObject.SendMessage("OpenTheMenu");
                            }
                        }
                    }
                    else if (hit.collider.gameObject.tag == "Action")
                    {
                        if (justOpened == false)
                        {
                            IncreaseTimeAndFillAmount(2);
                            colliderHit = true;
                            ActivateRenderer(hit);
                            if (time > actionExecuteDelay)
                            {
                                time = 0;
                                SetFillAmountToZero();
                                hit.collider.gameObject.SendMessage("SelectActionToExecute");
                            }
                        }
                    }
                }

                if (!colliderHit)
                {
                    meshRenderer.enabled = false;
                    time = 0;
                    SetFillAmountToZero();
                }
            }
            else
            {
                time = 0;
                justOpened = false;
                meshRenderer.enabled = false;
                SetFillAmountToZero();
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
    /// Activates the MeshRenderer of the cursor and set his position to point,
    /// where the Raycast hit the object.
    /// </summary>
    /// <param name="hit"></param>
    private void ActivateRenderer(RaycastHit hit)
    {
        meshRenderer.enabled = true;

        this.transform.position = hit.point;
        this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
    }

    /// <summary>
    /// Reset the fillAmount of 'loadingCircle' to 0.
    /// </summary>
    private void SetFillAmountToZero()
    {
        loadingCircle.fillAmount = 0f;
    }

    /// <summary>
    /// Increases the value of 'time' and the fillAmount of 'loadingCircle'.
    /// </summary>
    private void IncreaseTimeAndFillAmount(int number)
    {
        time += Time.deltaTime;
        if (number == 1)
        {
            loadingCircle.fillAmount += Time.deltaTime * (1 / sensorOpeningDelay);
        }
        else
        {
            loadingCircle.fillAmount += Time.deltaTime * (1 / actionExecuteDelay);
        }
    }
}
