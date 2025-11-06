using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Firebase.Extensions;
using TMPro;
using System.Data.Common;
using UnityEngine.SceneManagement;

public class Logindata : MonoBehaviour
{
    [Header("Register")]
    public TMP_InputField registerEmailInput;
    public TMP_InputField registerPasswordInput;

    [Header("Login")]
    public TMP_InputField loginEmailInput;
    public TMP_InputField loginPasswordInput;

    [Header("Status")]
    public Text statusText;

    private FirebaseAuth auth;
    private FirebaseUser user;

    void Start()
    {
        FirebaseFirestore db; 
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            auth = FirebaseAuth.DefaultInstance;
            db = FirebaseFirestore.DefaultInstance;
            statusText.text = "Firebase initialized.";
        });
    }

    // Call Register Button
    public void RegisterUser()
    {
        string email = registerEmailInput.text;
        string password = registerPasswordInput.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                statusText.text = "Register failed: " + task.Exception?.Flatten().InnerExceptions[0].Message;
                Debug.Log("Register Failed");
                return;
            }
            else
            {

                //user = task.Result;
                statusText.text = "Register successful! User: " + user.Email;
                SceneManager.LoadScene("Main menu");
                Debug.Log("register success");
            }

        });
       
            
    }

    // Call Login Button
    public void LoginUser()
    {
        string email = loginEmailInput.text;
        string password = loginPasswordInput.text;

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                statusText.text = "Login failed: " + task.Exception?.Flatten().InnerExceptions[0].Message;
                Debug.Log("Login Failed");
                return;
            }
            else
            {

                //user = task.Result;
                statusText.text = "Login successful! Welcome: " + user.Email;
                SceneManager.LoadScene("Main menu");
                Debug.Log("Login Success");
            }
        });
    }
}