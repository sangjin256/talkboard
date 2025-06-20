using UnityEngine;

public class UI_Manager : BehaviourSingleton<UI_Manager>
{
    [SerializeField] private UI_Board _board;
    [SerializeField] private UI_Post _postPanel;
    [SerializeField] private UI_Comment _commentPanel;
    
    private PostDTO _post;
    public PostDTO Post => _post;

    public void OnClickContentPreview(PostDTO post)
    {
        _post = post;
        
        _postPanel.UpdateContent(post);
        _commentPanel.Refresh();
        _postPanel.gameObject.SetActive(true);
    }
 
    public async void OnClickRefreshButton()
    {
        await _board.UpdateContentPreviewList();
    }

    public void RefreshComments()
    {
        _commentPanel.Refresh();
    }
}
