using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Class to save the username for each avatar.
/// </summary>
public class User
{
    static public string username;
}


public class Login : Bolt.GlobalEventListener
{

    private InputField usernameInput;
    private string errorMessage;
    private InputField portInput;
    private InputField ipAdressInput;

    // Use this for initialization
    void Start()
    {
        usernameInput = GameObject.FindGameObjectWithTag("UsernameInput").GetComponent<InputField>();
        errorMessage = GameObject.FindGameObjectWithTag("ErrorMessage").GetComponent<Text>().text;
        ipAdressInput = GameObject.FindGameObjectWithTag("IPAdressInput").GetComponent<InputField>();
        portInput = GameObject.FindGameObjectWithTag("PortInput").GetComponent<InputField>();
    }

    /// <summary>
    /// When you press the button to enter the scene, this methode will be called.
    /// This methode check the validation of the form and connect the client to the right server.
    /// </summary>
    public void EnterTheScene()
    {
        if (usernameInput != null && errorMessage != null)
        {
            string username = usernameInput.text;
            if (username != "")
            {
                User.username = username;
                if (BoltNetwork.isServer)
                    BoltNetwork.LoadScene("Station_T_Current");
                else
                {
                    if (ipAdressInput.text != "" && portInput.text != "")
                    {
                        try
                        {
                            BoltNetwork.Connect(UdpKit.UdpEndPoint.Parse(string.Format("{0}:{1}", ipAdressInput.text, portInput.text)));
                        }
                        catch (System.Exception ex)
                        {
                            errorMessage = string.Format("{0}", ex);
                        }
                    }
                    else
                    {
                        errorMessage = "No IP-Adress or Port was inserted.";
                    }
                }
            }
            else
            {
                errorMessage = "No username selected. Please define a username.";
            }
        }
        else
        {
            if (errorMessage != null)
            {
                errorMessage = "Username InputField is missing.";
            }
        }
    }
}
 