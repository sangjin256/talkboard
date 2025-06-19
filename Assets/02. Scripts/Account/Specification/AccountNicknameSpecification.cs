using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class AccountNicknameSpecification : ISpecification<string>
{
    public string ErrorMessage { get; private set; }

    // ��Ģ�� ����Ʈ
    private static readonly string[] BannedWords = { "�ٺ�", "��û��", "���", "��ȫ��" };

    // �г��� ���Խ�
    private static readonly Regex NickNameRegex = new Regex(@"^[��-�Ra-zA-Z]{2,7}$");

    public bool IsSatisfiedBy(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            ErrorMessage = "�г����� ������� �� �����ϴ�.";
            return false;
        }

        if (!NickNameRegex.IsMatch(value))
        {
            ErrorMessage = "�г����� �ѱ� �Ǵ� �������� 2�� �̻� 7�� ���ϸ� �����մϴ�.";
            return false;
        }

        foreach (var banned in BannedWords)
        {
            if (value.Contains(banned, StringComparison.OrdinalIgnoreCase))
            {
                ErrorMessage = "�г��ӿ� �������� �ܾ ���ԵǾ� �ֽ��ϴ�.";
                return false;
            }
        }

        return true;
    }
}
