using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.SceneManagement;

[Serializable]
public class UI_InputFields
{
    public TextMeshProUGUI ResultText;
    public TMP_InputField IDInputField;
    public TMP_InputField NicknameInputField;
    public TMP_InputField PasswordInputField;
    public TMP_InputField PasswordConfirmInputField;

    public Button ConfirmButton;
}

public class UI_LoginScene : MonoBehaviour
{
    [Header("패널")]
    public GameObject LoginPanel;
    public GameObject SignupPanel;

    [Header("로그인")]
    public UI_InputFields LoginInputFields;

    [Header("회원가입")]
    public UI_InputFields SignupInputFields;

    private const string PREFIX = "ID_";
    private const string SALT = "1006247";

    private void Start()
    {
        OnClickGoToLoginButton();
        LoginCheck();
    }

    public void OnClickGoToSignupButton()
    {
        LoginPanel.SetActive(false);
        SignupPanel.SetActive(true);
    }

    public void OnClickGoToLoginButton()
    {
        LoginPanel.SetActive(true);
        SignupPanel.SetActive(false);
    }

    // 회원가입
    public async void Signup()
    {
        // 1. 이메일 입력 확인
        string email = SignupInputFields.IDInputField.text;
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            SignupInputFields.ResultText.text = emailSpecification.ErrorMessage;
            SignupInputFields.ResultText.transform.DOShakePosition(0.5f, 15);
            return;
        }

        // 2. 닉네임 입력 확인
        string nickName = SignupInputFields.NicknameInputField.text;
        var nicknameSpecification = new AccountNicknameSpecification();
        if (!nicknameSpecification.IsSatisfiedBy(nickName))
        {
            SignupInputFields.ResultText.text = nicknameSpecification.ErrorMessage;
            SignupInputFields.ResultText.transform.DOShakePosition(0.5f, 15);
            return;
        }

        // 3. 비밀번호 입력 확인
        string password = SignupInputFields.PasswordInputField.text;
        var passwordSpecification = new AccountPasswordSpecification();
        if (!passwordSpecification.IsSatisfiedBy(password))
        {
            SignupInputFields.ResultText.text = passwordSpecification.ErrorMessage;
            SignupInputFields.ResultText.transform.DOShakePosition(0.5f, 15);
            return;
        }
        
        string confirmPwd = SignupInputFields.PasswordConfirmInputField.text;
        if (string.IsNullOrEmpty(confirmPwd))
        {
            SignupInputFields.ResultText.text = "확인 비밀번호를 입력해주세요.";
            SignupInputFields.ResultText.transform.DOShakePosition(0.5f, 15);
            return;
        }
        else if(confirmPwd.Equals(password) == false)
        {
            SignupInputFields.ResultText.text = "확인 비밀번호가 다릅니다.";
            SignupInputFields.ResultText.transform.DOShakePosition(0.5f, 15);
            return;
        }

        Result result = await AccountManager.Instance.TryRegister(new AccountDTO(email, nickName, password));
        if (result.IsSuccess)
        {
            LoginInputFields.IDInputField.text = email;
            OnClickGoToLoginButton();
        }
        else
        {
            LoginInputFields.ResultText.text = result.Message;
            SignupInputFields.ResultText.transform.DOShakePosition(0.5f, 15);
            return;
        }
    }

    public async void Login()
    {
        // 1. 아이디 입력을 확인한다.
        string id = LoginInputFields.IDInputField.text;
        if (string.IsNullOrEmpty(id))
        {
            LoginInputFields.ResultText.text = "아이디를 입력해주세요.";
            LoginInputFields.ResultText.transform.DOShakePosition(0.5f, 15);
            return;
        }

        string pwd = LoginInputFields.PasswordInputField.text;

        Result result = await AccountManager.Instance.TryLogin(id, pwd);
        if (result.IsSuccess)
        {
            LoginInputFields.ResultText.text = result.Message;
            SceneManager.LoadScene(1);
        }
        else
        {
            LoginInputFields.ResultText.text = result.Message;
            LoginInputFields.ResultText.transform.DOShakePosition(0.5f, 15);
            return;
        }
    }

    public void LoginCheck()
    {
        string id = LoginInputFields.IDInputField.text;
        string password = LoginInputFields.PasswordInputField.text;
    }
}
