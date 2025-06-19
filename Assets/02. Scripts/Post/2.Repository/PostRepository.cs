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
        try
        {
            QuerySnapshot snapshot = await PostCollection.OrderByDescending("createdAt").GetSnapshotAsync();
            List<PostDTO> posts = new();

            foreach (var doc in snapshot.Documents)
            {
                posts.Add(doc.ConvertTo<PostDTO>());
            }

            return posts;
        }
        catch (Exception e)
        {
            Debug.LogError("게시글 전체 불러오기 실패: " + e.Message);
            return null;
        }
    }

    public async Task<PostDTO> LoadPostByIdAsync(string postId)
    {
        var doc = await PostCollection.Document(postId).GetSnapshotAsync();
        if (!doc.Exists) return null;
        return doc.ConvertTo<PostDTO>();
    }

    public async Task<bool> SavePostAsync(PostDTO post)
    {
        
        await PostCollection.Document(post.Id).SetAsync(post);
        return true;
    }

    public async Task<bool> EditPostAsync(PostDTO post)
    {
        await PostCollection.Document(post.Id).SetAsync(post, SetOptions.MergeAll);
        return true;
    }

    public async Task<bool> DeletePostAsync(string postId)
    {
        await PostCollection.Document(postId).DeleteAsync();
        return true;
    }
}