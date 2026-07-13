using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Memobacadetail : MonoBehaviour
{
    public TextMeshProUGUI Titletextmemo;
    public TextMeshProUGUI IsiTextmemo;
    void Start()
    {
        Titletextmemo.text = MemoHolder.Instance.Title;
        IsiTextmemo.text = MemoHolder.Instance.Content;
    }

    
    public void Buttonbacktomenu()
    {
        SceneManager.LoadScene("Main menu");
    }
}
