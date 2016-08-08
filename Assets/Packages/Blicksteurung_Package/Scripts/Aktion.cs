using UnityEngine;
using System.Collections;

public class Aktion : MonoBehaviour
{

    // Use this for initialization
    void Start(){ }

    // Update is called once per frame
    void Update(){ }

    /// <summary>
    /// Execute the Action which is given in the parameter.
    /// </summary>
    /// <param name="actionName"></param>
    public void ExecuteAction(AllActions actionName)
    {
        GameObject[] listOfHandlingZones = GameObject.FindGameObjectsWithTag("HandlingZone");

        switch (actionName)
        {
            case AllActions.HandlingZoneOperation:
                if (listOfHandlingZones != null)
                {
                    foreach (GameObject item in listOfHandlingZones)
                    {
                        HandlingZoneSwitch hzSwitch = item.GetComponent<HandlingZoneSwitch>();
                        hzSwitch.SetHZ_Operation();
                    }
                }
                break;

            case AllActions.HandlingZoneService:
                if (listOfHandlingZones != null)
                {
                    foreach (GameObject item in listOfHandlingZones)
                    {
                        HandlingZoneSwitch hzSwitch = item.GetComponent<HandlingZoneSwitch>();
                        hzSwitch.SetHZ_Service();
                    }
                }
                break;

            case AllActions.HandlingZoneInstallation:
                if (listOfHandlingZones != null)
                {
                    foreach (GameObject item in listOfHandlingZones)
                    {
                        HandlingZoneSwitch hzSwitch = item.GetComponent<HandlingZoneSwitch>();
                        hzSwitch.SetHZ_Installation();
                    }
                }
                break;

            case AllActions.HandlingZoneQuestion:
                if (listOfHandlingZones != null)
                {
                    foreach (GameObject item in listOfHandlingZones)
                    {
                        HandlingZoneSwitch hzSwitch = item.GetComponent<HandlingZoneSwitch>();
                        hzSwitch.SetHZ_Question();
                    }
                }
                break;

            case AllActions.HandlingZoneOff:
                if (listOfHandlingZones != null)
                {
                    foreach (GameObject item in listOfHandlingZones)
                    {
                        HandlingZoneSwitch hzSwitch = item.GetComponent<HandlingZoneSwitch>();
                        hzSwitch.SetHZ_Off();
                    }
                }
                break;

            default:
                break;
        }
    }
}
