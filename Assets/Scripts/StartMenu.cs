using UnityEngine;
using System.Collections;
using System.Net;
using System;
using System.Net.Sockets;

public class StartMenu : Bolt.GlobalEventListener
{
    /// <summary>
    /// Create 2 Buttons on the GUI. One for the server and the other for the clients.
    /// </summary>
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, Screen.width - 20, Screen.height - 20));

        if (GUILayout.Button("Start Server", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)))
        {
            // START SERVER
            BoltLauncher.StartServer(UdpKit.UdpEndPoint.Parse(GetLocalIPAddress()+ ":27000"));
        }

        if (GUILayout.Button("Start Client", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)))
        {
            // START CLIENT
            BoltLauncher.StartClient();
        }

        GUILayout.EndArea();
    }

    /// <summary>
    /// After you clicked one of the buttons, the scene "Login" will be loaded.
    /// </summary>
    public override void BoltStartDone()
    {
        if (BoltNetwork.isRunning)
        {
            Application.LoadLevel("Login");
        }
    }

    /// <summary>
    /// Method to get te IP-adress of the host.
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
}

