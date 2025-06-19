using System;
using UnityEngine;

public class Comment
{
    public string Id { get; private set; }
    public string AuthorEmail { get; private set; }
    public string AuthorNickname { get; private set; }
    public string Content { get; private set; }
    public bool IsModified { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Comment(string id, string authorEmail, string authorNickname, string content, bool isModified, DateTime createdAt)
    {
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(authorEmail))
        {
            throw new Exception(emailSpecification.ErrorMessage);
        }

        var nicknameSpecification = new AccountNicknameSpecification();
        if (!nicknameSpecification.IsSatisfiedBy(authorNickname))
        {
            throw new Exception(nicknameSpecification.ErrorMessage);
        }

        var contentSpecification = new CommentContentSpecification();
        if (!contentSpecification.IsSatisfiedBy(content))
        {
            throw new Exception(contentSpecification.ErrorMessage);
        }

        Id = id;
        AuthorEmail = authorEmail;
        AuthorNickname = authorNickname;
        Content = content;
        IsModified = isModified;
        CreatedAt = createdAt;
    }

    public bool TryEditContent(string userEmail, string newContent)
    {
        if (CanEdit(userEmail))
        {
            if (Content != newContent)
            {
                Content = newContent;
                IsModified = true;
                return true;
            }
        }

        return false;
    }

    private bool CanEdit(string userEmail)
    {
        return AuthorEmail == userEmail;
    }
}
