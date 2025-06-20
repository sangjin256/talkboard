using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class UI_Comment : MonoBehaviour
{
    [SerializeField] private GameObject _commentSlotPrefab;
    [SerializeField] private GameObject _slotParent;
    [SerializeField] private UI_CommentDetailScreen _commentDetailScreen;
    private List<UI_CommentSlot> _commentSlotList;

    public void Start()
    {
        _commentSlotList = new List<UI_CommentSlot>();
    }

    [ContextMenu("REFRESH")]
    public void OnClickRefresh()
    {
        Refresh();
    }

    public async void Refresh()
    {
        Debug.Log("채워넣어야댐");       
        string postId = "854At4JAotmWYHAbS9wQ";
        List<CommentDTO> commentList = await CommentManager.Instance.GetAllCommentsOrderbyTime(postId);

        if(_commentSlotList.Count < commentList.Count)
        { 
            for(int i = 0; i < commentList.Count - _commentSlotList.Count; i++)
            {
                UI_CommentSlot slot = Instantiate(_commentSlotPrefab, _slotParent.transform).GetComponent<UI_CommentSlot>();
                slot.Init(_commentDetailScreen);
                _commentSlotList.Add(slot);
            }
        }
        for (int i = 0; i < _commentSlotList.Count; i++)
        {
            if (i < commentList.Count)
            {
                _commentSlotList[i].Refresh(commentList[i]);
                _commentSlotList[i].gameObject.SetActive(true);
            }
            else _commentSlotList[i].gameObject.SetActive(false);
        }
    }
}
