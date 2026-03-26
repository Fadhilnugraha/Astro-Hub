using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ArticleDetailReader : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI contentText;
    public TextMeshProUGUI authorText;
    void Start()
    {
        Dictionary<string, object> data = ArticleDataTransfer.selectedArticleDict;
        if (data == null)
        {
            Debug.LogError("Tidak ditemukan data artikel");
            return;
        }
        titleText.text=data.ContainsKey("Judul")? data["judul"].ToString():"";
        contentText.text=data.ContainsKey("IsiArtikel")? data["IsiArtikel"].ToString():"";
        titleText.text=data.ContainsKey("AuthorId")?data["AuthorId"].ToString():"";


    }

    public void Buttonbacktomenu()
    {
        SceneManager.LoadScene("Main menu");
    }
}
