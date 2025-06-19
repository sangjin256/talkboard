using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class UI_Comment : MonoBehaviour
{
    [SerializeField] private GameObject _commentSlotPrefab;
    [SerializeField] private GameObject _slotParent;
    private List<UI_CommentSlot> _commentSlotList;

    public void Start()
    {
        _commentSlotList = new List<UI_CommentSlot>();
    }

    public void OnClickRefresh()
    {
        Refresh();
    }

    public async void Refresh()
    {
        Debug.Log("채워넣어야댐");       
        string postId = "";
        List<CommentDTO> commentList = await CommentManager.Instance.GetAllCommentsOrderbyTime(postId);

        if(_commentSlotList.Count < commentList.Count)
        { 
            for(int i = 0; i < commentList.Count - _commentSlotList.Count; i++)
            {
                UI_CommentSlot slot = Instantiate(_commentSlotPrefab, _slotParent.transform).GetComponent<UI_CommentSlot>();
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
