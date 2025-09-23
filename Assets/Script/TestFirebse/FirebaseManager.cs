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
                    dbRef = FirebaseDatabase.GetInstance("https://astro-hub-3b4c9-default-rtdb.asia-southeast1.firebasedatabase.app/").RootReference;
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

    // Login data awake
    public void Logindata(Logindata Login)
    {
        string key = dbRef.Child("email").Push().Key;
        //dbRef.Child("email").Child(key).SetRawJsonValueAsync(JsonUtility.ToJson(Login))
        //.ContinueWithOnMainThread(task =>
        {
           // if (as)
        }
    }



    // Write an article
    public void UploadArticle(Article article)
    {
        string key = dbRef.Child("articles").Push().Key;
        dbRef.Child("articles").Child(key).SetRawJsonValueAsync(JsonUtility.ToJson(article))
        .ContinueWithOnMainThread(task =>
        {
            if (task.IsCompletedSuccessfully)
                Debug.Log("Artikel berhasil di-upload.");
            else
                Debug.LogError("Gagal upload: " + task.Exception);
        });
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