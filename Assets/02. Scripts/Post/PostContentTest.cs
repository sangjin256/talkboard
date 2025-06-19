using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PostContentTest : MonoBehaviour
{
    public AccountDTO dto;
    
    public async void OnClickLoginButton()
    {
        string email = "doa@test.com";
        string nickname = "hesther";
        string password = "123456"; // <- 여기에 길이 제한 걸 수 있음
        
        dto = new AccountDTO(email, nickname, password);
        Result result = await AccountManager.Instance.TryLogin(email, password);
    }
    
    // 1. 게시글 등록
    public async void OnClickAddPostButton()
    {
        string email = "doa@test.com";
        string nickname = "hesther";
        string content = "테스트게시글입니다."; // <- 여기에 길이 제한 걸 수 있음
        Post post = new Post("", email, nickname, content, 0, DateTime.UtcNow);

        if (string.IsNullOrWhiteSpace(content) || content.Trim().Length < 1)
        {
            Debug.LogWarning("게시글 내용은 1자 이상이어야 합니다.");
            return;
        }

        // var post = new Post(Guid.NewGuid().ToString(), email, nickname, content, DateTime.UtcNow.AddHours(9));
        Result result = await PostManager.Instance.TryCreatePost(content);
        Debug.Log(result.Message);
    }

    // 2. 게시글 목록 불러오기
    public async void OnClickLoadPostsButton()
    {
        List<PostDTO> posts = await PostManager.Instance.GetAllPostsAsync();
        Debug.Log($"불러온 게시글 수: {posts?.Count}");
        foreach (var post in posts)
        {
            Debug.Log($"[{post.Id}] {post.AuthorNickname} - {post.Content}");
        }
    }

    // 3. 게시글 상세 보기 (첫 번째 게시글)
    public async void OnClickViewDetailPost()
    {
        var posts = await PostManager.Instance.GetAllPostsAsync();
        if (posts.Count == 0)
        {
            Debug.Log("게시글이 없습니다.");
            return;
        }

        var firstPost = posts[0];
        PostDTO detailed = await PostManager.Instance.GetPostById(firstPost.Id);
        Debug.Log($"상세 보기 - 작성자: {detailed.AuthorNickname}, 내용: {detailed.Content}");
    }

    // 4. 게시글 수정
    public async void OnClickEditPost()
    {
        var posts = await PostManager.Instance.GetAllPostsAsync();
        if (posts.Count == 0)
        {
            Debug.Log("게시글이 없습니다.");
            return;
        }
        
        var firstPost = posts[0];
        string newContent = "수정된 내용입니다.";

        Result result = await PostManager.Instance.TryEditPost(firstPost, newContent);
        Debug.Log(result.Message);
    }

    // 5. 게시글 삭제
    public async void OnClickDeletePost()
    {
        var posts = await PostManager.Instance.GetAllPostsAsync();
        if (posts.Count == 0)
        {
            Debug.Log("게시글이 없습니다.");
            return;
        }

        var firstPost = posts[0];
        Result result = await PostManager.Instance.TryDeletePost(firstPost);
        Debug.Log(result.Message);
    }

    // 6. 좋아요 토글
    public async void OnClickToggleLike()
    {
        var posts = await PostManager.Instance.GetAllPostsAsync();
        if (posts.Count == 0)
        {
            Debug.Log("게시글이 없습니다.");
            return;
        }

        string userEmail = "doa@test.com";
        var firstPost = posts[0].ToDomain();
        bool wasLiked = firstPost.IsLikedBy(userEmail);

        firstPost.ToggleLike(userEmail);

        Debug.Log(wasLiked ? "좋아요 취소됨" : "좋아요 추가됨");

        // 저장
        await PostManager.Instance.TryEditPost(new PostDTO(firstPost), firstPost.Content);
    }
}
