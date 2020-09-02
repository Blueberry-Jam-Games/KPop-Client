using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

    public int puzzleID;
    public PictureComponent aSide;
    public PictureComponent bSide;
    public string path = "http://127.0.0.1:5000/api-puzzle";
    public string submission = "http://127.0.0.1:5000/api-response";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetPuzzleID());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetPuzzleID()
    {
        //Debug.Log("Preparing to send request");
        UnityWebRequest www = UnityWebRequest.Get(path);
        //Debug.Log("Yeilding to web request");
        Debug.Log("Getting puzzle id");
        yield return www.SendWebRequest();
        //Debug.Log("Returned form yield");
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Got puzzle data");
            Debug.Log(www.downloadHandler.text);
            string[] obs = www.downloadHandler.text.Split(',');
            string id = obs[0].Split('=')[1];
            puzzleID = int.Parse(id);
            aSide.LoadTexture(puzzleID);
            bSide.LoadTexture(puzzleID);
        }
    }

    public void SamePressed()
    {
        StartCoroutine(SubmitResponse("s"));
    }

    public void DifferentPressed()
    {
        StartCoroutine(SubmitResponse("d"));
    }

    IEnumerator SubmitResponse(string same)
    {
        //Debug.Log("Preparing to send request");
        string response = submission + "?id=" + puzzleID + "&same=" + same;
        UnityWebRequest www = UnityWebRequest.Get(response);
        //Debug.Log("Yeilding to web request");
        Debug.Log("Submitting response to");
        yield return www.SendWebRequest();
        //Debug.Log("Returned form yield");
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Submitted answer");
            //nothing
        }
        Reload();
    }

    private void Reload()
    {
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }
}
