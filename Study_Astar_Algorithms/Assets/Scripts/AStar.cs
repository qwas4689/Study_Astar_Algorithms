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

    private List<GameObject> _openTiles = new List<GameObject>();
    private List<GameObject> _closeTiles = new List<GameObject>();

    private List<int> _ghf = new List<int>();

    private bool[] _tileGroup;
    private int _totalTileCounts;
    private int _setMoveTileCount;
    private const string MOVE_TILE = "MoveTile(Clone)";

    private int[] LOOKUP_TABLE = { 20, 21, 1, -19, -20, -21, -1, +19 };

    private const int UP = 0;
    private const int RIGHT_UP_ANGLE = 1;
    private const int RIGHT = 2;
    private const int RIGHT_DOWN_ANGLE = 3;
    private const int DOWN = 4;
    private const int LEFT_DOWN_ANGLE = 5;
    private const int LEFT = 6;
    private const int LEFT_UP_ANGLE = 7;

    private int _playerPosX;
    private int _playerPosY;

    private IEnumerator _maybeAStar;

    private Dictionary<int, List<int>> _astarRecorder = new Dictionary<int, List<int>>();

    private void Start()
    {
        _totalTileCounts = json.XTileCount * json.YTileCount;

        _tileGroup = new bool[_totalTileCounts];

        _maybeAStar = MaybeAStar(_openTiles);

        _playerPosX = Mathf.RoundToInt(GameManager.Instance.PlayerPos.x);
        _playerPosY = Mathf.RoundToInt(GameManager.Instance.PlayerPos.y);
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

            StartCoroutine(_maybeAStar);
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
    /// ���̽�Ÿ �˰��� �ڷ�ƾ
    /// </summary>
    /// <param name="openTile"></param>
    /// <returns></returns>
    private IEnumerator MaybeAStar(List<GameObject> openTile)
    {
        // ó�� �÷��̾ �ִ� Ÿ���� �޾ƿ��ݾ� ��� �ε������� ���ݾ� �װ� ���� ������̺��� ������ ���� �͵��� �ε����� ����Ÿ�Ϸ� �־���

        for (int LookUpTableIndex = 0; LookUpTableIndex < LOOKUP_TABLE.Length; ++LookUpTableIndex)
        {
            if (_tileGroup[PlayerTileIndex() + LOOKUP_TABLE[LookUpTableIndex]])
            {
                openTile.Add(transform.GetChild(PlayerTileIndex() + LOOKUP_TABLE[LookUpTableIndex]).gameObject);
            }
        }

        yield return null;
    }

    private void ManhattanDistance(List<GameObject> tileList, int goalX, int goalY)
    {
        foreach (var a in tileList)
        {
            if (goalX < _playerPosX)
            {
                 = _playerPosX - goalX;
            }

        }
    }

}
