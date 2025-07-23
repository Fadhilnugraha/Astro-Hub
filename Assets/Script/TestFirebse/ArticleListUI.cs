using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class ArticleListUI : MonoBehaviour
{
    public GameObject articleItemPrefab; // drag prefab ini dari Inspector
    public Transform contentParent; // drag "Content" dari Scroll View ke sini

    void Start()
    {
        FirebaseManager.Instance.GetAllArticles(OnArticlesLoaded);
    }

    void OnArticlesLoaded(List<Article> articles)
    {
        foreach (var article in articles)
        {
            GameObject item = Instantiate(articleItemPrefab, contentParent);
            var texts = item.GetComponentsInChildren<TextMeshProUGUI>();

            texts[0].text = article.title;
            texts[1].text = article.content.Length > 15 
                ? article.content.Substring(0, 15) + "..." 
                : article.content;
        }

    }
}
