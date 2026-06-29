using UnityEngine;
using System.Collections;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Firestore;
using TMPro;
using Firebase.Storage;
public class Memo_Firebase : MonoBehaviour
{
    public TMP_InputField TitleMemo;
    public TMP_InputField IsiMemo;
    public TMP_InputField Tanggal;

    private FirebaseStorage db;

    void Start()
    {
        FirebaseFirestore db;
        db = FirebaseFirestore.DefaultInstance;



        string title = TitleMemo.text;
        string content = IsiMemo.text;

        
    }
}
