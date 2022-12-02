using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr
{
    private readonly Vector2Int mapAreaSize = new Vector2Int(10, 10);

    private List<short[,]> terrain = null;

    public void DeserializeTerrainData(Object obj)
    {
        TextAsset asset = obj as TextAsset;
        if (asset != null)
        {
            terrain = ByteUtils.DeSerializable(asset.bytes) as List<short[,]>;
        }
    }

    public int GetUVIndexByPosition(int index,Vector2 position)
    {
        Vector2 pos = position + (mapAreaSize/2);
        int pos_x = Mathf.FloorToInt(pos.x/2);
        int pos_y = Mathf.FloorToInt(pos.y/2);
        int uvIndex = terrain[index][pos_x, pos_y];
        return uvIndex;
    }
}
