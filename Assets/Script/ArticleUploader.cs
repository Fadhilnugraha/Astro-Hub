using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Storage;

public class ArticleUploader : MonoBehaviour
{
    public InputField titleInput;
    public InputField contentInput;
    public Text statusText;
    private DatabaseReference dbRef;
    private FirebaseStorage storage;
    private StorageReference storageRef;

    private string imagePath;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                dbRef = FirebaseDatabase.DefaultInstance.RootReference;
                storage = FirebaseStorage.DefaultInstance;
                storageRef = storage.GetReferenceFromUrl("gs://<project-id>.appspot.com"); // Ganti <project-id>
            }
            else
            {
                Debug.LogError("Firebase tidak tersedia");
            }
        });
    }

    public void PickImage()
    {
        #if UNITY_EDITOR
        imagePath = UnityEditor.EditorUtility.OpenFilePanel("Pilih Gambar", "", "png,jpg,jpeg");
        statusText.text = "Gambar dipilih: " + imagePath;
        #else
        statusText.text = "File picker hanya tersedia di editor.";
        #endif
    }

    public void SubmitArticle()
    {
        if (string.IsNullOrEmpty(titleInput.text) || string.IsNullOrEmpty(contentInput.text) || string.IsNullOrEmpty(imagePath))
        {
            statusText.text = "Isi semua field termasuk gambar.";
            return;
        }

        UploadImageAndSaveArticle(titleInput.text, contentInput.text, imagePath);
    }

    private void UploadImageAndSaveArticle(string title, string content, string path)
    {
        string fileName = Path.GetFileName(path);
        StorageReference imgRef = storageRef.Child("article_images/" + fileName);

        imgRef.PutFileAsync(path).ContinueWithOnMainThread(uploadTask =>
        {
            if (uploadTask.IsFaulted || uploadTask.IsCanceled)
            {
                statusText.text = "Gagal upload gambar.";
                return;
            }

            imgRef.GetDownloadUrlAsync().ContinueWithOnMainThread(urlTask =>
            {
                if (!urlTask.IsCompletedSuccessfully)
                {
                    statusText.text = "Gagal mendapatkan URL gambar.";
                    return;
                }

                string imageUrl = urlTask.Result.ToString();
                SaveArticleToDatabase(title, content, imageUrl);
            });
        });
    }

    private void SaveArticleToDatabase(string title, string content, string imageUrl)
    {
        string key = dbRef.Child("articles").Push().Key;
        ArticleData article = new ArticleData(title, content, imageUrl);
        string json = JsonUtility.ToJson(article);
        dbRef.Child("articles").Child(key).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                statusText.text = "Artikel berhasil dikirim!";
                ClearForm();
            }
            else
            {
                statusText.text = "Gagal menyimpan artikel.";
            }
        });
    }

    private void ClearForm()
    {
        titleInput.text = "";
        contentInput.text = "";
        imagePath = "";
    }
}

[Serializable]
public class ArticleData
{
    public string title;
    public string content;
    public string imageUrl;

    public ArticleData(string title, string content, string imageUrl)
    {
        this.title = title;
        this.content = content;
        this.imageUrl = imageUrl;
    }
}
