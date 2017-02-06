using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserPhotoController : MonoBehaviour {
    private SocialManager SM { get { return SocialManager.Instance; } }
    //https://pp.vk.me/c626127/v626127444/3b2ee/XtUK2AuGfFs.jpg


    private void Start()
    {
       
    }
    public void SetImageFromUrl(string url)
    {
        StartCoroutine(LoadImage(url));
    }
    private IEnumerator LoadImage(string loadedURL)
    {
        Texture2D temp = new Texture2D(0, 0);
        WWW www = new WWW(loadedURL);
        yield return www;

        temp = www.texture;
        Sprite sprite = Sprite.Create(temp, new Rect(0, 0, temp.width, temp.height), new Vector2(0.5f, 0.5f));
        GetComponent<Image>().sprite = sprite;
        www.Dispose();
        www = null;
    }
}
