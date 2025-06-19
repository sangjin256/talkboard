using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PostManager : BehaviourSingleton<PostManager>
{
    private PostRepository _repository;

    private void Awake()
    {
        Init();
        DontDestroyOnLoad(gameObject);
    }

    private void Init()
    {
        _repository = new PostRepository();
    }

    // 게시글 작성
    public async Task<Result> TryCreatePost(string content)
    {
        string email = AccountManager.Instance.CurrencAccount.Email;
        string nickname = AccountManager.Instance.CurrencAccount.NickName;
        Post post = new Post("",
            email, 
            nickname, 
            content,
            0,
            DateTime.UtcNow
        );
        
        if (string.IsNullOrWhiteSpace(content) || content.Length < 1)
        {
            return new Result(false, "게시글은 최소 1자 이상이어야 합니다.");
        }
        
        bool success = await _repository.AddPostAsync(post.ToDTO());
        return new Result(success, success ? "게시글을 작성했습니다." : "게시글 작성 실패");
    }

    // 게시글 수정
    public async Task<Result> TryEditPost(PostDTO oldPost, string content)
    {
        string email = AccountManager.Instance.CurrencAccount.Email;

        Post post = oldPost.ToDomain();
        if (!post.TryEditContent(email, content))
        {
            return new Result(false, "수정할 권한이 없거나 동일한 내용입니다.");
        }

        bool success = await _repository.EditPostAsync(post.ToDTO());
        return new Result(success, success ? "게시글 수정 완료" : "게시글 수정 실패");
    }

    // 게시글 삭제
    public async Task<Result> TryDeletePost(PostDTO oldPost)
    {
        string email = AccountManager.Instance.CurrencAccount.Email;
        
        Post post = oldPost.ToDomain();
        if (!post.CanBeDeletedBy(email))
        {
            return new Result(false, "삭제 권한이 없습니다.");
        }

        bool success = await _repository.DeletePostAsync(post.ToDTO());
        return new Result(success, success ? "게시글 삭제 완료" : "게시글 삭제 실패");
    }

    // 게시글 목록 가져오기
    public async Task<List<PostDTO>> GetAllPostsAsync()
    {
        return await _repository.LoadAllPostsAsync();
    }

    // 게시글 업로드
    public async Task<PostDTO> GetPostById(string postId)
    {
        return await _repository.LoadPostByIdAsync(postId);
    }
    
    // 게시글 좋아요
    public async Task<Result> ToggleLike(PostDTO dto)
    {
        string email = AccountManager.Instance.CurrencAccount.Email;

        Post post = dto.ToDomain();
        post.ToggleLike(email);

        bool success = await _repository.EditPostAsync(post.ToDTO());
        return new Result(success, post.IsLikedBy(email) ? "좋아요를 눌렀습니다." : "좋아요를 취소했습니다.");
    }
}