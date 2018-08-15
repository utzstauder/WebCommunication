using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkScript : MonoBehaviour {

    public static System.Action<HighscoreData> OnHighscoreDataReceived;

    public string uri = "http://www.google.com";

    [ContextMenu("Test")]
	void Start () {
        StartCoroutine(GetText());
	}

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get(uri);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        } else
        {
            // successful
            Debug.Log(www.downloadHandler.text);

            // parse json text
            HighscoreData data = JsonUtility.FromJson<HighscoreData>(www.downloadHandler.text);
            
            //Debug.Log(data.entries);
            //for (int i = 0; i < data.entries.Count; i++)
            //{
            //    Debug.Log(data.entries[i].user + ": " + data.entries[i].score);
            //}

        if (OnHighscoreDataReceived != null)
            {
                OnHighscoreDataReceived(data);
            }
        }
    }
}
