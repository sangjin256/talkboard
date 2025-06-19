using System.Collections.Generic;
using UnityEngine;

public class UI_Board : MonoBehaviour
{
    [SerializeField] private GameObject _contentPreviewPrefab;
    [SerializeField] private Transform _contentPreviewParent;
    
    private List<UI_ContentPreview> _contentPreviewList;
    
    private void Awake()
    {
        _contentPreviewList = new List<UI_ContentPreview>();
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
