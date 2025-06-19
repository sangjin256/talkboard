using UnityEngine;

public class UI_CommentDetailScreen : MonoBehaviour
{

    [SerializeField] private GameObject DetailScreen;
    public void OnClickDetailButton()
    {
        DetailScreen.SetActive(true);
    }

}
