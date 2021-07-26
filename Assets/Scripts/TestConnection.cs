using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TestConnection : MonoBehaviour
{
    [SerializeField] private Text _text;

    public bool IsConnection;

    private void OnEnable()
    {
        StartCoroutine(GetText());
    }

    private IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://google.com");
        yield return www.SendWebRequest();

        if (www.isNetworkError)
            IsConnection = false;
        else
            IsConnection = true;
    }
}
