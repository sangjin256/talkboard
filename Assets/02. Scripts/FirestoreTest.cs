using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FirestoreTest : MonoBehaviour
{
    // private FirebaseFirestore _db;
    //
    // public void Start()
    // {
    //     _db = FirebaseFirestore.DefaultInstance;
    // }
    //
    // public async Task AddPost(Post post)
    // {
    //     // Firestore의 컬렉션 참조
    //     CollectionReference postsRef = _db.Collection("posts");
    //
    //     // 새로운 랜덤 문서 ID 생성
    //     DocumentReference newPostRef = postsRef.Document();
    //
    //     // Firestore에 저장할 Dictionary로 변환
    //     Dictionary<string, object> postData = new Dictionary<string, object>
    //     { 
    //         { "authorEmail", post.AuthorEmail },
    //         { "authorNickname", post.AuthorNickname },
    //         { "content", post.Content },
    //         { "commentCount", 0 },
    //         { "likeCount", 0 },
    //         { "likeUserEmails", new List<string>() },
    //         { "isModified", false },
    //         { "createdAt", Timestamp.FromDateTime(post.CreatedAt.ToUniversalTime()) }
    //     };
    //
    //     // Firestore에 업로드
    //     await newPostRef.SetAsync(postData);
    // }
    //
    // public async Task<List<Post>> GetPostsByEmail(string email)
    // {
    //     List<Post> posts = new List<Post>();
    //
    //     QuerySnapshot snapshot = await _db.Collection("posts")
    //                                     .WhereEqualTo("authorEmail", email)
    //                                     .GetSnapshotAsync();
    //
    //     foreach(DocumentSnapshot doc in snapshot.Documents)
    //     {
    //         Post post = new Post
    //         {
    //             Id = doc.Id,
    //             AuthorEmail = doc.GetValue<string>("authorEmail"),
    //             AuthorNickname = doc.GetValue<string>("authorNickname"),
    //             Content = doc.GetValue<string>("content"),
    //             CommentCount = doc.GetValue<int>("commentCount"),
    //             LikeCount = doc.GetValue<int>("likeCount"),
    //             LikeUserEmails = new HashSet<string>(doc.GetValue<List<string>>("likeUserEmails")),
    //             IsModified = doc.GetValue<bool>("isModified"),
    //             CreatedAt = doc.GetValue<Timestamp>("createdAt").ToDateTime()
    //         };
    //
    //         posts.Add(post);
    //     }
    //
    //     return posts;
    // }
    //
    // public async void OnClickAddPostButton()
    // {
    //     Post newPost = new Post
    //     {
    //         AuthorEmail = "userUid@a.a",
    //         AuthorNickname = "이상진",
    //         Content = "이 게임 진짜 재밌어요!",
    //         // 한국시간
    //         CreatedAt = DateTime.UtcNow.AddHours(9)
    //     };
    //
    //     try
    //     {
    //         await AddPost(newPost);
    //         Debug.Log("게시글 등록 성공");
    //     }
    //     catch (Exception e)
    //     {
    //         Debug.LogError("게시글 등록 실패: " + e.Message);
    //     }
    // }
    //
    // public async Task AddComment(string postId, Comment comment)
    // {
    //     // 게시글 문서 참조
    //     DocumentReference postRef = _db.Collection("posts").Document(postId);
    //
    //     // 댓글 서브컬렉션 참조
    //     CollectionReference commentsRef = postRef.Collection("comments");
    //
    //     // 새로운 댓글 문서 ID 생성
    //     DocumentReference newCommentRef = commentsRef.Document();
    //     string generatedId = newCommentRef.Id;
    //     comment.Id = generatedId;
    //
    //     // 댓글 데이터를 Dictionary로 변환
    //     Dictionary<string, object> commentData = new Dictionary<string, object>
    //     {
    //         { "authorEmail", comment.AuthorEmail },
    //         { "authorNickname", comment.AuthorNickname },
    //         { "content", comment.Content },
    //         { "isModified", false },
    //         { "createdAt", Timestamp.FromDateTime(comment.CreatedAt.ToUniversalTime()) }
    //     };
    //
    //     // Firestore에 저장
    //     await newCommentRef.SetAsync(commentData);
    //
    //     // 댓글 수 증가시키기 (트랜잭션)
    //     await _db.RunTransactionAsync(async transaction =>
    //     {
    //         DocumentSnapshot snapshot = await transaction.GetSnapshotAsync(postRef);
    //         int currentCount = snapshot.ContainsField("commentCount") ? snapshot.GetValue<int>("commentCount") : 0;
    //         transaction.Update(postRef, "commentCount", currentCount + 1);
    //     });
    // }
    //
    // public async void OnClickAddCommentButton()
    // {
    //     Comment newComment = new Comment
    //     {
    //         AuthorEmail = "user1@a.a",
    //         AuthorNickname = "댓글러123",
    //         Content = "저도 그렇게 생각해요!",
    //         // 한국시간
    //         CreatedAt = DateTime.UtcNow.AddHours(9)
    //     };
    //
    //     try
    //     {
    //         List<Post> posts = await GetPostsByEmail("sangjin256@naver.com");
    //         await AddComment(posts[0].Id, newComment);
    //         Debug.Log("댓글 등록 성공!");
    //     }
    //     catch (Exception e)
    //     {
    //         Debug.LogError("댓글 등록 실패: " + e.Message);
    //     }
    // }
    //
    // public async Task<List<Comment>> GetComments(string postId)
    // {
    //     CollectionReference commentsRef = _db.Collection("posts").Document(postId).Collection("comments");
    //     QuerySnapshot snapshot = await commentsRef.OrderBy("createdAt").GetSnapshotAsync();
    //
    //     List<Comment> comments = new List<Comment>();
    //
    //     foreach (DocumentSnapshot doc in snapshot.Documents)
    //     {
    //         Comment comment = new Comment
    //         {
    //             Id = doc.Id, // 🔹 자동 생성된 ID를 여기서 획득
    //             AuthorEmail = doc.GetValue<string>("authorEmail"),
    //             AuthorNickname = doc.GetValue<string>("authorNickname"),
    //             Content = doc.GetValue<string>("content"),
    //             IsModified = doc.GetValue<bool>("isModified"),
    //             CreatedAt = doc.GetValue<Timestamp>("createdAt").ToDateTime()
    //         };
    //
    //         comments.Add(comment);
    //     }
    //
    //     return comments;
    // }
}

[Serializable]
public class Comment
{
    public string Id;
    public string AuthorEmail;
    public string AuthorNickname;
    public string Content;
    public bool IsModified;
    public DateTime CreatedAt;
}