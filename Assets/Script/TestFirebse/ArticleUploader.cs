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

public class ArticleUploader : MonoBehaviour
{
    public TMP_InputField TitleInput;
    public TMP_InputField contentInput;
    public TMP_InputField authorInput;

    private FirebaseFirestore db;
    private FirebaseStorage storage;

    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        storage = FirebaseStorage.DefaultInstance;
    }
    public void OnUploadButtonClick()
    {
        string title = TitleInput.text;
        string content = contentInput.text;
        string author = authorInput.text;

        Dictionary<string,object> articleData= new Dictionary<string, object>()
        {
            {"Judul",title},
            {"Isi artikel",content},
            {"Jenis artikel","edukasi"},
            {"AuthorId", author},
            {"Status artikel","draft"},
            {"createdAt",Timestamp.GetCurrentTimestamp()}
        };

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
        
        
    }

    
    public void Buttonbacktomenu()
    {
        SceneManager.LoadScene("Main menu");
    }

}