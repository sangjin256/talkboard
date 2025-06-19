using Firebase;
using Firebase.Extensions;
using UnityEngine;
using System.Collections.Generic;
using System;
using Firebase.Auth;
using Firebase.Firestore;
using System.Threading.Tasks;

public class FirebaseManager : BehaviourSingleton<FirebaseManager>
{
    private FirebaseApp _app;
    public FirebaseApp App => _app;
    private FirebaseAuth _auth;
    public FirebaseAuth Auth => _auth;
    private FirebaseFirestore _db;
    public FirebaseFirestore DB => _db;
    private async void Awake()
    {
        await InitAsync();
    }

    // 파이어베이스 내 프로젝트에 연결
    private async Task InitAsync()
    {
        var dependencyStatus = await Firebase.FirebaseApp.CheckAndFixDependenciesAsync();

        if (dependencyStatus == Firebase.DependencyStatus.Available)
        {
            Debug.Log("파이어베이스 연결에 성공했습니다.");
            _app = Firebase.FirebaseApp.DefaultInstance;
            _auth = FirebaseAuth.DefaultInstance;
            _db = FirebaseFirestore.DefaultInstance;
        }
        else
        {
            Debug.LogError($"파이어베이스 연결에 실패했습니다. {dependencyStatus}");
        }
    }
}
