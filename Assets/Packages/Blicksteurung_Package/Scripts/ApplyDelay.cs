using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ApplyDelay : MonoBehaviour {
	// Private attributes
	private GameObject inputDelayLampe;
	private GameObject inputDelayMenuPunkt;
	private GameObject[] listLampe;
	private InputField inputFieldLampe;
	private InputField inputFieldMenuPunkt;

	// Public attributes
	public List<GameObject> alleMenupunkte;


	// Use this for initialization
	void Start() {
		inputDelayLampe = GameObject.FindGameObjectWithTag("InputDelayLampe");
		inputDelayMenuPunkt = GameObject.FindGameObjectWithTag("InputDelayMenupunkt");
		inputFieldLampe = inputDelayLampe.GetComponent<InputField>();
		inputFieldMenuPunkt = inputDelayMenuPunkt.GetComponent<InputField>();

		listLampe = GameObject.FindGameObjectsWithTag("Licht");
		if (listLampe != null) {
			// Assign the standard values to the delay in every 'LampeEinschalten' script.
			foreach (GameObject item in listLampe) {
				LampeEinschalten scriptLampeEinschalten = item.GetComponent<LampeEinschalten>();

				scriptLampeEinschalten.openingDelay = 0.5f;

			}
		}


		if (alleMenupunkte != null) {
			// Assign the standard values to the delay of every actioncube.
			foreach (GameObject item in alleMenupunkte) {
				Ausfuehren scriptAusfuehren = item.GetComponent<Ausfuehren>();
				CloseMenu scriptCloseMenu = item.GetComponent<CloseMenu>();

				if (scriptAusfuehren != null) {
					scriptAusfuehren.openingDelay = 1.0f;
				} else if (scriptCloseMenu != null) {
					scriptCloseMenu.closingDelay = 1.0f;
				}
			}
		}
	}

	// Update is called once per frame
	void Update() {

	}

	/// <summary>
	/// This is the OnClick()-Method of the 'ApplyDelayButton'.
	/// Take the values, which are definited in 'InputDelayMenuPunkt' and InputDelayLampe'
    /// for the delays.
	/// </summary>
	public void SetDelay() {
		string textInputFieldLampe = inputFieldLampe.text;
		string textInputFieldMenuPunkt = inputFieldMenuPunkt.text;

		if (listLampe != null) {
			foreach (GameObject item in listLampe) {
				item.SetActive(true);
				LampeEinschalten scriptLampeEinschalten = item.GetComponent<LampeEinschalten>();
				if (scriptLampeEinschalten != null) {
					if (textInputFieldLampe == "") {
						scriptLampeEinschalten.openingDelay = 0.5f;
					} else {
						scriptLampeEinschalten.openingDelay = Single.Parse(textInputFieldLampe);
					}
				}
			}
		}

		if (alleMenupunkte != null) {
			foreach (GameObject item in alleMenupunkte) {
				Ausfuehren scriptAusfuehren = item.GetComponent<Ausfuehren>();
				CloseMenu scriptCloseMenu = item.GetComponent<CloseMenu>();
				if (scriptAusfuehren != null) {
					if (textInputFieldMenuPunkt == "") {
						scriptAusfuehren.openingDelay = 1.0f;
					} else {
						scriptAusfuehren.openingDelay = Single.Parse(textInputFieldMenuPunkt);
					}
				} else if (scriptCloseMenu != null) {
					if (textInputFieldMenuPunkt == "") {
						scriptCloseMenu.closingDelay = 1.0f;
					} else {
						scriptCloseMenu.closingDelay = Single.Parse(textInputFieldMenuPunkt);
					}
				}
			}
		}
	}
}
