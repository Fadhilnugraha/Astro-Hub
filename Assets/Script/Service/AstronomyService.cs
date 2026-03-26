using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
public class AstronomyServices : MonoBehaviour
{
    [Header("backend URL")]
    public string apiUrl = 
    "http://localhost:3000/api/astro/calendar?start=2026-02-01&end=2026-02-28";



void Start()
{
    StartCoroutine(GetAstronomyData());
}

IEnumerator GetAstronomyData()
{
    using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
    {
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("API error:"+request.error);
        }
        else
        {
            Debug.Log("Astronomy Data:");
            Debug.Log(request.downloadHandler.text);

            // nanti parsing ke kalender
        }
    }
}}