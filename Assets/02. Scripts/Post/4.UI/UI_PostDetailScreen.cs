using UnityEngine;

public class UI_PostDetailScreen : MonoBehaviour
{
    public void OnClickEditPostButton()
    {
        UI_Manager.Instance.OnClickEditPostButton();
        gameObject.SetActive(false);
    }

    public async void OnClickDeletePostButton()
    {
        Result result = await PostManager.Instance.TryDeletePost(UI_Manager.Instance.Post);
        
        if (result.IsSuccess)
        {
            UI_Manager.Instance.PostPanel.gameObject.SetActive(false);
            UI_Manager.Instance.SetNotification("게시글이 삭제되었습니다.");
            UI_Manager.Instance.Board.UpdatePostPreviewList();
        }
        else
        {
            UI_Manager.Instance.SetNotification("본인이 작성한 게시글만 삭제할 수 있습니다.");
        }
        gameObject.SetActive(false);
    }
    
    public void OnClickCancelButton()
    {
        gameObject.SetActive(false);
    }

}
