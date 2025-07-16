using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ArticleReader : MonoBehaviour
{
    public Transform contentPanel;
    public GameObject articleItemPrefab; // UI prefab to display one article

    void Start()
    {
        LoadArticles();
    }

    void LoadArticles()
    {
        FirebaseManager.Instance.GetAllArticles((articles) =>
        {
            foreach (var article in articles)
            {
                GameObject item = Instantiate(articleItemPrefab, contentPanel);
                item.GetComponentInChildren<Text>().text = article.title + "\n" + article.author + "\n" + article.content;
            }
        });
    }
}