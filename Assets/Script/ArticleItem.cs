using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ArticleItem : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    private Article articleData;

    public void SetArticle(Article article)
    {
        articleData = article;
        titleText.text = article.title;

    }

    public void pindahbaca()
    {
        if (articleData == null)
    {
        Debug.LogError("articleData masih NULL sebelum LoadScene");
    }
    else
    {
        Debug.Log("articleData yang diklik: " + articleData.title);
    }

        ArticleDataTransfer.selectedArticle = articleData;
        SceneManager.LoadScene("Article_reader_detaile");
    }
}