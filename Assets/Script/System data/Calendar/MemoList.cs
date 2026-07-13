using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;

public class MemoList : MonoBehaviour
{
    //==================================================
    public static MemoList Instance;

    [Header("Prefab")]
    public GameObject memoPrefab;
    public Transform contentParent;

    private FirebaseFirestore db;

    //==================================================
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //==================================================
    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;

        //LoadAllMemo();
    }

    //==================================================
    // LOAD MEMO BERDASARKAN TANGGAL
    //==================================================
    public void LoadMemo(string tanggal)
    {
       Debug.Log("========== LOAD MEMO ==========");
       Debug.Log("Tanggal dicari : " + tanggal);

        //Hapus prefab lama
       foreach (Transform child in contentParent)
       {
           Destroy(child.gameObject);
       }

       db.Collection("Memo")
       .WhereEqualTo("Date", tanggal)
       //.OrderBy("CreatedAt")
       .GetSnapshotAsync()
       .ContinueWithOnMainThread(task =>
       {
           Debug.Log("query terpanggil");
           if (task.IsFaulted)
           {
               Debug.Log("Query gagal");
               foreach(var e in task.Exception.InnerExceptions)
               {
                   Debug.LogError(e.Message);
               }
               return;
           }

           if (task.IsCanceled)
           {
               Debug.LogError("Query dibatalkan");
               return;
           }

           QuerySnapshot snapshot = task.Result;

           Debug.Log("Jumlah Memo : " + snapshot.Count);

            foreach (DocumentSnapshot doc in snapshot.Documents)
            {
              
               Debug.Log("Document ditemukan : " + doc.Id);
               Debug.Log("Jumlah Memo = " + snapshot.Count);

               Debug.Log("Prefab dibuat");

               GameObject obj =
                   Instantiate(memoPrefab, contentParent);

               MemoItem item =
                   obj.GetComponent<MemoItem>();

               if (item == null)
               {
                   Debug.LogError("MemoItem tidak ada pada prefab.");
                   continue;
               }

               
               string title = "";
               string content = "";
               string date = "";
               string author = "";

               if (doc.ContainsField("Title"))
                   title = doc.GetValue<string>("Title");

               if (doc.ContainsField("Content"))
                   content = doc.GetValue<string>("Content");

               if (doc.ContainsField("Date"))
                   date = doc.GetValue<string>("Date");

               if (doc.ContainsField("AuthorName"))
                   author = doc.GetValue<string>("AuthorName");

  
               Debug.Log("Title   : " + title);
               Debug.Log("Content : " + content);
               Debug.Log("Date    : " + date);
               Debug.Log("Author  : " + author);

               item.SetData(
                   doc.Id,
                   date,
                   title,
                   content,
                   author
               );
           }

           Debug.Log("========== SELESAI LOAD ==========");
       });
   }
//    public void LoadAllMemo()
//    {
//     foreach (Transform child in contentParent)
//     {
//        // Destroy(child.gameObject);
//     }

//     db.Collection("Memo")
//       .OrderBy("CreatedAt")
//       .GetSnapshotAsync()
//       .ContinueWithOnMainThread(task =>
//       {
//           if(task.IsCompleted)
//           {
//             QuerySnapshot snapshot = task.Result;
//             Debug.Log("Jumlah memo di Firestore = " + snapshot.Count);
//               foreach(DocumentSnapshot doc in task.Result.Documents)
//               {
//                 Debug.Log("====================================");
//                 Debug.Log("Memo ditemukan : " + doc.Id);
//                   GameObject obj =
//                       Instantiate(memoPrefab, contentParent);
//                       Debug.Log("Prefab dibuat");


//                   MemoItem item = obj.GetComponent<MemoItem>();
//                       Debug.Log("MemoItem : " + (item != null));

//                   item.SetData(
//                       doc.Id,
//                       doc.GetValue<string>("Date"),
//                       doc.GetValue<string>("Title"),
//                       doc.GetValue<string>("Content"),
//                       doc.GetValue<string>("AuthorName")
//                   );
//               }
//           }
//       });
// }
}