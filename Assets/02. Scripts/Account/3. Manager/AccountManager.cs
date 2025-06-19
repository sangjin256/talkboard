using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using UnityEditor;
using UnityEngine;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;

public class AccountManager : BehaviourSingleton<AccountManager>
{
    private Account _myAccount;
    public AccountDTO CurrencAccount => _myAccount.ToDTO();

    private AccountRepository _accountRepository;
    private const string SALT = "12315";

    private void Awake()
    {
        Init();
        DontDestroyOnLoad(gameObject);
    }

    private void Init()
    {
        _accountRepository = new AccountRepository();
    }

    public async Task<Result> TryRegister(AccountDTO dto)
    {
        string encryptedPassword = CryptoUtil.Encryption(dto.Password, SALT);
        AccountDTO account = new AccountDTO(dto.Email, dto.NickName, encryptedPassword);

        AccountDTO accountDTO = await _accountRepository.GetAccount(account);
        if(accountDTO != null)
        {
            return new Result(false, "이미 가입한 이메일입니다.");
        }

        if(await _accountRepository.TryAddAccount(account))
        {
            Debug.Log("회원가입에 성공하였습니다.");
            return new Result(true, "회원가입에 성공하였습니다.");
        }
        else
        {
            return new Result(false, "회원가입에 실패하였습니다");
        }
    }

    public async Task<Result> TryLogin(string email, string password)
    {
        string encryptedPassword = CryptoUtil.Encryption(password, SALT);
        AccountDTO accountDTO = await _accountRepository.GetAccount(new AccountDTO(email, "", encryptedPassword));
        if (accountDTO == null)
        {
            Debug.Log("로그인 실패");
            return new Result(false, "로그인에 실패하였습니다");
        }
        _myAccount = new Account(accountDTO.Email, accountDTO.NickName, "");
        
        Debug.Log("로그인 성공");
        return new Result(true, "로그인 성공!");
    }
}
