using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System;
using UnityEngine.SceneManagement;

/// <summary>
/// Class to save the username for each avatar.
/// </summary>
public class User
{
    static public string username;
    static public bool offlinemode = true;
	static public bool gazeControlMode = true; // Can switch between Gaze Control and Mouse Control.
	static public bool teleportMode = false;
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
    /// If you're a server, it will automatically start the "Station_T_Current" scene.
    /// If you're a server, it will connect you to the server you defined by yourself.
    /// </summary>
    public void EnterTheScene()
    {
       
        User.username = usernameInput.text;
        User.offlinemode = false;
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
        }
    }

    /// <summary>
    /// Method to gets your IP-adress.
    /// </summary>
    /// <returns></returns>
    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("Local IP Address Not Found!");
    }

    /// <summary>
    /// Call the method "EnterTheScene" if the network is running.
    /// </summary>
    public override void BoltStartDone()
    {
        if (BoltNetwork.isRunning)
        {
            EnterTheScene();
        }
    }

    /// <summary>
    /// Check if there were a username selected.
    /// Start a server on your local-IP.
    /// </summary>
    public void StartServer()
    {
        if (usernameInput.text != "")
        {
            BoltLauncher.StartServer(UdpKit.UdpEndPoint.Parse(GetLocalIPAddress() + ":27000"));
        }
        else
        {
            GameObject.FindGameObjectWithTag("ErrorMessage").GetComponent<UnityEngine.UI.Text>().text = "No username selected!";
            Debug.Log("No username (Server)");
        }
    }

    /// <summary>
    /// If everything is filled out (username, port and ipadress),
    /// it starts a client.
    /// </summary>
    public void StartClient()
    {
        if (usernameInput.text != "")
        {
            if (ipAdressInput.text != "" && portInput.text != "")
            {
                BoltLauncher.StartClient();
            }
            else
            {
                GameObject.FindGameObjectWithTag("ErrorMessage").GetComponent<UnityEngine.UI.Text>().text = "No IP-Adress or Port was inserted.";
                Debug.Log("No IP-Adress or Port");
            }
        }
        else
        {
            errorMessage = "No username selected!";
            Debug.Log("No username (Client)");
        }
    }

    /// <summary>
    /// This method starts the offlinemode.
    /// You don't to connect to any network and you're on your own in the scene.
    /// </summary>
    public void StartOfflineMode()
    {
        User.offlinemode = true;
        SceneManager.LoadScene("Station_T_Current");
    }
}
 