using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_CommentSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameTextUI;
    [SerializeField] private TextMeshProUGUI _timeTextUI;
    [SerializeField] private TextMeshProUGUI _modifiedTextUI;

    [SerializeField] private TextMeshProUGUI _contentTextUI;
    private CommentDTO _comment;

    public void Init(CommentDTO comment)
    {
        _comment = comment;
        _nameTextUI.text = _comment.AuthorNickname;
        _timeTextUI.text = FormatKoreanTimeAgo(_comment.CreatedAt);
        _modifiedTextUI.text = _comment.IsModified == true ? "수정됨" : "";

        _contentTextUI.text = _comment.Content;
    }

    private string FormatKoreanTimeAgo(DateTime dateTime)
    {
        DateTime now = DateTime.UtcNow.AddHours(9); // 현재 한국 시간
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
