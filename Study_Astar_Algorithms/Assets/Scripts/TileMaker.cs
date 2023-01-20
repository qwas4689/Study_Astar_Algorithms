using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMaker : MonoBehaviour
{
    [SerializeField] private GameObject[] _tilePrefabs;
    [SerializeField] private GameObject _tileGroup;
    [SerializeField] private Json _json;

    private const int MOVE_TILE = 0;
    private const int BLOCK_TILE = 1;

    private Vector2 _tilePosition;

    private void Start()
    {
        MakeField();
    }

    /// <summary>
    /// 타일 생성기
    /// </summary>
    private void MakeField()
    {
        int x = 0;
        int y = 0;

        for (int i = 0; i < _json.YTileCount; ++i)
        {
            for (int j = 0; j < _json.XTileCount; ++j)
            {
                _tilePosition = new Vector2(x, y);

                if (x == 10 && y == 10)
                {
                    GameObject playerTile = Instantiate(_tilePrefabs[MOVE_TILE], _tilePosition, Quaternion.identity);

                    playerTile.transform.SetParent(_tileGroup.transform, playerTile.transform);

                    x += 1;
                    continue;
                }

                int tileDestiny = Random.Range(0, 10);

                GameObject tile = tileDestiny <= 1 ? Instantiate(_tilePrefabs[BLOCK_TILE], _tilePosition, Quaternion.identity) : Instantiate(_tilePrefabs[MOVE_TILE], _tilePosition, Quaternion.identity);


                tile.transform.SetParent(_tileGroup.transform, tile.transform);


                x += 1;
            }

            y += 1;
            x = 0;
        }
    }
}
