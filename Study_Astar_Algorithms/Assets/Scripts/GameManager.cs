using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    [Header("�÷��̾��� �ʱ� ��ġ")]
    [SerializeField] private Vector2 _playerPos;
    public Vector2 PlayerPos { get { return _playerPos; } private set { _playerPos = value; } }

    [SerializeField] private GameObject _playerPrefab;
    
    void Awake()
    {
        Instantiate(_playerPrefab, _playerPos, Quaternion.identity);
    }
}
