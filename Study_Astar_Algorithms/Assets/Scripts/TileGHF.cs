using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGHF : MonoBehaviour
{
    /// <summary>
    /// 시작점부터 현재 위치까지 이동한 거리
    /// </summary>
    public int G { get; set; }

    /// <summary>
    /// 현재 위치에서 도착지점까지 장애물의 방해 없이 계산한 거리
    /// </summary>
    public int H { get; set; }

    /// <summary>
    /// G + H
    /// </summary>
    public int F { get; set; }
}