using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("�÷��̾��� �ʱ� ��ġ")]
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
