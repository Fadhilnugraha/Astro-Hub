using UnityEngine;
using TMPro;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine.UI;
using System.Collections.Generic;

public class ArticleReader : MonoBehaviour
{
    public Transform contentPanel; 
    public GameObject articleItemPrefab; // UI prefab to display one article

    

    private FirebaseFirestore db;
    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        LoadArticles();
    }

    void LoadArticles()
    {
    db.Collection("Articles").GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                QuerySnapshot snapshots=task.Result;
                //GameObject item = Instantiate(articleItemPrefab, contentPanel);
                //ArticleItem itemScript = item.GetComponent<ArticleItem>();

                foreach(DocumentSnapshot doc in snapshots.Documents)
                {
                    Dictionary<string,object> data= doc.ToDictionary();

                    GameObject item = Instantiate(articleItemPrefab, contentPanel);
                    ArticleItem itemScript = item.GetComponent<ArticleItem>();

                    if (itemScript != null)
                    {
                        itemScript.SetArticle(data);
                    }


                }

            }
            else
            {
                Debug.LogError("gagal mengambil artikel:"+task.Exception);
            }
        });
    }

    
}