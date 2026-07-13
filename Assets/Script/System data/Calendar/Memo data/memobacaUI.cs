using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Firebase.Firestore;
using System.Collections.Generic;

public class memobacaUI : MonoBehaviour
{
    public TextMeshProUGUI Titletextmemo;
    public TextMeshProUGUI IsiTextmemo;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Buttonbacktomenu()
    {
        SceneManager.LoadScene("Main meneu");
    }
}
