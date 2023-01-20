using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGHF : MonoBehaviour
{
    /// <summary>
    /// ���������� ���� ��ġ���� �̵��� �Ÿ�
    /// </summary>
    public int G { get; set; }

    /// <summary>
    /// ���� ��ġ���� ������������ ��ֹ��� ���� ���� ����� �Ÿ�
    /// </summary>
    public int H { get; set; }

    /// <summary>
    /// G + H
    /// </summary>
    public int F { get; set; }
}