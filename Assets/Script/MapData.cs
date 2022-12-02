using System.Collections.Generic;

public class MapData
{
    public int compressionLevel;
    public int height;
    public bool infinite;
    public LayerData[] layers;

    public int nextlayerid;
    public int nextobjectid;
    public string orientation;
    public string renderorder;
    public string tiledversion;
    public int tileheight;

    public List<TileSource> tilesets;
    public int tilewidth;
    public string type;
    public float version;
    public int width;

    public int level = 0;
}

public class LayerData
{
    public List<uint> data;
    public int height;
    public int id;
    public string name;
    public int opacity;
    public string type;
    public bool visible;
    public int width;
    public int x;
    public int y;
}

public class TileSource
{
    public int firstgid;
    public string source;
    public TileSource(int id,string name)
    {
        firstgid = id;
        source = name;
    }
}
