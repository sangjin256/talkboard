using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class UI_Comment : MonoBehaviour
{
    [SerializeField] private GameObject CommentSlotPrefab;
    private List<UI_CommentSlot> _commentSlotList;
}
