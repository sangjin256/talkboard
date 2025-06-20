using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Threading.Tasks;

public class UI_Comment : MonoBehaviour
{
    [SerializeField] private GameObject _commentSlotPrefab;
    [SerializeField] private GameObject _slotParent;
    [SerializeField] private UI_CommentDetailScreen _commentDetailScreen;
    [SerializeField] private TMP_InputField _commentInputField;
    private List<UI_CommentSlot> _commentSlotList = new List<UI_CommentSlot>();

    [ContextMenu("REFRESH")]
    public void OnClickRefresh()
    {
        Refresh();
    }

    public async Task Refresh()
    {
        string postId = UI_Manager.Instance.Post.Id;
        List<CommentDTO> commentList = await CommentManager.Instance.GetAllCommentsOrderbyTime(postId);

        if (commentList == null) return;
        
        if(_commentSlotList.Count < commentList.Count)
        {
            int listCount = _commentSlotList.Count;
            for(int i = 0; i < commentList.Count - listCount; i++)
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

        await Task.Yield();
    }

    public async void OnClickAddCommentButton()
    {
        string postId = UI_Manager.Instance.Post.Id;
        Result result = await CommentManager.Instance.TryAddComment(postId, _commentInputField.text);

        if(result.IsSuccess == false)
        {
            UI_Manager.Instance.SetNotification(result.Message);
        }
        else
        {
            _commentInputField.text = string.Empty;
            await Refresh();
            UI_Manager.Instance.ForceUpdateCanvas();
            await Task.Yield();
            await UI_Manager.Instance.SetCommentScrollVerticalPoint(true);
        }
    }
}
