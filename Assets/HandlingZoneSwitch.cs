using UnityEngine;
using System.Collections;

public class HandlingZoneSwitch : MonoBehaviour {
	public enum tZoneType { Operation, Service, Installation, Question};

 	public tZoneType zoneType = tZoneType.Operation;

	Material HZ_Operation, HZ_Service, HZ_Installation, HZ_Question, HZ_Off;

    // Use this for initialization
    void Start () {
		HZ_Operation = Resources.Load("Zone_Operation", typeof(Material)) as Material;
		HZ_Service = Resources.Load("Zone_Service", typeof(Material)) as Material;
		HZ_Installation = Resources.Load("Zone_Installation", typeof(Material)) as Material;
		HZ_Question = Resources.Load("Zone_Question", typeof(Material)) as Material;
		HZ_Off = Resources.Load("FullTransparent", typeof(Material)) as Material;

		GetComponent<Renderer>().material = HZ_Off;
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SetHZ_Operation();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SetHZ_Service();
        }

		if (Input.GetKeyDown(KeyCode.Alpha3)) {
            SetHZ_Installation();
        }

		if (Input.GetKeyDown(KeyCode.Alpha9)) {
            SetHZ_Question();
        }

		if (Input.GetKeyDown(KeyCode.Alpha0)) {
            SetHZ_Off();
		}
	}


    public void SetHZ_Operation()
    {
        if (zoneType == tZoneType.Operation)
        {
            GetComponent<Renderer>().material = HZ_Operation;
            //Debug.Log(HZ_Indication.name)
        }
        
    }

    public void SetHZ_Service()
    {
        if (zoneType == tZoneType.Service)
        {
            GetComponent<Renderer>().material = HZ_Service;
            //Debug.Log(HZ_Focus.name);
        }
      
    }

    public void SetHZ_Installation()
    {
        if (zoneType == tZoneType.Installation)
        {
            GetComponent<Renderer>().material = HZ_Installation;
            //Debug.Log(HZ_Focus.name);
        }
       
    }

    public void SetHZ_Question()
    {
        if (zoneType == tZoneType.Question)
        {
            GetComponent<Renderer>().material = HZ_Question;
            //Debug.Log(HZ_Focus.name);
        }
        
    }

    public void SetHZ_Off()
    {
        GetComponent<Renderer>().material = HZ_Off;
        //Debug.Log(HZ_Off.name);
    }
}
