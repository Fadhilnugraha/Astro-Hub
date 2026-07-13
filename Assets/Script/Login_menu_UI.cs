using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine.SceneManagement;
public class Login_menu_UI : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");
        }
    }

    public void buttonreg()
    {
        SceneManager.LoadScene("Register_page");
    }
}
