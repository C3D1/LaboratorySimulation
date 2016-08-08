using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LampeEinschalten : MonoBehaviour {
	// Private Attributes
	private float time; // Timer für die Dauer die man die Lampe angeschaut hat.
	private bool first = false; // Damit man das Menu nur Öffnen kann, wenn es geschlossen ist.
	private bool isFirstMenuActivation = true;
	private Color colSwitchedOff;
	private bool animWasPlayed = false; // Damit die Animation nur abgespielt wird, wenn das Menu geschlossen ist.
    private Image loadingCircle; // Für die Animation während des Anzeigeprozesses

    private List<int> avatarBerechtigungen;

	//Public Attributes
	public GameObject floatingMenu;
	public float openingDelay;

	public List<GameObject> menu;
	public List<string> actions;

	// Use this for initialization
	void Start() {
		Renderer rend = GetComponent<Renderer>();
		colSwitchedOff = rend.material.color;
		GameObject uiItemsContainer = GameObject.FindGameObjectWithTag("UI Item Container");
		ApplyDelay applyDelayScript = uiItemsContainer.GetComponent<ApplyDelay>();

        if (loadingCircle == null)
        {
            //loadingCircle = Resources.Load("LoadingCircle", typeof(Image)) as Image;
            GameObject loadingCircleObject = GameObject.FindGameObjectWithTag("LoadingCircle");
            loadingCircle = loadingCircleObject.GetComponent<Image>();
        }

        foreach (GameObject item in menu) {
			applyDelayScript.alleMenupunkte.Add(item);
			item.SetActive(false);
		}
	}



	/// <summary>
	/// Setzt die Variable time auf Null für die Wartezeit.
	/// </summary>
	/// <param name="col"></param>
	void OnTriggerEnter(Collider col) {
		time = 0;
        loadingCircle.fillAmount = 0f;
		first = true;
	}

	/// <summary>
	/// Opens the Menu and place it on the right location.
	/// </summary>
	/// <param name="col"></param>
	void OnTriggerStay(Collider col) {
        // The animation shouldnt show up, when the menu is still open.
        // Check if the Collider is the Object with the tag 'GazeReach. 
        if ((col == GameObject.FindGameObjectWithTag("Gaze").GetComponent<Collider>()) && (animWasPlayed == false)) {
			time += Time.deltaTime;
			if (time > 0.5) {
				// Cant open the menu when it already is.
				if (first == true) {
                    loadingCircle.fillAmount += Time.deltaTime * (1 / openingDelay);
					Renderer rend = GetComponent<Renderer>();
					rend.material.color = Color.yellow;

					// There must be a delay of 0.5 seconds
                    // or looking around would be very annoying.
					if ((time - 0.5) > openingDelay) {
                        loadingCircle.fillAmount = 0f;
						time = 0;
						int counter = 0;

						AvatarBerechtigung avatarBerechtigungScript = col.GetComponent<AvatarBerechtigung>();
						if (avatarBerechtigungScript != null) {
							avatarBerechtigungen = avatarBerechtigungScript.avatarBerechtigungen;
						}

						List<string> actionsWithContent = new List<string>();
						// Only the actions with a designation in 'actions'.
						foreach (string item in actions) {
							if (item != "") {
                                actionsWithContent.Add(item);
							}
						}

						// Close all menus before open the elected one.
						GameObject[] lichter = GameObject.FindGameObjectsWithTag("Licht");
						if (lichter != null) {
							foreach (GameObject item in lichter) {
								LampeEinschalten lampeEinschalten = item.GetComponent<LampeEinschalten>();
								if (lampeEinschalten != null) {
									lampeEinschalten.CloseMenu();
								}
							}
						}
						floatingMenu.SetActive(true);
                        

                        foreach (GameObject item in menu) {
							// The fist cube should be the "foundation stone".
							if (counter == 0) {
								item.SetActive(true);
								counter++;
							} else if (counter <= actionsWithContent.Count) {
								TextMesh textrend = menu[counter].GetComponent<TextMesh>();
								textrend.text = actions[counter - 1];

								Ausfuehren execute = item.GetComponent<Ausfuehren>();
								if (execute != null) {
                                    // Check if the user have the correct authorization for the action.
                                    bool setActive = execute.ActionVisibility(avatarBerechtigungen);
                                    
									if (setActive == true) {
										execute.AktionenFarbeEinstellen();
										item.SetActive(true);
									}
								}
								execute.MenuOffen();
								counter = counter + 1;
							}
						}

                        Animation animation = floatingMenu.GetComponent<Animation>();
                        Debug.Log("A1");
                        if (animation != null)
                        {
                            Debug.Log("A2");
                            Debug.Log(animation.isPlaying);
                            animation.wrapMode = WrapMode.Once;
                            animation.Play();
                            if (animation.isPlaying)
                            {
                                Debug.Log("True");
                            }
                            else
                            {
                                Debug.Log("False");
                            }
                        }

                        // Set the activation Location of the menu.
                        if (isFirstMenuActivation == true && floatingMenu != null) {
							GameObject player = GameObject.FindGameObjectWithTag("Player");

                            if (player != null)
                            {
                                Vector3 playerPos = player.transform.position;
                                Vector3 playerDirection = player.transform.forward;
                                Quaternion playerRotation = player.transform.rotation;
                                float spawnDistance = 3.0f;
                                Vector3 realposition = new Vector3(playerPos.x, floatingMenu.transform.position.y, playerPos.z);

                                Vector3 spawnPos = realposition + playerDirection * spawnDistance;

                                floatingMenu.transform.position = spawnPos;
                                floatingMenu.transform.rotation = playerRotation;

                                foreach (GameObject item in menu)
                                {
                                    TextMesh textMesh = item.GetComponent<TextMesh>();
                                    if (textMesh != null)
                                    {
                                        textMesh.transform.rotation = playerRotation;
                                    }
                                }
                            }

						}

						isFirstMenuActivation = false;
						animWasPlayed = true;
					}
				}
			}
		}
	}

	/// <summary>
	/// Set the color back and the same thing with the loadingCircle.
	/// </summary>
	/// <param name="col"></param>
	void OnTriggerExit(Collider col) {
		Renderer rend = GetComponent<Renderer>();
		rend.material.color = colSwitchedOff;
        loadingCircle.fillAmount = 0f;
	}

	/// <summary>
	/// Close the Menu and set the values back to the standard.
	/// </summary>
	/// <returns></returns>
	public void CloseMenu() {
		isFirstMenuActivation = true;
		animWasPlayed = false;
		floatingMenu.SetActive(false);
	}
}
