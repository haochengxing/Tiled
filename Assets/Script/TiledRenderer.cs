using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Rendering;

namespace Script
{
    /// <summary>
    /// Tiled渲染脚本，直接解析tmx文件
    /// </summary>
    [ExecuteInEditMode]
    [Serializable]
    public class TiledRenderer : MonoBehaviour
    {

        private MeshRenderer _meshRenderer;
        private MeshFilter _meshFilter;
        private Mesh _mesh;
        private Vector3[] _vertices;
        private Vector3[] _normals;
        private int[] _triangles;
        private Vector2[] _uvs;
        private Vector2 _rectSize;
        private Bounds _bound;
        private Vector2 _pos;
        private Vector2Int _mapAreaSize;
        private Vector2 _textureSize;
        private Vector2 _cellSize;
        private Vector2 _tileSize;
        private int _max;

        //层的id
        //[LabelText("图层Index")]
        public int tiledIndex;
        //json的文件
        //[LabelText("tmx另存为json格式，后缀改为.bytes")]
        public TextAsset tiledFile;
        //材质球
        //[LabelText("BundleAssets/Tiled/Materials目录的材质球，shader设置Fly/Water，图片是tmx对应的图片")]
        public Material material;
        //运行时数据
        private uint[,] _uvData;
        //图片名称
        private string _textureName = string.Empty;
        //shader名称
        public static readonly string ShaderName = "Unlit/textures";

        private void InitRenderer()
        {
            //渲染
            _meshRenderer = gameObject.GetComponent<MeshRenderer>();
            if (_meshRenderer == null)
            {
                _meshRenderer = gameObject.AddComponent<MeshRenderer>();
            }
            _meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
            _meshRenderer.receiveShadows = false;
            _meshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
            _meshRenderer.lightProbeUsage = LightProbeUsage.Off;

            //材质
            _meshRenderer.material = material;

            //网格
            _meshFilter = gameObject.GetComponent<MeshFilter>();
            if (_meshFilter == null)
            {
                _meshFilter = gameObject.AddComponent<MeshFilter>();
            }
            _mesh = new Mesh();
            _meshFilter.mesh = _mesh;

            //解析json文件
            if (tiledFile != null)
            {
                ConvertJson(tiledFile.text);
            }

            //图片尺寸
            if (material != null && material.mainTexture != null)
            {
                Texture mainTexture = material.mainTexture;
                _textureSize.x = mainTexture.width;
                _textureSize.y = mainTexture.height;

                _cellSize.x = _textureSize.x / _tileSize.x;
                _cellSize.y = _textureSize.y / _tileSize.y;

                _max = (int)(_cellSize.x * _cellSize.y) * 2;
            }
        }


        private void ConvertJson(string json)
        {
            //解析json文件
            MapData mapData = JsonConvert.DeserializeObject<MapData>(json);

            //检查tiledIndex

            if (tiledIndex < 0 || tiledIndex >= mapData.layers.Length)
            {
                Debug.LogError(tiledIndex + "不能比0小，不能超过" + mapData.layers.Length);
                return;
            }

            _mapAreaSize.x = mapData.width;
            _mapAreaSize.y = mapData.height;

            _tileSize.x = mapData.tilewidth;
            _tileSize.y = mapData.tileheight;

            //string name = Path.GetFileNameWithoutExtension(mapData.tilesets[0].source);
            
            uint kFlippedMask = TMXTile.kFlippedMask;

            int firstgid = -1;
            int gidIndex = -1;

            int[] gidArr = new int[mapData.tilesets.Count];
            for (int i = 0; i < mapData.tilesets.Count; i++)
            {
                gidArr[i] = mapData.tilesets[i].firstgid - 1;
            }

            LayerData layerData = mapData.layers[tiledIndex];

            //运行时数据
            _uvData = new uint[layerData.width, layerData.height];

            for (int x = 0; x < layerData.width; x++)
            {
                for (int y = 0; y < layerData.height; y++)
                {

                    int index = (layerData.height - 1 - y) * layerData.width + x;

                    uint id = layerData.data[index];

                    if (id>0)
                    {
                        uint temp = (id & kFlippedMask) >> 0;
                        
                        if (firstgid==-1)
                        {
                            for (int i = gidArr.Length-1; i >= 0; i--)
                            {
                                if (temp>gidArr[i])
                                {
                                    firstgid = gidArr[i];
                                    gidIndex = i;
                                    break;
                                }
                            }
                        }
                        
                        id -= (uint)firstgid;
                    }

                    _uvData[x, y] = id;
                }
            }

            _textureName = Path.GetFileNameWithoutExtension(mapData.tilesets[gidIndex].source);

            Debug.Log(_textureName);
        }


        private uint GetUVIndexByPosition(Vector2 position)
        {
            position += _mapAreaSize;
            int x = Mathf.FloorToInt(position.x / 2);
            int y = Mathf.FloorToInt(position.y / 2);
            uint uvIndex = _uvData[x, y];
            return uvIndex;
        }

        // Start is called before the first frame update
        [ContextMenu("生成TiledMap", false, 10)]
        private void Start()
        {
            if (Check() == false)
            {
                Debug.LogError("TiledRenderer的参数设置错误");
                return;
            }

            InitRenderer();
            
            if (Check2() == false)
            {
                Debug.LogError("TiledRenderer的参数设置错误");
                return;
            }

            Init(_mapAreaSize);

            ReBuildMesh(Vector2.zero);
        }

        //创建矩形区域
        private void Init(Vector2 rect)
        {
            _rectSize = rect;

            int quadCount = (int)rect.x * (int)rect.y;

            _vertices = new Vector3[quadCount * 4];
            _normals = new Vector3[quadCount * 4];
            _triangles = new int[quadCount * 6];
            _uvs = new Vector2[quadCount * 4];
        }

        //设置网格范围
        private void SetMeshBounds(Vector2 center, Vector2 size)
        {
            _bound.center.Set(center.x, 0, center.y);
            _bound.size.Set(size.x * 2, 0, size.y * 2);
            _mesh.bounds = _bound;
        }

        //创建面片
        private void CreateQuadToArrays(Vector2 pos, int index, uint uvindex)
        {
            int vIndex = index * 4;
            int vindex0 = vIndex;
            int vindex1 = vIndex + 1;
            int vindex2 = vIndex + 2;
            int vindex3 = vIndex + 3;

            //左下，左上，右上，右下
            _vertices[vindex0].Set(pos.x, 0, pos.y);
            _vertices[vindex1].Set(pos.x, 0, pos.y + 2);
            _vertices[vindex2].Set(pos.x + 2, 0, pos.y + 2);
            _vertices[vindex3].Set(pos.x + 2, 0, pos.y);

            _normals[vindex0] = Vector3.up;
            _normals[vindex1] = Vector3.up;
            _normals[vindex2] = Vector3.up;
            _normals[vindex3] = Vector3.up;

            if (uvindex <= 0)
            {
                int tIndex = index * 6;
                _triangles[tIndex] = 0;
                _triangles[tIndex + 1] = 0;
                _triangles[tIndex + 2] = 0;
                _triangles[tIndex + 3] = 0;
                _triangles[tIndex + 4] = 0;
                _triangles[tIndex + 5] = 0;
            }
            else
            {
                int f0 = vindex0;
                int f1 = vindex1;
                int f2 = vindex2;
                int f3 = vindex3;

                if (uvindex > _max)
                {
                    //UV翻转
                    uvindex = GetUVindex(uvindex, ref f0, ref f1, ref f2, ref f3);
                }

                float row = _cellSize.x;

                float size = _tileSize.x;

                uvindex -= 1;
                float x = (int)uvindex % row;
                float y = Mathf.FloorToInt(uvindex / row);
                float offset = 0f;

                float width = _textureSize.x;
                float height = _textureSize.y;

                _uvs[f0].Set((x * size + offset) / width, (((row - y - 1) * size) + offset) / height);
                _uvs[f1].Set((x * size + offset) / width, (((row - y) * size) - offset) / height);
                _uvs[f2].Set((((x + 1) * size) - offset) / width, (((row - y) * size) - offset) / height);
                _uvs[f3].Set((((x + 1) * size) - offset) / width, (((row - y - 1) * size) + offset) / height);

                int tIndex = index * 6;
                _triangles[tIndex] = vindex0;
                _triangles[tIndex + 1] = vindex1;
                _triangles[tIndex + 2] = vindex2;
                _triangles[tIndex + 3] = vindex2;
                _triangles[tIndex + 4] = vindex3;
                _triangles[tIndex + 5] = vindex0;
            }
        }

        //获取翻转
        private uint GetUVindex(uint uvindex, ref int f0, ref int f1, ref int f2, ref int f3)
        {
            int vindex0 = f0;
            int vindex1 = f1;
            int vindex2 = f2;
            int vindex3 = f3;

            uint kTmxTileHorizontalFlag = TMXTile.kTMXTileHorizontalFlag;
            uint kTmxTileVerticalFlag = TMXTile.kTMXTileVerticalFlag;
            uint kTmxTileDiagonalFlag = TMXTile.kTMXTileDiagonalFlag;
            uint kFlippedMask = TMXTile.kFlippedMask;

            if ((uvindex & kTmxTileDiagonalFlag) != 0)
            {
                // put the anchor in the middle for ease of rotation.

                uint flag = uvindex & (kTmxTileHorizontalFlag | kTmxTileVerticalFlag);

                // handle the 4 diagonally flipped states.
                if (flag == kTmxTileHorizontalFlag)
                {
                    //Debug.Log("旋转90");
                    f0 = vindex1;
                    f1 = vindex2;
                    f2 = vindex3;
                    f3 = vindex0;
                }
                else if (flag == kTmxTileVerticalFlag)
                {
                    //Debug.Log("旋转270");
                    f0 = vindex3;
                    f1 = vindex0;
                    f2 = vindex1;
                    f3 = vindex2;
                }
                else if (flag == (kTmxTileVerticalFlag | kTmxTileHorizontalFlag))
                {
                    //Debug.Log("旋转90 && x轴翻转");
                    f0 = vindex0;
                    f1 = vindex3;
                    f2 = vindex2;
                    f3 = vindex1;
                }
                else
                {
                    //Debug.Log("旋转270 && x轴翻转");
                    f0 = vindex2;
                    f1 = vindex1;
                    f2 = vindex0;
                    f3 = vindex3;
                }
            }
            else
            {
                if ((uvindex & kTmxTileHorizontalFlag) != 0)
                {
                    //Debug.Log("x轴翻转");
                    f0 = vindex3;
                    f1 = vindex2;
                    f2 = vindex1;
                    f3 = vindex0;
                }

                if ((uvindex & kTmxTileVerticalFlag) != 0)
                {
                    int a0=f0;
                    int a1=f1;
                    int a2=f2;
                    int a3=f3;
                    
                    //Debug.Log("y轴翻转");
                    f0 = a1;
                    f1 = a0;
                    f2 = a3;
                    f3 = a2;
                }
            }

            uvindex = (uvindex & kFlippedMask) >> 0;

            return uvindex;
        }

        //设置三角形
        private void ReBuildMesh(Vector2 center)
        {
            SetMeshBounds(center, _rectSize);
            Vector2 bottomLeftPos = center - _rectSize;
            int index = 0;
            for (int x = 0; x < _rectSize.x; x++)
            {
                for (int y = 0; y < _rectSize.y; y++)
                {
                    _pos.Set(bottomLeftPos.x + x * 2, bottomLeftPos.y + y * 2);
                    uint uvIndex = GetUVIndexByPosition(_pos);
                    CreateQuadToArrays(_pos, index, uvIndex);
                    index++;
                }
            }
            _mesh.vertices = _vertices;
            _mesh.normals = _normals;
            _mesh.uv = _uvs;
            _mesh.triangles = _triangles;
        }

        // Update is called once per frame
        // private void Update()
        // {
        // }

        //正确性的检查
        private bool Check()
        {
            if (tiledFile == null)
            {
                Debug.LogError("TiledMap的json文件设置，后缀必须.bytes");
                return false;
            }

            if (material == null || material.mainTexture == null || material.shader.name.Equals(ShaderName) == false)
            {
                Debug.LogError("材质球没有设置，图片是tmx对应的图片，shader必须Fly/Water");
                return false;
            }
            
            return true;
        }
        
        private bool Check2()
        {
            if (_uvData == null)
            {
                Debug.LogError("tiledIndex参数错误，数据未初始化");
                return false;
            }
            
            if (material.mainTexture.name.Equals(_textureName)==false)
            {
                Debug.LogError("材质球的图片需要设置为"+_textureName);
                return false;
            }
            
            return true;
        }
    }
}
