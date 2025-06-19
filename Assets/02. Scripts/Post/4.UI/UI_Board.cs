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
}
