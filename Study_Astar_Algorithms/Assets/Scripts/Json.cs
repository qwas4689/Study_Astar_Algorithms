using System;
using UnityEngine;

public class Json : MonoBehaviour
{
    [Serializable]
    public class Parser
    {
        public int XTileCount;
        public int YTileCount;
    }

    private TextAsset _textAsset;
    private Parser _parser;
    private const string _tileCounts = "TileCounts";

    public int XTileCount { get; private set; }
    public int YTileCount { get; private set; }

    private void Awake()
    {
        _textAsset = Resources.Load<TextAsset>(_tileCounts);
        _parser = JsonUtility.FromJson<Parser>(_textAsset.ToString());

        XTileCount = _parser.XTileCount;
        YTileCount = _parser.YTileCount;
    }
}
