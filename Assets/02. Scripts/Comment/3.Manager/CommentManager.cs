using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CommentManager : BehaviourSingleton<CommentManager>
{
    private CommentRepository _repository;

    private void Start()
    {
        Init();
        DontDestroyOnLoad(gameObject);
    }

    private void Init()
    {
        _repository = new CommentRepository();
    }

    public async Task<Result> TryAddComment(string postId, string content)
    {
        string email = AccountManager.Instance.CurrencAccount.Email;
        string nickname = AccountManager.Instance.CurrencAccount.NickName;

        if (string.IsNullOrWhiteSpace(content)) return new Result(false, "댓글은 빈칸일 수 없습니다!");

        CommentDTO comment = new CommentDTO("",
                                          email, 
                                          nickname, 
                                          content, 
                                          false,
                                          DateTime.UtcNow
                                          );

        return await _repository.TryAddComment(postId, comment);
    }

    public Result CheckIsMyComment(CommentDTO oldComment)
    {
        string email = AccountManager.Instance.CurrencAccount.Email;
        if(oldComment.AuthorEmail == email)
        {
            return new Result(true, "본인의 댓글입니다.");
        }
        return new Result(false, "본인의 댓글이 아닙니다.");
    }

    public async Task<Result> TryUpdateComment(string postId, CommentDTO oldComment, string content)
    {
        string email = AccountManager.Instance.CurrencAccount.Email;
        string nickname = AccountManager.Instance.CurrencAccount.NickName;
        Comment comment = new Comment(oldComment.Id,
                                      oldComment.AuthorEmail,
                                      oldComment.AuthorNickname,
                                      oldComment.Content,
                                      oldComment.IsModified,
                                      oldComment.CreatedAt
                                      );

        if(!comment.TryEditContent(email, content))
        {
            return new Result(false, "중복된 내용입니다.");
        }

        return await _repository.TryUpdateComment(postId, email, new CommentDTO(comment));
    }

    public async Task<Result> TryDeleteComment(string postId, CommentDTO comment)
    {
        string email = AccountManager.Instance.CurrencAccount.Email;

        return await _repository.TryDeleteComment(postId, comment.Id, email);
    }

    public async Task<List<CommentDTO>> GetAllComment(string postId)
    {
        return await _repository.GetAllComments(postId);
    }

    public async Task<List<CommentDTO>> GetAllCommentsOrderbyTime(string postId)
    {
        return await _repository.GetAllCommentsOrderbyTime(postId);
    }

    public async Task<List<CommentDTO>> GetcommentsByEmail(string postId, string email)
    {
        return await _repository.GetCommentsByEmail(postId, email);
    }
}
