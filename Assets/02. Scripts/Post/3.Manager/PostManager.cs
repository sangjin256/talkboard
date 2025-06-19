using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PostManager : BehaviourSingleton<PostManager>
{
    private Post _myPost;
    
    private PostRepository _postRepository;

    private void Awake()
    {
        Init();
        DontDestroyOnLoad(gameObject);
    }

    private void Init()
    {
        _postRepository = new PostRepository();
    }

    public async Task<List<Post>> GetAllPostsAsync()
    {
        var dtos = await _postRepository.LoadAllPostsAsync();
        var posts = new List<Post>();
        foreach (var dto in dtos)
        {
            posts.Add(dto.ToDomain());
        }
        return posts;
    }

    public async Task<Post> GetPostByIDAsync(string postId)
    {
        var dto = await _postRepository.LoadPostByIdAsync(postId);
        return dto?.ToDomain();
    }

    public async Task<bool> TryCreatePostAsync(PostDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Content))
        {
            Debug.LogWarning("내용이 비어 있습니다.");
            return false;
        }
        
        var post = dto.ToDomain();
        return await _postRepository.SavePostAsync(post.ToDTO());
    }
    
    
    
    
    
    
    // 게시글 작성 -> 자기 자신을 만들 수 없다. 매니저 책임
    // 게시글 삭제 -> 저장소에 대한 작업. 레포 or 매니저 책임
    
}