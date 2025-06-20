using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_PostPreview : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nicknameText;
    [SerializeField] private TextMeshProUGUI _contentText;
    [SerializeField] private TextMeshProUGUI _likeCountText;
    [SerializeField] private TextMeshProUGUI _commmentCountText;
    [SerializeField] private Image _likeImage;
    
    [SerializeField] private Sprite _redHeartSprite;
    [SerializeField] private Sprite _whiteHeartSprite;

    private PostDTO _post;

    public void UpdatePreview(PostDTO post)
    {
        _post = post;
        
        _nicknameText.text = post.AuthorNickname;
        _contentText.text = post.Content;
        _likeCountText.text = post.LikeCount.ToString();
        _commmentCountText.text = post.CommentCount.ToString();
        if (post.LikeUserEmails.Contains(AccountManager.Instance.CurrencAccount.Email))
        {
            _likeImage.sprite = _redHeartSprite;
        }
        else
        {
            _likeImage.sprite = _whiteHeartSprite;
        }
    }
    
    public void OnClickPostPreview()
    {
        Debug.Log("Clicked on content preview: " + _post.Id);
        UI_Manager.Instance.OnClickContentPreview(_post);
    }
}
