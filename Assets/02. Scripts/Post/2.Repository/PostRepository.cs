using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PostRepository
{
    private CollectionReference PostCollection => FirebaseManager.Instance.DB.Collection("posts");

    public async Task<List<PostDTO>> LoadAllPostsAsync()
    { 
        QuerySnapshot snapshot;
        try
        {
            snapshot = await PostCollection.OrderByDescending("createdAt").GetSnapshotAsync();
        }
        catch (Exception e)
        {
            Debug.LogError($"게시글 불러오기 실패: {e.Message}");
            return null;
        }

        List<PostDTO> posts = new List<PostDTO>();
            foreach (var doc in snapshot.Documents)
            {
                try
                {
                    PostDTO post = new PostDTO(
                        doc.Id,
                        doc.GetValue<string>("authorEmail"),
                        doc.GetValue<string>("authorNickname"),
                        doc.GetValue<string>("content"),
                        doc.GetValue<int>("commentCount"),
                        doc.GetValue<Timestamp>("createdAt").ToDateTime(),
                        doc.GetValue<bool>("isModified")
                    );
                    posts.Add(post);
                }
                catch (Exception e)
                {
                    Debug.LogError($"게시글 변환 실패: {e.Message}");
                    return null;
                }
            }

            return posts; 
    }


    public async Task<PostDTO> LoadPostByIdAsync(string postId)
    {
        try
        {
            DocumentSnapshot doc = await PostCollection.Document(postId).GetSnapshotAsync();
            if (!doc.Exists)
            {
                Debug.LogWarning($"게시글 조회 실패: ID({postId}) 존재하지 않음");
                return null;
            }
            try
            {
                PostDTO post = new PostDTO(
                    doc.Id,
                    doc.GetValue<string>("authorEmail"),
                    doc.GetValue<string>("authorNickname"),
                    doc.GetValue<string>("content"),
                    doc.GetValue<int>("commentCount"),
                    doc.GetValue<Timestamp>("createdAt").ToDateTime(),
                    doc.GetValue<bool>("isModified")
                );
                return post;
            }
            catch (Exception e)
            {
                Debug.LogError($"게시글 변환 실패: {e.Message}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"게시글 조회 실패: {e.Message}");
        }
        return null;
    }

    public async Task<bool> AddPostAsync(PostDTO post)
    {
        try
        {
            DocumentReference newPostRef = PostCollection.Document();
            post.Id = newPostRef.Id;

            Dictionary<string, object> postData = new Dictionary<string, object>
            {
                { "authorEmail", post.AuthorEmail },
                { "authorNickname", post.AuthorNickname },
                { "content", post.Content },
                { "commentCount", post.CommentCount },
                { "isModified", false },
                { "createdAt", Timestamp.FromDateTime(post.CreatedAt.ToUniversalTime()) }
            };
            
            await newPostRef.SetAsync(postData);

            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("포스트 생성에 실패했습니다.");
            return false;
        }
    }

    public async Task<bool> EditPostAsync(PostDTO post)
    {
        try
        {
            // await PostCollection.Document(post.Id).SetAsync(post, SetOptions.MergeAll);
            DocumentReference newPostRef = PostCollection.Document(post.Id);

            Dictionary<string, object> postData = new Dictionary<string, object>
            {
                { "authorEmail", post.AuthorEmail },
                { "authorNickname", post.AuthorNickname },
                { "content", post.Content },
                { "commentCount", post.CommentCount },
                { "isModified", false },
                { "createdAt", Timestamp.FromDateTime(post.CreatedAt.ToUniversalTime()) }
            };
            
            await newPostRef.SetAsync(postData);
            return true;

        }
        catch (Exception e)
        {
            Debug.LogError("게시글 수정 실패.");
            return false;
        }
    }

    public async Task<bool> DeletePostAsync(PostDTO post)
    {
        try
        {
            DocumentReference newPostRef = PostCollection.Document(post.Id);
            
            await newPostRef.DeleteAsync();

            return true;

        }
        catch (Exception e)
        {
            Debug.LogError("포스트 삭제 실패");
            throw;
        }
    }
}