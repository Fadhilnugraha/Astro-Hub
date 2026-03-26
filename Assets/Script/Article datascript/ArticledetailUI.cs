using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Firebase.Firestore;
using UnityEngine.SceneManagement;

public class ArticledetailUI : MonoBehaviour
{
    
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI contentText;
    void Start()
    {
        Dictionary<string, object> dict = ArticleDataTransfer.selectedArticleDict;
        //var Dict = ArticleDataTransfer.selectedArticleDict;
        //Debug.Log("Selected Article: " + (Dict != null ? Dict.Judul : "NULL"));
       
        //if (dict != null)
        //{
        //titleText.text = dict.Judul;
        //  contentText.text = dict.IsiArtikel;
        //}
        //else
        //{
        // titleText.text = "Artikel tidak ditemukan.";
        //   contentText.text = "";
       // }

       if (dict == null)
        {
            titleText.text="article tidak ditemukan";
            contentText.text= "";
            return;
        }

        string Get(string key)
        {
            return dict.ContainsKey(key) && dict[key] != null ? dict[key].ToString() : "";
        }

        string title = Get("Judul");
        if (string.IsNullOrEmpty(title)) title = Get("judul");

        string isi = Get("IsiArtikel");
        if (string.IsNullOrEmpty(isi)) isi = Get("Isi artikel");
        if (string.IsNullOrEmpty(isi)) isi = Get("isiArtikel");

        titleText.text = title;
        contentText.text = isi;
    }
        public void ButtonGoArtikel()
    {
        SceneManager.LoadScene("Artikel");
    }

}
