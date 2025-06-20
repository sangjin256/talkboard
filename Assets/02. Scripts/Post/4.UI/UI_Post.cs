using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Post : MonoBehaviour
{
    [SerializeField] private Button _backButton;
    [SerializeField] private TextMeshProUGUI _nicknameText;
    [SerializeField] private TextMeshProUGUI _timeStampText;
    [SerializeField] private TextMeshProUGUI _modifiedText; 
    [SerializeField] private TextMeshProUGUI _contentText;
    [SerializeField] private TextMeshProUGUI _likeCountText;
    [SerializeField] private TextMeshProUGUI _commentCountText;
    [SerializeField] private Button _likeButton;
    [SerializeField] private Sprite _redHeartSprite;
    [SerializeField] private Sprite _whiteHeartSprite;
    
    private PostDTO _post;

    public void UpdateContent(PostDTO post)
    {
        _post = post;
        
        _nicknameText.text = _post.AuthorNickname;
        _timeStampText.text = _post.CreatedAt.ToString("yyyy.MM.dd. HH:mm:ss");
        if (post.IsModified)
        {
            _modifiedText.gameObject.SetActive(true);
        }
        else
        {
            _modifiedText.gameObject.SetActive(false);
        }
        _contentText.text = _post.Content;
        _likeCountText.text = _post.LikeCount.ToString();
        _commentCountText.text = $"댓글({_post.CommentCount.ToString()})";
        if (post.LikeUserEmails.Contains(AccountManager.Instance.CurrencAccount.Email))
        {
            _likeButton.image.sprite = _redHeartSprite;
        }
        else
        {
            _likeButton.image.sprite = _whiteHeartSprite;
        }
    }

    public async void OnClickLikeButton()
    { 
        Debug.Log("OnClickLikeButton");
        await PostManager.Instance.ToggleLike(_post);
        UI_Manager.Instance.RefreshPost();
    }

    public void OnClickBackButton()
    {
        UI_Manager.Instance.Board.UpdatePostPreviewList();
        gameObject.SetActive(false);
    }

    public void OnClickOptionButton()
    {
        UI_Manager.Instance.PostDetailScreen.SetActive(true);
    }

    public async Task RefreshPost()
    {
        PostDTO refreshedPost = await PostManager.Instance.GetPostById(_post.Id);
        
        if (refreshedPost != null)
        {
            UpdateContent(refreshedPost);
        }
        else
        {
            UI_Manager.Instance.SetNotification("게시글 새로고침에 실패했습니다.");
        }
    }
}
