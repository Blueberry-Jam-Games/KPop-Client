using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PictureComponent : MonoBehaviour
{
    Image display;
    int index;
    public string side;
    public string root = "http://127.0.0.1:5000/api-image";

    // Start is called before the first frame update
    void Start()
    {
        display = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadTexture(int id)
    {
        index = id;
        StartCoroutine(GetImage());
    }

    IEnumerator GetImage()
    {
        string path = root + "?id=" + index + "&side=" + side;

        Debug.Log("Getting image side " + side + " path = " + path);

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(path);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            display.sprite = Sprite.Create((Texture2D)myTexture, new Rect(0, 0, 512, 512), new Vector2());
        }
    }
}
