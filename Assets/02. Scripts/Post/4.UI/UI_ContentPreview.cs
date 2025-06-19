using UnityEngine;
using TMPro;

public class UI_ContentPreview : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nicknameText;
    [SerializeField] private TextMeshProUGUI _contentText;
    [SerializeField] private TextMeshProUGUI _likeCountText;
    [SerializeField] private TextMeshProUGUI _commmentCountText;

    public void UpdatePreview(PostDTO post)
    {
        _nicknameText.text = post.AuthorNickname;
        _contentText.text = post.Content;
        _likeCountText.text = post.LikeCount.ToString();
        _commmentCountText.text = post.CommentCount.ToString();
    }
}
