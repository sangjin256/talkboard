using UnityEngine;

public class UI_Manager : BehaviourSingleton<UI_Manager>
{
    [SerializeField] private UI_Board _board;
    [SerializeField] private UI_Post PostPanel;
    
    private PostDTO _post;
    public PostDTO Post => _post;

    public void OnClickContentPreview(PostDTO post)
    {
        _post = post;
        
        PostPanel.UpdateContent(post);
        PostPanel.gameObject.SetActive(true);
    }
}
