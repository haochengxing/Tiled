using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperMapData
{
    
}

public class MapLayer
{
    public int layer;
    public int firstgid;
    public Dictionary<Vector2Int, uint> idDic;

    public MapLayer()
    {
        layer = 0;
        firstgid = 0;
        idDic = new Dictionary<Vector2Int, uint>();
    }
}
