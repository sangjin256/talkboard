using UnityEngine;
using TMPro;

public class UI_WritePost : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;

    public async void OnClickWritePostButton()
    {
        string content = _inputField.text;

        if (string.IsNullOrEmpty(content))
        {
            UI_Manager.Instance.SetNotification("내용을 입력해주세요.");
            return;
        }

        Result result = await PostManager.Instance.TryCreatePost(content);

        if (result.IsSuccess)
        {
            UI_Manager.Instance.SetNotification("게시글이 등록되었습니다.");
            _inputField.text = string.Empty;
            gameObject.SetActive(false);
            await UI_Manager.Instance.Board.UpdateContentPreviewList();
        }
        else
        {
            UI_Manager.Instance.SetNotification(result.Message);
        }
    }

    public void OnClickCancelButton()
    {
        _inputField.text = "";
        gameObject.SetActive(false);
    }
}
