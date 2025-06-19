using UnityEngine;

public class AccountDTO
{
    public readonly string Email;
    public readonly string NickName;
    public readonly string Password;

    public AccountDTO(string email, string nickName, string password)
    {
        Email = email;
        NickName = nickName;
        Password = password;
    }

    public AccountDTO(Account account)
    {
        Email = account.Email;
        NickName = account.NickName;
        Password = account.Password;
    }
}
