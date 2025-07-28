using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ArticledetailUI : MonoBehaviour
{
    
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI contentText;
    void Start()
    {
        var article = ArticleDataTransfer.selectedArticle;
        Debug.Log("Selected Article: " + (article != null ? article.title : "NULL"));
       
        if (article != null)
        {
            titleText.text = article.title;
            contentText.text = article.content;
        }
        else
        {
            titleText.text = "Artikel tidak ditemukan.";
            contentText.text = "";
        }
    }
        public void ButtonGoArtikel()
    {
        SceneManager.LoadScene("Artikel");
    }

}
