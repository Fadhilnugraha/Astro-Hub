using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Firebase.Database;
using UnityEngine.SceneManagement;
public class Login_menu_UI : MonoBehaviour
{

    

    public void buttonreg()
    {
        SceneManager.LoadScene("Register_page");
    }
}
