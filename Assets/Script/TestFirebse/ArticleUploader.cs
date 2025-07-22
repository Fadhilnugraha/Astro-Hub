using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ArticleUploader : MonoBehaviour
{
    public TMP_InputField TitleInput;
    public TMP_InputField contentInput;
    public TMP_InputField authorInput;

    

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