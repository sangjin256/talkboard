using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UI_Manager : BehaviourSingleton<UI_Manager>
{
    [SerializeField] private UI_Board _board;
    public UI_Board Board => _board;
    
    [SerializeField] private UI_Post _postPanel;
    [SerializeField] private UI_Comment _commentPanel;
    [SerializeField] private UI_WritePost WritePostPostPanel;
    [SerializeField] private TextMeshProUGUI _notificationdTextUI;
    [SerializeField] private ScrollRect _commentScrollRect;

    private CanvasGroup NotificationCanvasGroup;
    
    private PostDTO _post;
    public PostDTO Post => _post;

    private void Start()
    {
        NotificationCanvasGroup = _notificationdTextUI.transform.parent.GetComponent<CanvasGroup>();
    }

    public void OnClickContentPreview(PostDTO post)
    {
        _post = post;
        
        _postPanel.UpdateContent(post);
        _commentPanel.Refresh();
        _postPanel.gameObject.SetActive(true);
    }
 
    public async void OnClickRefreshButton()
    {
        await _board.UpdatePostPreviewList();
    }

    public void OnClickWritePostButton()
    {
        WritePostPostPanel.gameObject.SetActive(true);
    }

    public void RefreshComments()
    {
        _commentPanel.Refresh();
    }

    public void SetNotification(string content)
    {
        _notificationdTextUI.text = content;
        NotificationCanvasGroup.alpha = 1f;

        NotificationCanvasGroup.DOFade(0f, 0.5f).SetDelay(2f);
    }

    public void SetCommentScrollVerticalPoint(bool isDown)
    {
        if (isDown) _commentScrollRect.verticalNormalizedPosition = 0f;
        else _commentScrollRect.verticalNormalizedPosition = 1f;
    }
}
