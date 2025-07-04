using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class AccountNicknameSpecification : ISpecification<string>
{
    public string ErrorMessage { get; private set; }

    // 금칙어 리스트
    private static readonly string[] BannedWords = { "바보", "멍청이", "운영자", "김홍일" };

    // 닉네임 정규식
    private static readonly Regex NickNameRegex = new Regex(@"^[가-힣a-zA-Z]{2,7}$");

    public bool IsSatisfiedBy(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            ErrorMessage = "닉네임은 비어있을 수 없습니다.";
            return false;
        }

        if (!NickNameRegex.IsMatch(value))
        {
            ErrorMessage = "닉네임은 한글 또는 영문으로 2자 이상 7자 이하만 가능합니다.";
            return false;
        }

        foreach (var banned in BannedWords)
        {
            if (value.Contains(banned, StringComparison.OrdinalIgnoreCase))
            {
                ErrorMessage = "닉네임에 부적절한 단어가 포함되어 있습니다.";
                return false;
            }
        }

        return true;
    }
}
