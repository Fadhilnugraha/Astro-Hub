using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance;
    private DatabaseReference dbRef;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                if (task.Result == DependencyStatus.Available)
                {
                    dbRef = FirebaseDatabase.DefaultInstance.RootReference;
                    Debug.Log("Firebase initialized.");
                }
                else
                {
                    Debug.LogError("Could not resolve Firebase dependencies.");
                }
            });
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Write an article
    public void UploadArticle(Article article)
    {
        string key = dbRef.Child("articles").Push().Key;
        dbRef.Child("articles").Child(key).SetRawJsonValueAsync(JsonUtility.ToJson(article));
    }

    // Read all articles
    public void GetAllArticles(System.Action<List<Article>> callback)
    {
        dbRef.Child("articles").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                List<Article> articles = new List<Article>();

                foreach (var child in snapshot.Children)
                {
                    string json = child.GetRawJsonValue();
                    Article article = JsonUtility.FromJson<Article>(json);
                    articles.Add(article);
                }

                callback?.Invoke(articles);
            }
        });
    }
}