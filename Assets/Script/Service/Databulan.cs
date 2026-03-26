using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class AmbilDataBulan : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(AmbilData());
    }

    IEnumerator AmbilData()
    {
        string url = "https://api.timeanddate.com/v2.1/astronomy?object=moon&place=usa/new-york&date=2024-06-01&key=6ymMR7rYnx&secret=yysTDWroTwOOLdU12aU2";

        UnityWebRequest req = UnityWebRequest.Get(url);
        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Gagal ambil data");
        }
        else
        {
            Debug.Log(req.downloadHandler.text);
        }
    }
}
