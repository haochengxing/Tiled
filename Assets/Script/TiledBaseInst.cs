using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class TiledBaseInst : MonoBehaviour
{
    public Material material;
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;
    private Mesh mesh;
    private Vector3[] vertices;
    private Vector3[] normals;
    private int[] triangles;
    private Vector2[] uvs;
    private Vector2 rectSize;
    private int cellSize = 2;

    private Vector3 center;
    private Vector3 size;
    private Vector2 pos;

    private DataMgr mgr = new DataMgr();

    public Camera moveCamera;
    private Vector3 movePos;
    private float offset;

    private void Awake()
    {
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = material;
        meshFilter = gameObject.AddComponent<MeshFilter>();
        mesh = new Mesh();
        meshFilter.mesh = mesh;
    }




    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        TextAsset obj = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Terrain.bytes");

        mgr.DeserializeTerrainData(obj);
#endif
        Init(new Vector2(3, 3));

        if (moveCamera)
        {
            movePos = moveCamera.transform.position;
            offset = movePos.z;
        }
        ReBuildMesh(0, new Vector2(movePos.x, movePos.z- offset));

    }


    public void Init(Vector2 rect)
    {
        meshRenderer.shadowCastingMode = ShadowCastingMode.Off;

        rectSize = rect;

        int quadCount = (int)rect.x * (int)rect.y;

        vertices = new Vector3[quadCount * 4];
        normals = new Vector3[quadCount * 4];
        triangles = new int[quadCount * 6];
        uvs = new Vector2[quadCount * 4];
    }

    Bounds bound = new Bounds();

    private void SetMeshBounds(Vector2 center, Vector2 size)
    {
        this.center.Set(center.x, 0, center.y);
        this.size.Set(size.x * 2, 0, size.y * 2);
        bound.center = this.center;
        bound.size = this.size;
        mesh.bounds = bound;
    }


    public void CreateQuadToArrays(Vector2 pos,int index,uint uvindex)
    {
        int vIndex = index * 4;
        int vindex0 = vIndex;
        int vindex1 = vIndex+1;
        int vindex2 = vIndex+2;
        int vindex3 = vIndex+3;

        //左下，左上，右上，右下
        vertices[vindex0].Set(pos.x,0,pos.y);
        vertices[vindex1].Set(pos.x,0,pos.y+2);
        vertices[vindex2].Set(pos.x+2,0,pos.y+2);
        vertices[vindex3].Set(pos.x+2,0,pos.y);

        normals[vindex0] = Vector3.up;
        normals[vindex1] = Vector3.up;
        normals[vindex2] = Vector3.up;
        normals[vindex3] = Vector3.up;

        if (uvindex <= 0)
        {
            int tIndex = index * 6;
            triangles[tIndex] = 0;
            triangles[tIndex+1] = 0;
            triangles[tIndex+2] = 0;
            triangles[tIndex+3] = 0;
            triangles[tIndex+4] = 0;
            triangles[tIndex+5] = 0;
        }
        else
        {
            
            uint kTMXTileHorizontalFlag = TMXTile.kTMXTileHorizontalFlag;
            uint kTMXTileVerticalFlag = TMXTile.kTMXTileVerticalFlag;
            uint kTMXTileDiagonalFlag = TMXTile.kTMXTileDiagonalFlag;
            uint kFlippedMask = TMXTile.kFlippedMask;
            
            int f0=vindex0;
            int f1=vindex1;
            int f2=vindex2;
            int f3=vindex3;
            
            if ((uvindex & kTMXTileDiagonalFlag)!=0)
            {
                // put the anchor in the middle for ease of rotation.
                    
                uint flag = uvindex & (kTMXTileHorizontalFlag | kTMXTileVerticalFlag );
                    
                // handle the 4 diagonally flipped states.
                if (flag == kTMXTileHorizontalFlag)
                {
                    //Debug.Log("旋转90");
                    f0=vindex1;
                    f1=vindex2;
                    f2=vindex3;
                    f3=vindex0;
                }
                else if (flag == kTMXTileVerticalFlag)
                {
                    //Debug.Log("旋转270");
                    f0=vindex3;
                    f1=vindex0;
                    f2=vindex1;
                    f3=vindex2;
                }
                else if (flag == (kTMXTileVerticalFlag | kTMXTileHorizontalFlag) )
                {
                    //Debug.Log("旋转90 && x轴翻转");
                    f0=vindex0;
                    f1=vindex3;
                    f2=vindex2;
                    f3=vindex1;
                }
                else
                {
                    //Debug.Log("旋转270 && x轴翻转");
                    f0=vindex2;
                    f1=vindex1;
                    f2=vindex0;
                    f3=vindex3;
                }
            }
            else
            {
                if ((uvindex & kTMXTileHorizontalFlag)!=0)
                {
                    //Debug.Log("x轴翻转");
                    f0=vindex3;
                    f1=vindex2;
                    f2=vindex1;
                    f3=vindex0;
                }
                    
                if ((uvindex & kTMXTileVerticalFlag)!=0)
                {
                    //Debug.Log("y轴翻转");
                    f0=vindex1;
                    f1=vindex0;
                    f2=vindex3;
                    f3=vindex2;
                }
            }
            
            uvindex = (uvindex & kFlippedMask) >> 0;
            
            uvindex = uvindex - 1;
            float x = uvindex % 16;
            float y = Mathf.FloorToInt(uvindex/16);
            float offset = 1;

            uvs[f0].Set((x* 128 + offset)/2048f,(((16-y-1)* 128) +offset)/ 2048f);
            uvs[f1].Set((x* 128 + offset)/ 2048f, (((16-y)* 128) -offset)/ 2048f);
            uvs[f2].Set((((x+1)* 128) - offset)/ 2048f, (((16-y)* 128) -offset)/ 2048f);
            uvs[f3].Set((((x+1)* 128) - offset)/ 2048f, (((16-y-1)* 128) +offset)/ 2048f);


            int tIndex = index * 6;
            triangles[tIndex] = vindex0;
            triangles[tIndex + 1] = vindex1;
            triangles[tIndex + 2] = vindex2;
            triangles[tIndex + 3] = vindex2;
            triangles[tIndex + 4] = vindex3;
            triangles[tIndex + 5] = vindex0;
        }
    }


    public void ReBuildMesh(int layerIndex,Vector2 center)
    {
        SetMeshBounds(center, rectSize);
        Vector2 bottomLeftPos = center - rectSize;
        int index = 0;
        for (int x = 0; x < rectSize.x; x++)
        {
            for (int y = 0; y < rectSize.y; y++)
            {
                pos.Set(bottomLeftPos.x + x * cellSize, bottomLeftPos.y + y * cellSize);
                uint uvIndex = mgr.GetUVIndexByPosition(layerIndex, pos);
                CreateQuadToArrays(pos,index,uvIndex);
                index++;
            }
        }
        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.uv = uvs;
        mesh.triangles = triangles;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCamera)
        {
            if (movePos!= moveCamera.transform.position)
            {
                movePos = moveCamera.transform.position;
                if(movePos.x<-2||movePos.x>3|| movePos.z - offset < -2 || movePos.z - offset > 3)
                {
                    return;
                }
                ReBuildMesh(0, new Vector2(movePos.x, movePos.z - offset));
            }
            
        }
        

    }
}
