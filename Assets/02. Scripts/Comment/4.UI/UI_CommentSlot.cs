using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Threading.Tasks;

public class UI_CommentSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameTextUI;
    [SerializeField] private TextMeshProUGUI _timeTextUI;
    [SerializeField] private TextMeshProUGUI _isModifiedTextUI;
    [SerializeField] private TMP_InputField _modifyTextInputField;

    [SerializeField] private TextMeshProUGUI _contentTextUI;
    [SerializeField] private GameObject DetailScreen;

    private CommentDTO _comment;
    public CommentDTO CommentDTO => _comment;

    public void Refresh(CommentDTO comment)
    {
        _comment = comment;
        _modifyTextInputField.gameObject.SetActive(false);
        _nameTextUI.text = _comment.AuthorNickname;
        _timeTextUI.text = FormatKoreanTimeAgo(_comment.CreatedAt);
        _isModifiedTextUI.text = _comment.IsModified == true ? "수정됨" : "";

        _contentTextUI.text = _comment.Content;
    }

    public async void OnClickDeleteButton()
    {
        string postId = "";
        Debug.Log("포스트 아이디 가져오기 + 예외처리");
        Result result = await CommentManager.Instance.TryDeleteComment(postId, _comment);

        if (result.IsSuccess == false)
        {
            Debug.LogError(result.Message);
        }

        DetailScreen.SetActive(false);
    }

    public void OnClickUpdateStartButton()
    {
        _modifyTextInputField.text = _contentTextUI.text;
        _contentTextUI.gameObject.SetActive(false);
        _modifyTextInputField.gameObject.SetActive(true);
        DetailScreen.SetActive(false);
    }

    public async Task OnClickUpdateButton()
    {
        string postId = "";
        Debug.Log("포스트 아이디 가져오기");
        _modifyTextInputField.gameObject.SetActive(false);
        Result result = await CommentManager.Instance.TryUpdateComment(postId, _comment, _modifyTextInputField.text);

        if (result.IsSuccess)
        {
            _contentTextUI.text = _modifyTextInputField.text;
        }
        else
        {
            Debug.LogError(result.Message);
        }

        _contentTextUI.gameObject.SetActive(true);
    }

    private string FormatKoreanTimeAgo(DateTime dateTime)
    {
        DateTime now = DateTime.UtcNow;
        TimeSpan diff = now - dateTime;

        if (diff.TotalMinutes < 0)
        {
            return "방금 전"; // 미래 시간 방지용
        }

        if (diff.TotalMinutes < 2)
        {
            return "방금 전";
        }

        if (diff.TotalMinutes < 60)
        {
            int minutes = (int)diff.TotalMinutes;
            return $"{minutes}분 전";
        }

        if (diff.TotalHours < 24)
        {
            int hours = (int)diff.TotalHours;
            return $"{hours}시간 전";
        }

        if (diff.TotalDays < 30)
        {
            int days = (int)diff.TotalDays;
            return $"{days}일 전";
        }

        if (diff.TotalDays < 365)
        {
            int months = (int)(diff.TotalDays / 30);
            return $"{months}달 전";
        }

        int years = (int)(diff.TotalDays / 365);
        return $"{years}년 전";
    }
}
