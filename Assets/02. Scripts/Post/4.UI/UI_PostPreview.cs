using UnityEngine;
using TMPro;

public class UI_PostPreview : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nicknameText;
    [SerializeField] private TextMeshProUGUI _contentText;
    [SerializeField] private TextMeshProUGUI _likeCountText;
    [SerializeField] private TextMeshProUGUI _commmentCountText;

    private PostDTO _post;

    public void UpdatePreview(PostDTO post)
    {
        _post = post;
        
        _nicknameText.text = post.AuthorNickname;
        _contentText.text = post.Content;
        _likeCountText.text = post.LikeCount.ToString();
        _commmentCountText.text = post.CommentCount.ToString();
    }
    
    public void OnClickPostPreview()
    {
        Debug.Log("Clicked on content preview: " + _post.Id);
        UI_Manager.Instance.OnClickContentPreview(_post);
    }
}
