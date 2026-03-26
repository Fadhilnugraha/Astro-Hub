using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

//use for prefab
public class ArticleItem : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI previewText;
    public TextMeshProUGUI authorText;
    private Dictionary<string,object> articleDict;

    public void SetArticle(Dictionary<string, object> data)
    {
        articleDict = data;

        string GetString(string key)
        {
            if (data == null) return"";
            if (data.ContainsKey(key) && data[key] != null)
                return data[key].ToString();
            return"";
        }


        // contoh key Firestore:
        // "Judul" / "judul", "IsiArtikel" / "isiArtikel", "AuthorId" / "authorId"
        // sesuaikan key dengan yang benar di Firestore
        string title = GetString("Judul");
        if (string.IsNullOrEmpty(title)) title = GetString("judul"); // fallback jika pakai lowercase

        string isi = GetString("IsiArtikel");
        if (string.IsNullOrEmpty(isi)) isi = GetString("isiArtikel");
        if (string.IsNullOrEmpty(isi)) isi = GetString("Isi artikel"); // jika masih ada spasi di DB

        string author = GetString("AuthorId");
        if (string.IsNullOrEmpty(author)) author = GetString("authorId");

        // assign ke UI (cek null reference)
        if (titleText != null) titleText.text = title;
        if (previewText != null)
        {
            string preview = string.IsNullOrEmpty(isi) ? "" :
                (isi.Length > 80 ? isi.Substring(0, 80) + "..." : isi);
            previewText.text = preview;
        }
        if (authorText != null) authorText.text = string.IsNullOrEmpty(author) ? "" : ("By " + author);
           
    }

    public void pindahbaca()
    {
        if (articleDict == null)
    {
        Debug.LogError("articleData masih NULL sebelum LoadScene");
        return;
    }
     else
    {
        Debug.Log("articleData yang diklik: " + articleDict);
    }




        ArticleDataTransfer.selectedArticleDict = articleDict;
        SceneManager.LoadScene("Article_reader_detaile");
    }
}