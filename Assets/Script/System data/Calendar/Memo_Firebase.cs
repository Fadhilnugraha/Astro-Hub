using UnityEngine;
using System.Collections;
using Firebase.Database;
using Firebase.Firestore;
using Firebase.Extensions;
using TMPro;
using Firebase.Storage;
using System.Collections.Generic;
public class Memo_Firebase : MonoBehaviour
{
    public TMP_InputField TitleMemo;
    public TMP_InputField IsiMemo;
    public TMP_InputField Tanggal;

    private FirebaseFirestore db;

    void Start()
    {
        FirebaseFirestore db;
        db = FirebaseFirestore.DefaultInstance;



        string title = TitleMemo.text;
        string content = IsiMemo.text;

        
    }

    public void memo()
    {
        Dictionary<string,object> memodata = new Dictionary<string, object>()
        {
            {"Title",TitleMemo.text},
            {"Isi",IsiMemo.text},
             {"Tanggal",Tanggal.text}
        };

        db.Collection("Memo")
        .AddAsync(memodata)
        .ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Memo berhasil diupload ke Firebase");
            }
            else
            {
                Debug.LogError(task.Exception);
            }
        });


    }

    
}
