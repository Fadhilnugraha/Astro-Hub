using UnityEngine;
using UnityEngine.UI;

public class ArticleUploader : MonoBehaviour
{
    public InputField titleInput;
    public InputField contentInput;
    public InputField authorInput;

    public void OnUploadButtonClick()
    {
        string title = titleInput.text;
        string content = contentInput.text;
        string author = authorInput.text;

        Article article = new Article(title, content, author);
        FirebaseManager.Instance.UploadArticle(article);
        Debug.Log("Article uploaded");
    }
}