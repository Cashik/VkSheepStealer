using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialManager : MonoBehaviour {

    public UserPhotoController imageController;
    private static SocialManager _instance;
    public static SocialManager Instance { get { return _instance; } }

    public bool IsLoaded { get; private set; }
    public string UserId { get; private set; }
    public string AuthKey { get; private set; }
    public string Protocol { get; private set; }

    public readonly Dictionary<string, SocialData> SocialData = new Dictionary<string, SocialData>(32);

    private void Awake()
    {
        _instance = this;
        Write("Create Social Manager instance!");
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        //imageController.SetImageFromUrl("https://pp.vk.me/c626127/v626127444/3b2ee/XtUK2AuGfFs.jpg");

        Application.ExternalCall("GetParams");

    }

    public static void Write(string message)
    {
        Debug.Log("SM->" + message);
    }
	// Update is called once per frame
	void Update () {
		
	}

    #region CALLBACK

    public void RecvParams(string a)
    {
        Write("RecvParams: " + a);

        if (a.StartsWith("https"))
            Protocol = "https";
        else
            Protocol = "http";

        a = a.Split('?')[1];

        string[] mas = a.Split('&');
        foreach (string s in mas)
        {
            string[] k = s.Split('=');
            switch (k[0])
            {
                case "viewer_id":
                    UserId = k[1];
                    Write("User id is " + UserId);
                    break;
                case "auth_key":
                    AuthKey = k[1];
                    Write("AuthKey set to " + AuthKey);
                    break;
            }
        }

        GetUserInfo(UserId);

        IsLoaded = true;
    }

    public void OnGetPlayer(string str)
    {
        var mas = str.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        var socialData = new SocialData
        {
            ViewerId = mas[0],
            FirstName = mas[1],
            LastName = mas[2],
            Photo = mas[3]
        };

        if (SocialData.ContainsKey(socialData.ViewerId))
        {
            SocialData[socialData.ViewerId] = socialData;
        }
        else
        {
            SocialData.Add(socialData.ViewerId, socialData);
        }

        imageController.SetImageFromUrl(socialData.Photo);

        Write("OnGetPlayer: " + str);
    }

    public void OnAvatarChange()
    {

    }
    #endregion

    #region VkApi

    public static void PostToWall(string text)
    {
        Application.ExternalCall("PostToWall", text);
    }

    public static void InviteFriends()
    {
        Application.ExternalCall("ShowInvite");
    }

    public static void GetUserInfo(string viewer_id)
    {
        if (Instance.SocialData.ContainsKey(viewer_id))
            return;

        Application.ExternalCall("GetProfile", viewer_id);
    }

    #endregion

}
