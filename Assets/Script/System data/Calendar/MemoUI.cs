using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

using Firebase.Firestore;
using Firebase.Extensions;
using Firebase.Auth;

public class MemoUI : MonoBehaviour
{
    public static MemoUI Instance;

    [Header("UI")]
    public GameObject panel;
    public TMP_InputField title;
    public TMP_InputField input;

    [Header("List Memo")]

    public Transform contentParent;

    public GameObject memoPrefab;

    private DateTime currentDate;

    private FirebaseFirestore db;
    private FirebaseAuth auth;



    public GameObject detailPanel;

    public TMP_Text detailTitle;
    public TMP_Text detailDate;
    public TMP_Text detailAuthor;
    public TMP_Text detailContent;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        panel.SetActive(false);

        db = FirebaseFirestore.DefaultInstance;
        auth = FirebaseAuth.DefaultInstance;
    }

    //--------------------------------------------------
    // MEMBUKA MEMO
    //--------------------------------------------------

    public void Open(DateTime date)
    {
        currentDate = date;

        panel.SetActive(true);

        title.text = "";
        input.text = "";

        LoadMemo(date);
    }

    //--------------------------------------------------
    // SAVE MEMO
    //--------------------------------------------------

    public void Save()
{
    MemoData memo = new MemoData();

    memo.Title = title.text;
    memo.Content = input.text;
    memo.Date = currentDate.ToString("yyyy-MM-dd");
    memo.AuthorUID = auth.CurrentUser.UserId;
    memo.AuthorName = auth.CurrentUser.DisplayName;
    memo.CreatedAt = Timestamp.GetCurrentTimestamp();

    db.Collection("Memo")
      .AddAsync(memo)
      .ContinueWithOnMainThread(task =>
      {
          if(task.IsCompleted)
          {
              panel.SetActive(false);

              LoadMemo(currentDate);

              Debug.Log("Memo berhasil disimpan");
          }
      });
}

    //--------------------------------------------------
    // LOAD MEMO BERDASARKAN TANGGAL
    //--------------------------------------------------

    void LoadMemo(DateTime date)
{
    db.Collection("Memo")
    .WhereEqualTo("Date", date.ToString("yyyy-MM-dd"))
    .OrderBy("CreatedAt")
    .GetSnapshotAsync()
    .ContinueWithOnMainThread(task =>
    {
        if(task.IsCompleted)
        {
            foreach(Transform child in contentParent)
            {
                Destroy(child.gameObject);
            }

            QuerySnapshot snapshot = task.Result;

            foreach(DocumentSnapshot doc in snapshot.Documents)
            {
                MemoData memo = doc.ConvertTo<MemoData>();

                GameObject obj =
                    Instantiate(memoPrefab,
                                contentParent);

                obj.GetComponent<MemoItem>()
                   .Setup(memo);
            }
        }
    });
}
//--------------------------------------------------
public void ShowDetail(MemoData memo)
{
    detailPanel.SetActive(true);

    detailTitle.text = memo.Title;

    detailDate.text = memo.Date;

    detailAuthor.text = memo.AuthorName;

    detailContent.text = memo.Content;
}

    //--------------------------------------------------
    // CLOSE
    //--------------------------------------------------

    public void Close()
    {
        panel.SetActive(false);
    }
}