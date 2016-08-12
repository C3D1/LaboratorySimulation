using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum AllActions
{
    Nothing,
    HandlingZoneOperation,
    HandlingZoneService,
    HandlingZoneInstallation,
    HandlingZoneQuestion,
    HandlingZoneOff
}


public class Ausfuehren : MonoBehaviour
{
    // Private attributes
    private float time;
    private Color colorInactive;
    private bool isExecuting = false;
    private bool setDisabled = false;
    private Image loadingCircle; // For the animation in the collider progress.

    // Public attributes
    public Material individualColorForTheSecondLamp;
    public AllActions actionsAvaible;
    public float openingDelay;

    public List<int> freigabeBerechtigteStufen;

    // Use this for initialization
    // If no loading circle was set, load the default
    void Start()
    {
        if (loadingCircle == null)
        {
            GameObject loadingCircleObject = GameObject.FindGameObjectWithTag("LoadingCircle");
            loadingCircle = loadingCircleObject.GetComponent<Image>();
        }
    }

    void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        colorInactive = renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Check that an action can only be executed, if she isnt already in progress.
    /// Also reset the time.
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter(Collider col)
    {
        // Check if the Collider is the Object with the tag 'GazeReach. 
        if (col == GameObject.FindGameObjectWithTag("Gaze").GetComponent<Collider>())
        {
            time = 0;

            Renderer renderer = GetComponent<Renderer>();

            if (renderer.material.color != Color.gray)
            {
                isExecuting = false;
            }
            else
            {
                isExecuting = true;
            }
            // Set the fillamount of 'loadingCircle' to zero.
            loadingCircle.fillAmount = 0f;
        }
    }


    /// <summary>
    /// If the collider is triggered for over a second, change the cube its color.
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerStay(Collider col)
    {
        // Check if the Collider is the Object with the tag 'GazeReach. 
        if (col == GameObject.FindGameObjectWithTag("Gaze").GetComponent<Collider>())
        {

            // When its the first pass
            if (isExecuting != true)
            {
                time += Time.deltaTime;
                loadingCircle.fillAmount += Time.deltaTime * (1 / openingDelay);
                Renderer rend = GetComponent<Renderer>();
                rend.material.color = Color.red;
                
                if (time > openingDelay)
                {
                    loadingCircle.fillAmount = 0f;

                    setDisabled = true;

                    time = 0;

                    rend.material.color = Color.yellow;
                    if (actionsAvaible != AllActions.Nothing)
                    {
                        isExecuting = true;
                        Aktion aktionScript = GetComponent<Aktion>();
                        aktionScript.ExecuteAction(actionsAvaible);
                    }
                    else
                    {
                        if (individualColorForTheSecondLamp != null)
                        {
                            Coloring();
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Change the Color back to 'colorInactive'.
    /// Set the values back to the standards.
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerExit(Collider col)
    {
        if (isExecuting != true)
        {
            Renderer rend = GetComponent<Renderer>();
            rend.material.color = colorInactive;

            loadingCircle.fillAmount = 0f;
            time = 0;
        }
    }

    /*
        Coloring is only an example action. Normaly, this action should be in a script for its own.
    */
    /// <summary>
    /// Set the color of the second lamp to the color, which the action has got.
    /// </summary>
    private void Coloring()
    {
        Color lampColor = individualColorForTheSecondLamp.color;
        GameObject lampToColor = GameObject.FindGameObjectWithTag("Licht2");
        Renderer rendLampe = lampToColor.GetComponent<Renderer>();
        rendLampe.material.color = lampColor;

        isExecuting = true;
    }

    /// <summary>
    /// Will be called when the avatar opens the menu.
    /// Should that be the first time of Execution of the script, the variable 'colorInactive' will change.
    /// </summary>
    public void SetColorToDefaultWhenOpeningMenu()
    {
        if (isExecuting == false && colorInactive != null)
        {
            Renderer rend = GetComponent<Renderer>();
            if (setDisabled == false)
            {
                rend.material.color = colorInactive;
            }
            else
            {
                setDisabled = false;
            }
        }
    }

    /// <summary>
    ///  Define, if the action should be visible for this user or not.(permission granted or not)
    /// </summary>
    /// <param name="avatarvalues"></param>
    /// <returns></returns>
    public bool ActionVisibility(List<int> avatarvalues)
    {
        foreach (int x in avatarvalues)
        {
            foreach (int y in freigabeBerechtigteStufen)
            {
                if (x == y)
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Set the color of each menupoint to the default value with each time it will be openend, except the first time.
    /// </summary>
    public void SetActionColor()
    {
        if (individualColorForTheSecondLamp != null && colorInactive != null)
        {
            if (setDisabled == false)
            {
                Renderer rend = GetComponent<Renderer>();
                rend.material.color = colorInactive;
            }
            else
            {
                setDisabled = false;
            }
        }
    }
}
