using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class AccountEmailSpecification : ISpecification<string>
{
    public string ErrorMessage { get; private set; }

    // �̸��� ���Խ�
    private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);

    public bool IsSatisfiedBy(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            ErrorMessage = "�̸����� ������� �� �����ϴ�.";
            return false;
        }

        if (!EmailRegex.IsMatch(value))
        {
            ErrorMessage = "�ùٸ� �̸��� ������ �ƴմϴ�.";
            return false;
        }

        return true;
    }
}
