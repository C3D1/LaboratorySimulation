using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

/// <summary>
/// Should list all action which our project will have.
/// </summary>
public enum AllActionsToExecute
{
    NothingToDo,
    CloseTheMenuAction,
    HZ_OperationOnAction,
    HZ_ServiceOnAction,
    HZ_InstallationOnAction,
    HZ_QuestionOnAction,
    HZ_Off
}

public class ExecuteAction : MonoBehaviour {

    public AllActionsToExecute actionToExecute;
    public GameObject sensor;

    void Start()
    {
        TextMesh textMesh = GetComponent<TextMesh>();

        switch (actionToExecute)
        {
            case AllActionsToExecute.NothingToDo:
                break;

            case AllActionsToExecute.CloseTheMenuAction:      
                textMesh.text = "Close the Menu";
                break;

            case AllActionsToExecute.HZ_InstallationOnAction:
                textMesh.text = "Turn Installation-Zone on.";
                break;

            case AllActionsToExecute.HZ_OperationOnAction:
                textMesh.text = "Turn Operation-Zone on.";
                break;

            case AllActionsToExecute.HZ_QuestionOnAction:
                textMesh.text = "Turn Question-Zone on.";
                break;

            case AllActionsToExecute.HZ_ServiceOnAction:
                textMesh.text = "Turn Service-Zone on.";
                break;

            case AllActionsToExecute.HZ_Off:
                textMesh.text = "Turn all Zones off.";
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Select the Action which the menupoint should run through.
    /// </summary>
	public void SelectActionToExecute()
    {
        if (actionToExecute != AllActionsToExecute.NothingToDo)
        {
            string actionName = actionToExecute.ToString();
            typeof(ExecuteAction).GetMethod(actionName, BindingFlags.Instance | BindingFlags.NonPublic).Invoke(this , null);
        }
    }

    /// <summary>
    /// Call the Method 'CloseMenu()' from the 'sensor' object.
    /// </summary>
    private void CloseTheMenuAction()
    {
        if (sensor != null)
        {
            sensor.SendMessage("CloseMenu");
        }
    }

    private void HZ_InstallationOnAction()
    {
        Debug.Log("HZ_InstallationOnAction wird ausgeführt");
        GameObject[] handlingZones = GameObject.FindGameObjectsWithTag("HandlingZone");
        foreach (GameObject item in handlingZones)
        {
            HandlingZoneSwitch hzSwitch = item.GetComponent<HandlingZoneSwitch>();
            hzSwitch.SetHZ_Installation();
        }
    }

    private void HZ_OperationOnAction()
    {
        Debug.Log("HZ_InstallationOnAction wird ausgeführt");
        GameObject[] handlingZones = GameObject.FindGameObjectsWithTag("HandlingZone");
        foreach (GameObject item in handlingZones)
        {
            HandlingZoneSwitch hzSwitch = item.GetComponent<HandlingZoneSwitch>();
            hzSwitch.SetHZ_Operation();
        }
    }

    private void HZ_QuestionOnAction()
    {
        Debug.Log("HZ_QuestionOnAction wird ausgeführt");
        GameObject[] handlingZones = GameObject.FindGameObjectsWithTag("HandlingZone");
        foreach (GameObject item in handlingZones)
        {
            HandlingZoneSwitch hzSwitch = item.GetComponent<HandlingZoneSwitch>();
            hzSwitch.SetHZ_Question();
        }
    }

    private void HZ_ServiceOnAction()
    {
        Debug.Log("HZ_ServiceOnAction wird ausgeführt");
        GameObject[] handlingZones = GameObject.FindGameObjectsWithTag("HandlingZone");
        foreach (GameObject item in handlingZones)
        {
            HandlingZoneSwitch hzSwitch = item.GetComponent<HandlingZoneSwitch>();
            hzSwitch.SetHZ_Service();
        }
    }

    private void HZ_Off()
    {
        Debug.Log("HZ_Off wird ausgeführt");
        GameObject[] handlingZones = GameObject.FindGameObjectsWithTag("HandlingZone");
        foreach (GameObject item in handlingZones)
        {
            HandlingZoneSwitch hzSwitch = item.GetComponent<HandlingZoneSwitch>();
            hzSwitch.SetHZ_Off();
        }
    }
}
