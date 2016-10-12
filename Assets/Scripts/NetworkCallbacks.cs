using UnityEngine;
using System.Collections;


[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{

    public override void SceneLoadLocalDone(string map)
    {

        string scene = Application.loadedLevelName;

        // If scene which was loaded not "Login",
        // it creates a new avatar and set his username.
        if (scene != "Login")
        {
            Vector3 pos = new Vector3(-44.51f, 0.964f, -2.364706f);
            GameObject avatar = BoltNetwork.Instantiate(BoltPrefabs.FPSController, pos, Quaternion.identity);
            //TextMesh textmesh = avatar.GetComponentInChildren<TextMesh>();
            //if (textmesh != null)
            //{
            //    textmesh.text = User.username;
            //}
        }

    }
}