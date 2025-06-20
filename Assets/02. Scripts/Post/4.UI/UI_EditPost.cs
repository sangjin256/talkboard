using System;
using UnityEngine;
using TMPro;

public class UI_EditPost : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;

    public void OnEnable()
    {
        if (UI_Manager.Instance.Post != null)
        {
            _inputField.text = UI_Manager.Instance.Post.Content;
        }
    }

    public async void OnClickEditPostButton()
    {
        string content = _inputField.text;
        PostDTO currentPost = UI_Manager.Instance.Post;

        if (string.IsNullOrEmpty(content))
        {
            UI_Manager.Instance.SetNotification("내용을 입력해주세요.");
            return;
        }

        Result result = await PostManager.Instance.TryEditPost(currentPost, content);
        
        if (result.IsSuccess)
        {
            UI_Manager.Instance.SetNotification("게시글이 수정되었습니다.");
            _inputField.text = string.Empty;
            gameObject.SetActive(false);
            await UI_Manager.Instance.Board.UpdatePostPreviewList();
        }
        else
        {
            UI_Manager.Instance.SetNotification(result.Message);
            return;
        }
    }
    
    public void OnClickCancelButton()
    {
        _inputField.text = "";
        gameObject.SetActive(false);
    }
}
