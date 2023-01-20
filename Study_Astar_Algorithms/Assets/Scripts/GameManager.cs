using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("플레이어의 초기 위치")]
    [SerializeField] private Vector2 _playerPos;

    [SerializeField] private GameObject _playerPrefab;

    void Start()
    {
        Instantiate(_playerPrefab, _playerPos, Quaternion.identity);
    }

    void Update()
    {
        
    }
}
