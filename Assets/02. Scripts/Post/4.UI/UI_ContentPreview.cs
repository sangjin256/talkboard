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
    
    /*
    private void OnEnable()
    {
        RefreshMyRankingSlot();
        RefreshRankingList();
    }
    
    public void RefreshMyRankingSlot()
    {
        int myRanking = RankingManager.Instance.GetPlayerRankNumber();
        RankingDTO myRankingData = RankingManager.Instance.GetPlayerRankData();
        _myRankingSlot.RefreshRankingSlot(myRanking, myRankingData.Nickname, myRankingData.KillCount);
    }

    public void RefreshRankingList()
    {
        List<RankingDTO> rankingList = RankingManager.Instance.GetSortedRankList(50);
        
        SetSlotSize(rankingList.Count);
        
        for (int i = 0; i < rankingList.Count; ++i)
        {
            _rankingSlotList[i].RefreshRankingSlot(
                i + 1,
                rankingList[i].Nickname,
                rankingList[i].KillCount
            );
            _rankingSlotList[i].transform.SetSiblingIndex(i);
            _rankingSlotList[i].gameObject.SetActive(true);
        }
    }

    private void SetSlotSize(int size)
    {
        if (_rankingSlotList.Count < size)
        {
            for (int i = _rankingSlotList.Count; i < size; ++i)
            {
                UI_RankingSlot newSlot = Instantiate(_rankingSlotPrefab, transform);
                newSlot.transform.SetParent(Content.transform, false);
                _rankingSlotList.Add(newSlot);
            }
        }
        else if (_rankingSlotList.Count > size)
        {
            for (int i = _rankingSlotList.Count - 1; i >= size; --i)
            {
                _rankingSlotList[i].gameObject.SetActive(false);
            }
        }
    }
    */
}
