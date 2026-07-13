using UnityEngine;
using UnityEngine.SceneManagement;
public class Register_UI : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
