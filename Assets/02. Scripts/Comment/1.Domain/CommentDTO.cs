using System;

public class CommentDTO
{
    public readonly string Id;
    public readonly string AuthorEmail;
    public readonly string AuthorNickname;
    public readonly string Content;
    public readonly bool IsModified;
    public readonly DateTime CreatedAt;

    // 모든 필드를 명시적으로 받는 생성자
    public CommentDTO(string id, string authorEmail, string authorNickname, string content, bool isModified, DateTime createdAt)
    {
        Id = id;
        AuthorEmail = authorEmail;
        AuthorNickname = authorNickname;
        Content = content;
        IsModified = isModified;
        CreatedAt = createdAt;
    }

    public CommentDTO(Comment comment)
    {
        Id = comment.Id;
        AuthorEmail = comment.AuthorEmail;
        AuthorNickname = comment.AuthorNickname;
        Content = comment.Content;
        IsModified = comment.IsModified;
        CreatedAt = comment.CreatedAt;
    }
}