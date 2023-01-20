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

    private bool[] _tileGroup;
    private int _totalTileCounts;
    private int _setMoveTileCount;
    private const string MOVE_TILE = "MoveTile(Clone)";

    private int[] LOOKUP_TABLE = { 20, 21, 1, -19, -20, -21, -1, +19 };

    private void Start()
    {
        _totalTileCounts = json.XTileCount * json.YTileCount;

        _tileGroup = new bool[_totalTileCounts];
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
        }
    }
}
