using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
    private SocialManager SM { get { return SocialManager.Instance; } }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        if (!SM.IsLoaded)
        {
            return;
        }

        if (SM.SocialData.ContainsKey(SM.UserId))
        {
            var data = SM.SocialData[SM.UserId];

            GUI.Label(new Rect(Screen.width / 2 - 100, 10, Screen.width, 20), string.Format("{0} {1}", data.FirstName, data.LastName));
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height/2, 100, 20), "Invite"))
        {
            SocialManager.InviteFriends();
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2+30, 100, 20), "Post"))
        {
            SocialManager.PostToWall("Сашка красава!)0)");
        }


    }
}
