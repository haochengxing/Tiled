using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class TileBaseTool 
{
    [MenuItem("TileBase/Test")]
    public static void LoadFile()
    {

        string file = File.ReadAllText("Assets/Terrain.json");

        MapData mapData = JsonConvert.DeserializeObject<MapData>(file);

        List<MapLayer> layerList = new List<MapLayer>();
        for (int i = 0; i < mapData.layers.Length; i++)
        {
            MapLayer mapLayer = new MapLayer();
            layerList.Add(mapLayer);

            mapLayer.layer = i;
            mapLayer.firstgid = mapData.tilesets[i].firstgid - 1;

            LayerData layerData = mapData.layers[i];

            for (int y = 0; y < layerData.width; y++)
            {
                for (int x = 0; x < layerData.height; x++)
                {
                    Vector2Int pos = new Vector2Int(x,y);
                    int index = (layerData.height-1-y) * layerData.width + x;
                    uint id = layerData.data[index];

                    mapLayer.idDic.Add(pos,id);
                }
            }
        }

        List<short[,]> maps = new List<short[,]>();
        for (int i = 0; i < layerList.Count; i++)
        {
            MapLayer mapLayer = layerList[i];

            LayerData layerData = mapData.layers[i];

            short[,] data = new short[layerData.width,layerData.height];

            for (int y = 0; y < layerData.width; y++)
            {
                for (int x = 0; x < layerData.height; x++)
                {
                    Vector2Int index = new Vector2Int(x,y);

                    if (mapLayer.idDic.ContainsKey(index))
                    {
                        int id = (int)mapLayer.idDic[index];

                        id = id - mapLayer.firstgid;

                        data[x, y] = (short)id;
                    }
                }
            }

            maps.Add(data);
        }

        byte[] bytes = ByteUtils.Serializable(maps);
        File.WriteAllBytes("Assets/Terrain.bytes",bytes);
    }
}
