using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MemoItem : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text tanggal;
    public TMP_Text title;

    public TMP_Text content;
    public TMP_Text author;

    private string documentID;
    private string fullisi;


    //=================================================
    public void SetData(
        string id,
        string tgl,
        string judul,
        string isi,
        string pembuat)
    {

        documentID = id;
        fullisi = isi;

        tanggal.text = tgl;
        title.text = judul;
        //content.text = isi;

        if (content != null)
        {
            
            string preMemo = string.IsNullOrEmpty(isi)?"":
            (isi.Length>40 ? isi.Substring(0,40)+"...":isi);
            content.text = preMemo;
        }
        author.text = pembuat;
    }

    //=================================================
    //=================================================
    public void Setup(MemoData memo)
    {
        documentID = "";

        tanggal.text = memo.Date;
        title.text = memo.Title;
        content.text = memo.Content;
        author.text = memo.AuthorName;
    }

    //=================================================
    // TOMBOL PREFAB
    //=================================================
    public void OnClick()
    {
    Debug.Log("Document ID : " + documentID);
    MemoHolder.Instance.Title = title.text;
    MemoHolder.Instance.Content = fullisi;
    MemoHolder.Instance.Date = tanggal.text;
    MemoHolder.Instance.Author = author.text;
    SceneManager.LoadScene("Detail Memo");

        // MemoUI.Instance.ShowDetail(documentID);
    }
}