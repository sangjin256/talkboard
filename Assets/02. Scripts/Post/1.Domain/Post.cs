using System;
using System.Collections.Generic;

public class Post
{
    public string Id { get; private set; }
    public string AuthorEmail { get; private set; }
    public string AuthorNickname { get; private set; }
    public string Content { get; private set; }
    public int CommentCount { get; private set; }
    public HashSet<string> LikeUserEmails { get; private set; }
    public int LikeCount => LikeUserEmails?.Count ?? 0; // 좋아요 수는 항상 LikeUserEmails.Count로부터 계산 가능하다.
    public bool IsModified { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Post(string id, string authorEmail, string authorNickname, string content, DateTime createdAt)
    {
        // 예외 처리
        if (string.IsNullOrEmpty(id))
        {
            throw new Exception("아이디는 반드시 입력되어야 합니다.");
        }

        if (string.IsNullOrEmpty(authorEmail))
        {
            throw new Exception("이메일은 반드시 입력되어야 합니다.");
        }

        if (string.IsNullOrEmpty(authorNickname))
        {
            throw new Exception("닉네임은 반드시 입력되어야 합니다.");
        }

        if (string.IsNullOrEmpty(content))
        {
            throw new Exception("글 내용은 반드시 입력되어야 합니다.");
        }

        if (CreatedAt == DateTime.MinValue)
        {
            throw new Exception("글 생성 시간은 유효해야 합니다.");
        }

        Id = id;
        AuthorEmail = authorEmail;
        AuthorNickname = authorNickname;
        Content = content;
        CreatedAt = createdAt;
        CommentCount = 0;
        LikeUserEmails = new HashSet<string>();
        IsModified = false;
    }

    // 게시글을 수정/삭제할 수 있는 사람인지 확인
    private bool CanEdit(string userEmail)
    {
        return AuthorEmail == userEmail;
    }
    
    // 게시글 수정: 본문 내용을 바꾸고 IsModified를 true로 바꾼다.
    public bool TryEditContent(string userEmail, string newContent)
    {
        if (CanEdit(AuthorEmail))
        {
            if (Content != newContent)
            {
                Content = newContent;
                IsModified = true;
            }
        }

        return false;
    }

    // 게시글 좋아요: 유저 이메일 기준으로 좋아요/좋아요 취소 처리한다.
    public void ToggleLike(string userEmail)
    {
        if (LikeUserEmails.Contains(userEmail))
        {
            LikeUserEmails.Remove(userEmail);
        }
        
        else
        {
            LikeUserEmails.Add(userEmail);
        }
    }

    // 유저가 좋아요 눌렀는지 확인
    public bool IsLikedBy(string userEmail)
    {
        return LikeUserEmails.Contains(userEmail);
    }

    // 게시글 댓글 수 증가
    public void IncreaseCommentCount()
    {
        CommentCount++;
    }

    // 게시글 댓글 수 감소
    public void DecreaseCommentCount()
    {
        CommentCount = Math.Max(0, CommentCount - 1);
    }

    public PostDTO ToDTO()
    {
        return new  PostDTO(Id, AuthorEmail, AuthorNickname, Content, CreatedAt);
    }
}

    // 게시글 작성 -> 자기 자신을 만들 수 없다. 매니저 책임
    // 게시글 삭제 -> 저장소에 대한 작업. 레포 or 매니저 책임
    // 게시글 목록 보기 -> 포스트 여러개 불러오는 건 레포가 해야지
    // 게시글 상세 보기 -> 클릭하면 포스트 데이터 다 보여주는 거니까 이건 UI
