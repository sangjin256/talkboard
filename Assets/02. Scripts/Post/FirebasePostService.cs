using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Firestore;
using static Firebase.Firestore.Query;

public class FirebasePostService
{
    private static CollectionReference PostCollection =>
        FirebaseFirestore.DefaultInstance.Collection("posts");

    // 최신순 정렬된 전체 게시글 불러오기
    public static async Task<List<PostDTO>> LoadAllPosts()
    {
        var snapshot = await PostCollection
            .OrderByDescending("CreatedAt")
            .GetSnapshotAsync();

        return snapshot.Documents
            .Select(doc => doc.ConvertTo<PostDTO>())
            .ToList();
    }

    // 특정 ID로 게시글 불러오기
    public static async Task<PostDTO> LoadPost(string postId)
    {
        var doc = await PostCollection.Document(postId).GetSnapshotAsync();

        if (doc.Exists)
        {
            return doc.ConvertTo<PostDTO>();
        }

        return null;
    }

    // 게시글 저장 (Create)
    public static async Task<bool> SavePost(PostDTO post)
    {
        await PostCollection.Document(post.Id).SetAsync(post);
        return true;
    }

    // 게시글 수정 (Edit)
    public static async Task<bool> EditPost(PostDTO post)
    {
        await PostCollection.Document(post.Id).SetAsync(post, SetOptions.MergeAll);
        return true;
    }

    // 게시글 삭제
    public static async Task<bool> DeletePost(string postId)
    {
        await PostCollection.Document(postId).DeleteAsync();
        return true;
    }
}