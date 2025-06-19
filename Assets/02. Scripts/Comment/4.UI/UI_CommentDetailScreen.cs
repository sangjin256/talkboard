using UnityEngine;
using TMPro;

public class UI_CommentDetailScreen : MonoBehaviour
{
    private UI_CommentSlot _commentSlot;

    public void Init(UI_CommentSlot slot)
    {
        _commentSlot = slot;
        gameObject.SetActive(true);
    }

    public void OnClickUpdateStartButton()
    {
        _commentSlot.UpdateCommentStart();
        gameObject.SetActive(false);
    }
    public async void OnClickDeleteButton()
    {
        await _commentSlot.DeleteComment();
        gameObject.SetActive(false);
    }

    public void OnClickCancelButton()
    {
        gameObject.SetActive(false);
    }
}
