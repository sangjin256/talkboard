using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UI_Board : MonoBehaviour
{
    [SerializeField] private GameObject _postPreviewPrefab;
    [SerializeField] private Transform _postPreviewParent;
    
    private List<UI_PostPreview> _postPreviewList;
    
    private void Awake()
    {
        _postPreviewList = new List<UI_PostPreview>();
    }

    public async Task UpdatePostPreviewList()
    {
        List<PostDTO> postList = await PostManager.Instance.GetAllPostsAsync();

        SetPreviewSize(postList.Count);
        for (int i = 0; i < postList.Count; ++i)
        {
            _postPreviewList[i].UpdatePreview(postList[i]);
            _postPreviewList[i].transform.SetSiblingIndex(i);
            _postPreviewList[i].gameObject.SetActive(true);
        }
    }

    private void SetPreviewSize(int size)
    {
        if (_postPreviewList.Count < size)
        {
            for (int i = _postPreviewList.Count; i < size; ++i)
            {
                UI_PostPreview newPreview = Instantiate(_postPreviewPrefab, _postPreviewParent).GetComponent<UI_PostPreview>();
                _postPreviewList.Add(newPreview);
            }
        }
        else if (_postPreviewList.Count > size)
        {
            for (int i = _postPreviewList.Count - 1; i >= size; --i)
            {
                _postPreviewList[i].gameObject.SetActive(false);
            }
        }
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
