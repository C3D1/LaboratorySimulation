using UnityEngine;
using System.Collections;

public class Playername : Bolt.EntityEventListener<IAvatarState>
{

    private Vector3 screenpos;
    private GUIStyle namePlate;
    private Vector3 namePlatePos;


    /// <summary>
    /// Attach the value of User.username to state.Avatarname.
    /// </summary>
    public override void Attached()
    {
        if (entity.isOwner)
        {
            state.AvatarName = User.username;
        }
    }

    /// <summary>
    /// Attach the name of every player on the GUI.
    /// The position of the name is right above each player.
    /// </summary>
    void OnGUI()
    {
        if (User.offlinemode == false)
        {
            Vector3 offset = new Vector3(0, 1, 0);
            screenpos = Camera.main.WorldToScreenPoint(transform.position + offset);
            screenpos.y = Screen.height - screenpos.y;
            GUI.Label(new Rect(screenpos.x - 100, screenpos.y - 20, 200, 20), "Name: " + state.AvatarName); //+ User.username
        }
    }
}
