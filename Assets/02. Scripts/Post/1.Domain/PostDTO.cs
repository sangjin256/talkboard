using System;
using System.Collections.Generic;

public class PostDTO
{
    public string Id;
    public string AuthorEmail;
    public string AuthorNickname;
    public string Content;
    public int CommentCount;
    public HashSet<string> LikeUserEmails;
    public int LikeCount;
    public bool IsModified;
    public DateTime CreatedAt;

    public PostDTO(string id, string authorEmail, string authorNickname, string content, DateTime createdAt)
    {
        Id = id;
        AuthorEmail = authorEmail;
        AuthorNickname = authorNickname;
        Content = content;
        CreatedAt = createdAt;
        CommentCount = 0;
        LikeUserEmails = new HashSet<string>();
        IsModified = false;
    }

    public PostDTO(Post post)
    {
        Id = post.Id;
        AuthorEmail = post.AuthorEmail;
        AuthorNickname = post.AuthorNickname;
        Content = post.Content;
        CommentCount = post.CommentCount;
        LikeUserEmails = new HashSet<string>(post.LikeUserEmails);
        LikeCount = post.LikeCount;
        IsModified = post.IsModified;
        CreatedAt = post.CreatedAt;
    }
    

    public Post ToDomain()
    {
        var post = new Post(Id, AuthorEmail, AuthorNickname, Content, CreatedAt);

        // 댓글 수 및 수정 여부 수동 세팅
        for (int i = 0; i < CommentCount; i++) post.IncreaseCommentCount();
        if (IsModified) post.EditContent(Content); // 강제 수정된 걸로 표시

        // 좋아요 목록 복원
        foreach (var email in LikeUserEmails)
            post.ToggleLike(email);

        return post;
    }
}