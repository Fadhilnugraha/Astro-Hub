using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Database;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Firebase.Storage;
using Firebase.Auth;

public class ArticleUploader : MonoBehaviour
{
    public TMP_InputField TitleInput;
    public TMP_InputField contentInput;
    public TMP_InputField authorInput;

    private FirebaseFirestore db;
    //private FirebaseStorage storage;

    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        //storage = FirebaseStorage.DefaultInstance;
    }
    public void OnUploadButtonClick()
    {
        string title = TitleInput.text;
        string content = contentInput.text;
        //string author = authorInput.text;

        FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;

        if (user == null)
        {
            Debug.LogError("User belum login");
            return;
        }

        string userId = user.UserId;

        db.Collection("User").Document(userId).GetSnapshotAsync().ContinueWithOnMainThread(userTask =>
        {
            if (!userTask.Result.Exists)
            {
                Debug.LogError("Data user tidak ditemukan!");
                return;
            }

            string nama = userTask.Result.GetValue<string>("nama");

                        Dictionary<string, object> articleData = new Dictionary<string, object>()
            {
                {"Judul", title},
                {"Isi artikel", content},
                {"Jenis artikel", "edukasi"},
                {"AuthorId", userId},       // relasi utama
                {"AuthorName", nama},       // biar cepat ditampilkan
                {"Status artikel", "draft"},
                {"createdAt", Timestamp.GetCurrentTimestamp()}
            };

        
        



      //  Dictionary<string,object> articleData= new Dictionary<string, object>()
    // {
    //        {"Judul",title},
    //      {"Isi artikel",content},
    //        {"Jenis artikel","edukasi"},
    //       {"AuthorId", author},
    //        {"AuthorName", nama},
    //        {"Status artikel","draft"},
    //        {"createdAt",Timestamp.GetCurrentTimestamp()}
    //    };

        db.Collection("Articles").AddAsync(articleData).ContinueWithOnMainThread(task=>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Article uploaded");
                SceneManager.LoadScene("Screen_done_upload");
            }
            else
            {
                Debug.LogError(task.Exception);
            }
        });

        });
        
        
    }

    
    public void Buttonbacktomenu()
    {
        SceneManager.LoadScene("Main menu");
    }

}