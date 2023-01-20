using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    /*
     * ���Ұųĸ� ���� �ڽĿ� �ִ°͵��� �̸����� �з��ؼ� MoveTile(Clone)�� 1(����Ÿ��)���� BlockTile(Clone)�� 0(���Ÿ��)���� 1���� �迭(Ÿ�Ϲ迭)�� ���� �� ����
     * �迭�� �ε����� Ű������ ������ int G(���������� ���� ��ġ���� �̵��� �Ÿ�), , int H (�ʺ�켱 Ž������ ���� ���� �Ÿ�), int F( G + H )
     * 3������ ������ ������ ��ųʸ�(��ã�� ���)�� �����
     * ������ ��ġ�� Ÿ���� ���Ÿ�Ϸ� �ٲٰ� ��� ���̺��� ����� 8������ Ž��, Ÿ�Ϲ迭�� �ε����� �޾ƿ� 1�̸� ��ã�� ��Ͽ� �����ϰ� 0�̸� �����Ѵ�
     * F�� �ּ��� �������� �̵� �� 8������ Ž�� ���Ÿ���� �����ϰ� ����Ÿ�ϵ��� �˻� ���� �̵��� G���� �� Ÿ���� G���� ���� ���� ������ �̵��Ѵ�(�� ���������� �ű�� ���Ÿ�Ϸ� �ٲ۴�)
     * �� ������ ���������� Ž�������� ���Ե� �� ���� �ݺ��Ѵ�
     * 
     * ������������ ���� ���� �������� ���ÿ� ��� ����������� ��߽�Ų��
     */

    // ���� �ִ� �ε��� x + y*20

    [SerializeField] private Json json;
    [SerializeField] private Pointer _pointer;

    private List<GameObject> _openTiles = new List<GameObject>();
    private List<GameObject> _closeTiles = new List<GameObject>();

    private List<int> _ghf = new List<int>();

    private bool[] _tileGroup;
    private int _totalTileCounts;
    private int _setMoveTileCount;
    private const string MOVE_TILE = "MoveTile(Clone)";

    private int[] LOOKUP_TABLE = { 20, 21, 1, -19, -20, -21, -1, +19 };

    private int _playerPosX;
    private int _playerPosY;

    private int _xValue;
    private int _yValue;

    private int _gXValue;
    private int _gYValue;

    private int _minDistanse;
    private int _index;

    private void Start()
    {
        _totalTileCounts = json.XTileCount * json.YTileCount;

        _tileGroup = new bool[_totalTileCounts];

        _playerPosX = Mathf.RoundToInt(GameManager.Instance.PlayerPos.x);
        _playerPosY = Mathf.RoundToInt(GameManager.Instance.PlayerPos.y);

        _pointer.OnClickMouseButton.RemoveListener(() => ManhattanDistance(_openTiles, _pointer.MousePositionX, _pointer.MousePositionY));
        _pointer.OnClickMouseButton.AddListener(() => ManhattanDistance(_openTiles, _pointer.MousePositionX, _pointer.MousePositionY));
    }

    private void Update()
    {
        if (_totalTileCounts != _setMoveTileCount)
        {
            for (int i = 0; i < _totalTileCounts; ++i)
            {
                _tileGroup[i] = transform.GetChild(i).name == MOVE_TILE ? true : false;

                ++_setMoveTileCount;
            }

            _closeTiles.Add(transform.GetChild(PlayerTileIndex()).gameObject);

            MaybeAStar(_openTiles, PlayerTileIndex());
        }
    }

    /// <summary>
    /// �÷��̾� ��ǥ ���ϱ�
    /// </summary>
    /// <returns></returns>
    private int PlayerTileIndex()
    {
        return _playerPosX + _playerPosY * json.YTileCount;
    }

    /// <summary>
    /// �ֺ� Ž��
    /// </summary>
    /// <param name="openTile"></param>
    private void MaybeAStar(List<GameObject> openTile, int playerPositionIndex)
    {
        // ó�� �÷��̾ �ִ� Ÿ���� �޾ƿ��ݾ� ��� �ε������� ���ݾ� �װ� ���� ������̺��� ������ ���� �͵��� �ε����� ����Ÿ�Ϸ� �־���
        for (int LookUpTableIndex = 0; LookUpTableIndex < LOOKUP_TABLE.Length; ++LookUpTableIndex)
        {
            if (_tileGroup[playerPositionIndex + LOOKUP_TABLE[LookUpTableIndex]])
            {
                openTile.Add(transform.GetChild(playerPositionIndex + LOOKUP_TABLE[LookUpTableIndex]).gameObject);
            }
        }
    }

    private void ManhattanDistance(List<GameObject> tileList, int goalX, int goalY)
    {
        
            for (int i = 0; i < tileList.Count; ++i)
            {
                int x = Mathf.RoundToInt(tileList[i].transform.position.x);
                int y = Mathf.RoundToInt(tileList[i].transform.position.y);

                _xValue = goalX < x ? x - goalX : goalX - x;
                _yValue = goalY < y ? y - goalY : goalY - y;

                // ���ǹ� Ŭ���� Ÿ���� �޶��� �� �� �ؾ��ҵ�
                if (tileList[i].GetComponent<TileGHF>().H != 0)
                {
                    continue;
                }
                else
                {
                    tileList[i].GetComponent<TileGHF>().H = _xValue + _yValue;

                    _gXValue = _playerPosX < x ? x - _playerPosX : _playerPosX - x;
                    _gYValue = _playerPosY < y ? y - _playerPosY : _playerPosY - y;

                    tileList[i].GetComponent<TileGHF>().G = _gXValue + _gYValue;
                    tileList[i].GetComponent<TileGHF>().F = tileList[i].GetComponent<TileGHF>().H + tileList[i].GetComponent<TileGHF>().G;

                    if (_minDistanse == 0)
                    {
                        _minDistanse = Mathf.RoundToInt(tileList[i].transform.position.x + tileList[i].transform.position.y * json.YTileCount);
                        _index = Mathf.RoundToInt(tileList[i].transform.position.x + tileList[i].transform.position.y * json.YTileCount);
                    }
                    else
                    {
                        _minDistanse = _minDistanse < tileList[i].GetComponent<TileGHF>().F ? _minDistanse : tileList[i].GetComponent<TileGHF>().F; 
                        _index = _minDistanse < tileList[i].GetComponent<TileGHF>().F ? _index : Mathf.RoundToInt(tileList[i].transform.position.x + tileList[i].transform.position.y * json.YTileCount);
                    }
                    
                }
            }

            _closeTiles.Add(transform.GetChild(_index).gameObject);

            _openTiles.Clear();

            MaybeAStar(_openTiles, _index);


            
        
    }
}
