using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CommentRepository
{
    public async Task<bool> TryAddComment(string postId, CommentDTO comment)
    {
        try
        {
            DocumentReference postRef = FirebaseManager.Instance.DB.Collection("posts").Document(postId);

            CollectionReference commentsRef = postRef.Collection("comments");

            // 새로운 댓글 문서 ID 생성
            DocumentReference newCommentRef = commentsRef.Document();

            // 댓글 데이터를 Dictionary로 변환
            Dictionary<string, object> commentData = new Dictionary<string, object>
         {
             { "authorEmail", comment.AuthorEmail },
             { "authorNickname", comment.AuthorNickname },
             { "content", comment.Content },
             { "isModified", false },
             { "createdAt", Timestamp.FromDateTime(comment.CreatedAt.ToUniversalTime()) }
         };

            // Firestore에 저장
            await newCommentRef.SetAsync(commentData);

            await FirebaseManager.Instance.DB.RunTransactionAsync(async transaction =>
            {
                DocumentSnapshot snapshot = await transaction.GetSnapshotAsync(postRef);
                int currentCount = snapshot.ContainsField("commentCount") ? snapshot.GetValue<int>("commentCount") : 0;
                transaction.Update(postRef, "commentCount", currentCount + 1);
            });
        }
        catch(Exception e)
        {
            Debug.LogError("댓글 추가 실패");
            return false;
        }

        return true;
    }

    //public async void OnClickAddCommentButton()
    //{
    //    CommentDTO newComment = new CommentDTO("iD", "user1@a.a", "댓글러123", "저도 그렇게 생각해요!", false, DateTime.UtcNow.AddHours(9));

    //    try
    //    {
    //        List<Post> posts = await GetPostsByEmail("sangjin256@naver.com");
    //        await AddComment(posts[0].Id, newComment);
    //        Debug.Log("댓글 등록 성공!");
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogError("댓글 등록 실패: " + e.Message);
    //    }
    //}

    public async Task<List<CommentDTO>> GetAllComments(string postId)
    {
        CollectionReference commentsRef = FirebaseManager.Instance.DB.Collection("posts").Document(postId).Collection("comments");
        QuerySnapshot snapshot;
        try
        {
            snapshot = await commentsRef.GetSnapshotAsync();
        }
        catch (Exception e)
        {
            Debug.LogError($"댓글 불러오기 실패: {e.Message}");
            return null;
        }

        List<CommentDTO> comments = new List<CommentDTO>();

        foreach (DocumentSnapshot doc in snapshot.Documents)
        {
            try
            {
                CommentDTO comment = new CommentDTO(
                    doc.Id,
                    doc.GetValue<string>("authorEmail"),
                    doc.GetValue<string>("authorNickname"),
                    doc.GetValue<string>("content"),
                    doc.GetValue<bool>("isModified"),
                    doc.GetValue<Timestamp>("createdAt").ToDateTime()
                );

                comments.Add(comment);
            }
            catch (Exception e)
            {
                Debug.LogError($"댓글 변환 실패: {e.Message}");
                return null;
            }
        }

        return comments;
    }

    public async Task<List<CommentDTO>> GetAllCommentsOrderbyTime(string postId)
    {
        CollectionReference commentsRef = FirebaseManager.Instance.DB.Collection("posts").Document(postId).Collection("comments");
        QuerySnapshot snapshot;
        try
        {
            snapshot = await commentsRef.OrderBy("createdAt").GetSnapshotAsync();
        }
        catch (Exception e)
        {
            Debug.LogError($"댓글 불러오기 실패: {e.Message}");
            return null;
        }

        List<CommentDTO> comments = new List<CommentDTO>();

        foreach (DocumentSnapshot doc in snapshot.Documents)
        {
            try
            {
                CommentDTO comment = new CommentDTO(
                    doc.Id,
                    doc.GetValue<string>("authorEmail"),
                    doc.GetValue<string>("authorNickname"),
                    doc.GetValue<string>("content"),
                    doc.GetValue<bool>("isModified"),
                    doc.GetValue<Timestamp>("createdAt").ToDateTime()
                );

                comments.Add(comment);
            }
            catch (Exception e)
            {
                Debug.LogError($"댓글 변환 실패: {e.Message}");
                return null;
            }
        }

        return comments;
    }

    public async Task<List<CommentDTO>> GetCommentsByEmail(string postId, string email)
    {
        CollectionReference commentsRef = FirebaseManager.Instance.DB.Collection("posts").Document(postId).Collection("comments");
        QuerySnapshot snapshot;
        try
        {
            snapshot = await commentsRef.WhereEqualTo("authorEmail", email).GetSnapshotAsync();
        }
        catch (Exception e)
        {
            Debug.LogError($"댓글 불러오기 실패: {e.Message}");
            return null;
        }

        List<CommentDTO> comments = new List<CommentDTO>();

        foreach (DocumentSnapshot doc in snapshot.Documents)
        {
            try
            {
                CommentDTO comment = new CommentDTO(
                    doc.Id,
                    doc.GetValue<string>("authorEmail"),
                    doc.GetValue<string>("authorNickname"),
                    doc.GetValue<string>("content"),
                    doc.GetValue<bool>("isModified"),
                    doc.GetValue<Timestamp>("createdAt").ToDateTime()
                );

                comments.Add(comment);
            }
            catch (Exception e)
            {
                Debug.LogError($"댓글 변환 실패: {e.Message}");
                return null;
            }
        }

        return comments;
    }

    public async Task<Result> TryUpdateComment(string postId, string email, CommentDTO comment)
    {
        try
        {
            DocumentReference commentRef = FirebaseManager.Instance.DB.Collection("posts").Document(postId).Collection("comments").Document(comment.Id);

            DocumentSnapshot snapshot = await commentRef.GetSnapshotAsync();

            if (!snapshot.Exists)
            {
                return new Result(false, $"수정 실패: 댓글({comment.Id}) 존재하지 않음");
            }

            string authorEmail = snapshot.GetValue<string>("authorEmail");
            if (authorEmail != email)
            {
                return new Result(false, "수정 권한 없음: 본인의 댓글만 수정할 수 있습니다.");
            }
            Dictionary<string, object> newCommentData = new Dictionary<string, object>
             {
                 { "authorEmail", comment.AuthorEmail },
                 { "authorNickname", comment.AuthorNickname },
                 { "content", comment.Content },
                 { "isModified", true },
             };

            await commentRef.UpdateAsync(newCommentData);
            return new Result(true, "댓글 수정 성공");
        }
        catch (Exception e)
        {
            return new Result(false, "댓글 수정 실패");
        }
    }

    public async Task<Result> TryDeleteComment(string postId, string commentId, string email)
    {
        try
        {
            DocumentReference postRef = FirebaseManager.Instance.DB.Collection("posts").Document(postId);
            DocumentReference commentRef = postRef.Collection("comments").Document(commentId);
            DocumentSnapshot snapshot = await commentRef.GetSnapshotAsync();

            if (!snapshot.Exists)
            {
                return new Result(false, $"삭제 실패: 댓글({commentId}) 존재하지 않음");
            }

            string authorEmail = snapshot.GetValue<string>("authorEmail");
            if (authorEmail != email)
            {
                return new Result(false, "삭제 권한 없음: 본인의 댓글만 삭제할 수 있습니다.");
            }
            await FirebaseManager.Instance.DB.RunTransactionAsync(async transaction =>
            {
                DocumentSnapshot snapshot = await transaction.GetSnapshotAsync(postRef);
                int currentCount = snapshot.ContainsField("commentCount") ? snapshot.GetValue<int>("commentCount") : 0;
                transaction.Update(postRef, "commentCount", currentCount - 1);
            });

            await commentRef.DeleteAsync();

            return new Result(true, $"댓글 삭제 성공: {commentId}");
        }
        catch (Exception e)
        {
            return new Result(false, $"댓글 삭제 실패");
        }
    }
}
