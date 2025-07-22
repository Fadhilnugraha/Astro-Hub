using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Database;

public class ArticleUploader : MonoBehaviour
{
    public TMP_InputField TitleInput;
    public TMP_InputField contentInput;
    public TMP_InputField authorInput;

    private DatabaseReference dbReference;

    void Start()
    {
        dbReference = FirebaseDatabase.GetInstance("https://astro-hub-3b4c9-default-rtdb.asia-southeast1.firebasedatabase.app/").RootReference;
    }

    public void OnUploadButtonClick()
    {
        string title = TitleInput.text;
        string content = contentInput.text;
        string author = authorInput.text;

        Article article = new Article(title, content, author);
        FirebaseManager.Instance.UploadArticle(article);
        Debug.Log("Article uploaded");
    }
}