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
    private bool disabledSchalten = false;
    private Image loadingCircle; // Für die Animation während des Anzeigeprozesses    

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
        bool aktionWirdAusgeführt = ÜberprüfenAktionimGange();
        // Dazu da, damit ständig abgefragt wird, ob die Aktion jetzt ausgeführt wird oder nicht.
        // Das Ganze, weil es möglich ist, dass eine Aktion fertig ausgeführt wurde während man das Menu offen hat.
        if (!aktionWirdAusgeführt)
        {
            if (time == 0f && colorInactive != null)
            {
                Renderer renderer = GetComponent<Renderer>();
                renderer.material.color = colorInactive;
            }
        }
        else
        {
            if (time == 0f && colorInactive != null)
            {
                Renderer renderer = GetComponent<Renderer>();
                renderer.material.color = Color.gray;
            }
        }
    }

    /// <summary>
    /// Setzt die Zeit auf Null.
    /// Dient dazu das eine Aktion nur ausgeführt werden kann, wenn sie nicht bereits ausgeführt wird.
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter(Collider col)
    {
        // Check if the Collider is the Object with the tag 'GazeReach. 
        if (col == GameObject.FindGameObjectWithTag("Gaze").GetComponent<Collider>())
        {
            time = 0;

            Renderer renderer = GetComponent<Renderer>();

            // Enstpricht die Farbe der Aktion nicht grau, wird bereitsausgeführt auf 'false' gesetzt, sonst auf 'true'.
            // Da ein Aktion nur grau ist, wenn sie bereits ausgeführt wird, hat dies auf alle anderen Aktionen keinen Einfluss.
            if (renderer.material.color != Color.gray)
            {
                isExecuting = false;
            }
            else
            {
                isExecuting = true;
            }
            // Setzt den Kreisfüllstand der Animation auf null zurück.
            loadingCircle.fillAmount = 0f;
        }
    }


    /// <summary>
    /// Wechselt die Farbe des Würfels, sobald der Kontakt der beiden Collider mehr als eine Sekunde bestehen bleibt.
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerStay(Collider col)
    {
        // Check if the Collider is the Object with the tag 'GazeReach. 
        if (col == GameObject.FindGameObjectWithTag("Gaze").GetComponent<Collider>())
        {

            // Sobald es der erste Durchlauf ist, der über einer Sekunde ist.
            if (isExecuting != true)
            {
                time += Time.deltaTime;
                loadingCircle.fillAmount += Time.deltaTime * (1 / openingDelay);
                Renderer rend = GetComponent<Renderer>();
                rend.material.color = Color.red;
                // Sobald mehr als eine Sekunde verstrichen ist.
                if (time > openingDelay)
                {
                    loadingCircle.fillAmount = 0f;

                    disabledSchalten = true;

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
                            Färben(); // Jeweilige Aktion die man ausführen will.
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Change the Color back to 'colorInactive'.
    /// Set the values back to the standards.
    /// </summary>manu
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
        Die Methode Färben ist die Aktion an sich. Daher wäre sie auch bei jeder Aktion eine andere Methode.
        Bei diesem Beispiel bewirken aber alle Aktionen mehr oder weniger das gleiche.
    */
    /// <summary>
    /// Färbt die 2.Lampe in die entsprechende Farbe die dem Menupunkt mitgegeben wurde.
    /// </summary>
    private void Färben()
    {
        Color lampenfarbe = individualColorForTheSecondLamp.color;
        GameObject zuFärbendeLampe = GameObject.FindGameObjectWithTag("Licht2");
        Renderer rendLampe = zuFärbendeLampe.GetComponent<Renderer>();
        rendLampe.material.color = lampenfarbe;

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
            if (disabledSchalten == false)
            {
                rend.material.color = colorInactive;
            }
            else
            {
                disabledSchalten = false;
            }
        }
    }

    /// <summary>
    /// Bestimmt, ob ein bestimmtes Item sichtbar ist oder nicht.
    /// </summary>
    /// <param name="avatarwert"></param>
    /// <param name="benötigteWerte"></param>
    /// <returns></returns>
    public bool ActionVisibility(List<int> avatarwerte)
    {
        // Überprüft irgendein Wert in der Liste der Avatarwerte irgendeinem Wert in der Liste der Freigegebenen entspricht.
        foreach (int x in avatarwerte)
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
    /// Stellt die Aktion bei jedem Öffnen des Menus, ausser beim ersten Öffnen, auf die Grundfarbe zurück.
    /// Mit Ausnahe der Aktion die gerade Ausgeführt wurde.
    /// </summary>
    public void AktionenFarbeEinstellen()
    {
        if (individualColorForTheSecondLamp != null && colorInactive != null)
        {
            if (disabledSchalten == false)
            {
                Renderer rend = GetComponent<Renderer>();
                rend.material.color = colorInactive;
            }
            else
            {
                disabledSchalten = false;
            }
        }
    }


    /* 
        Die Methode ÜberprüfenAktionimGange sollte eigentlich in einem eingenen File(Script) platziert werden, da sie je nach Methode anders ist.
        Da das Ganze hier aber etwas anders ist, kann man es gleich im gleichen einheitlichen Script machen.
    */

    /// <summary>
    /// Überprüft ob die Aktion gerade ausgeführt wird.
    /// Falls dies zutrifft, wird 'true' zurückgegeben, sonst 'false'.
    /// </summary>
    /// <returns></returns>
    public bool ÜberprüfenAktionimGange()
    {
        // Normalerweise irgendein Status oder so übeprüfen.
        // In unserem Fall ist es die Farbe der 2.Lampe.
        GameObject zuFärbendeLampe = GameObject.FindGameObjectWithTag("Licht2");
        Renderer rendLamp = zuFärbendeLampe.GetComponent<Renderer>();

        if (rendLamp != null && individualColorForTheSecondLamp != null)
        {
            if (rendLamp.material.color == individualColorForTheSecondLamp.color)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
