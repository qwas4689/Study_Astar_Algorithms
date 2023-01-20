using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    /*
     * 뭐할거냐먼 먼저 자식에 있는것들을 이름으로 분류해서 MoveTile(Clone)는 1(열린타일)으로 BlockTile(Clone)는 0(잠금타일)으로 1차원 배열(타일배열)에 저장 해 놓고
     * 배열의 인덱스를 키값으로 가지고 int G(시작점부터 현재 위치까지 이동한 거리), , int H (너비우선 탐색으로 나온 예상 거리), int F( G + H )
     * 3가지를 벨류로 가지는 딕셔너리(길찾기 기록)를 만든다
     * 시작할 위치의 타일을 잠금타일로 바꾸고 룩업 테이블을 사용해 8방향을 탐색, 타일배열의 인덱스를 받아와 1이면 길찾기 기록에 저장하고 0이면 리턴한다
     * F가 최소인 지점으로 이동 해 8방향을 탐색 잠금타일은 리턴하고 열린타일들을 검색 여태 이동한 G값과 그 타일의 G값을 비교해 작은 쪽으로 이동한다(더 작은쪽으로 옮기고 잠금타일로 바꾼다)
     * 위 내용을 도착지점을 탐색범위에 포함될 때 까지 반복한다
     * 
     * 도착지점부터 나온 길을 역순으로 스택에 담아 출발지점에서 출발시킨다
     */

    // 내가 있는 인덱스 x + y*20

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
