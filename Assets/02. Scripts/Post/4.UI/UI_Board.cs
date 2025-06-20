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

    private async void Start()
    {
        await UpdatePostPreviewList();    
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
}
