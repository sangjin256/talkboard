using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using System;

public class AccountRepository
{
    public async Task<bool> TryAddAccount(AccountDTO account)
    {
        try
        {
            FirebaseUser user = (await FirebaseManager.Instance.Auth.CreateUserWithEmailAndPasswordAsync(account.Email, account.Password)).User;
            UserProfile profile = new UserProfile { DisplayName = account.NickName };
            await user.UpdateUserProfileAsync(profile);

            Debug.LogFormat("회원가입 성공: {0} ({1})", user.DisplayName, user.UserId);
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("회원가입 실패: " + e.Message);
            return false;
        }
    }

    public async Task<AccountDTO> GetAccount(AccountDTO account)
    {
        try
        {
            FirebaseUser user = (await FirebaseManager.Instance.Auth.SignInWithEmailAndPasswordAsync(account.Email, account.Password)).User;
            AccountDTO result = new AccountDTO(user.Email, user.DisplayName, account.Password);

            return result;
        }

        catch (FirebaseException fe)
        {
            Debug.LogError("Firebase 로그인 오류: " + fe.Message);
            return null;
        }
        catch (Exception e)
        {
            Debug.LogError("예외 발생: " + e.Message);
            return null;
        }
    }
}