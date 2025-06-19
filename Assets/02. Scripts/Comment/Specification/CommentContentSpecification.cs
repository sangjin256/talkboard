using UnityEngine;
using System;

public class CommentContentSpecification : ISpecification<string>
{
    public string ErrorMessage { get; private set; }

    public bool IsSatisfiedBy(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            ErrorMessage = "내용이 비어있을 수 없습니다.";
            return false;
        }
        return true;
    }
}
