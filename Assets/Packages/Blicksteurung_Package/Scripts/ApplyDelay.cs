using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ApplyDelay : MonoBehaviour
{
    // Private attributes
    private GameObject inputDelaySensor;
    private GameObject inputDelayAction;
    private GameObject[] listLamps;
    private InputField inputFieldSensor;
    private InputField inputFieldAction;
    private GameObject progressCircle;
    private WorldCursor worldCursorScript;


    // Public attributes
    public List<GameObject> alleMenupunkte;


    // Use this for initialization
    void Start()
    {
        inputDelaySensor = GameObject.FindGameObjectWithTag("InputDelaySensor");
        inputDelayAction = GameObject.FindGameObjectWithTag("InputDelayAction");
        inputFieldSensor = inputDelaySensor.GetComponent<InputField>();
        inputFieldAction = inputDelayAction.GetComponent<InputField>();

        progressCircle = GameObject.FindGameObjectWithTag("GlobalObjects");
        worldCursorScript = progressCircle.GetComponent<WorldCursor>();


        worldCursorScript.SetActionExecuteDelay(2f);
        worldCursorScript.SetSensorOpeningDelay(2f);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Alsw");
    }

    /// <summary>
    /// This is the OnClick()-Method of the 'ApplyDelayButton'.
    /// Take the values, which are definited in 'InputDelayAction' and InputDelaySensor'
    /// for the delays.
    /// </summary>
    public void SetDelay()
    {
        string textInputFieldSensor = inputFieldSensor.text;
        string textInputFieldAction = inputFieldAction.text;

        if (textInputFieldSensor == "")
        {
            worldCursorScript.SetSensorOpeningDelay(2f);
        }
        else
        {
            worldCursorScript.SetSensorOpeningDelay(Single.Parse(textInputFieldSensor));
        }

        if (textInputFieldAction == "")
        {
            worldCursorScript.SetActionExecuteDelay(2f);
        }
        else
        {
            worldCursorScript.SetActionExecuteDelay(Single.Parse(textInputFieldSensor));
        }

    }
}
