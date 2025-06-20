using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FirestoreTest : MonoBehaviour
{
    public async void OnClick()
    {
        AccountDTO dto = new AccountDTO("test@gmail.com", "hellos", "111111");
        await AccountManager.Instance.TryRegister(dto);
        await AccountManager.Instance.TryLogin(dto.Email, dto.Password);

        string postId = "5ysWAcilGhPvjSSjjQUH";
        await CommentManager.Instance.TryAddComment(postId, "1빠");
    }
}