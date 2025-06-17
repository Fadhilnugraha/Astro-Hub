using UnityEngine;
using System;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Storage;
using Firebase.Auth;
using Google.MiniJSON;

[Serializable]

public class logindata
{
    public string Email;
    public string password;
    
}
public class Logindata : MonoBehaviour
{
    public logindata dts;
    public string userID;
    DatabaseReference dbRef;

    private void Awake()
    {

        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void login()
    {
        string json = JsonUtility.ToJson(dts);
        dbRef.Child("Users").Child(userID).SetRawJsonValueAsync(json);

    }
}
