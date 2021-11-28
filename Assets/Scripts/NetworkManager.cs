using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;


public class NetworkManager : MonoBehaviour
{
    [SerializeField] private List<string> _wordList;

    private WordList _weeklyWordList;

    public UnityEvent WordsLoaded;
    public static NetworkManager Instance { get; private set; }

    private void Awake()
    {
        var url = _wordList[0];
        if (PlayerPrefs.GetString("UseAllWords") == "True")
        {
            url = _wordList[0];
        }
        Instance = this;
        StartCoroutine(GetRequest(url, (UnityWebRequest req) =>
        {
            if (req.isNetworkError || req.isHttpError)
            {
                Debug.Log($"{req.error}: {req.downloadHandler.text}");
                _weeklyWordList = null;
            } else
            {
                WordList wordList = JsonConvert.DeserializeObject<WordList>(req.downloadHandler.text);
                _weeklyWordList = wordList;
                WordsLoaded.Invoke();
            }
        }));
    }
    
    IEnumerator GetRequest(string url, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            // Send the request and wait for a response
            yield return request.SendWebRequest();
            callback(request);
        }
    }
    
    public WordList GetWeeklyWords()
    {
        return _weeklyWordList;
    }
}
