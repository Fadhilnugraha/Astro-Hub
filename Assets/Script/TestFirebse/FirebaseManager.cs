using Firebase;
using Firebase.Database;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance;
    private DatabaseReference dbRef;
    private FirebaseFirestore firestore;

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
                    firestore = FirebaseFirestore.DefaultInstance;  
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

    public void UploadArticle(Article article)
    {
        firestore.Collection("articles").AddAsync(article)
        .ContinueWithOnMainThread(Task=>
        {
            if(Task.IsCompletedSuccessfully)
            Debug.Log("article berhasil diupload");
            else
            Debug.LogError("Artikel gagal terupload"+Task.Exception);
        });
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


    // Read all articles
    public void GetAllArticles(System.Action<List<Article>> callback)
    {
        
        firestore.Collection("Articles").GetSnapshotAsync()
        .ContinueWithOnMainThread(task =>
        {
            List<Article> articles = new List<Article>();

            if (task.IsCompletedSuccessfully)
            {
                foreach (var doc in task.Result.Documents)
                {
                    Article a = doc.ConvertTo<Article>(); 
                    articles.Add(a);
                }

                callback?.Invoke(articles);
            }
            else
            {
                Debug.LogError("Gagal mengambil artikel: " + task.Exception);
            }
        });
    }
}