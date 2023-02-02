using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoW : MonoBehaviour
{
    [Range(1, 100)]
    [SerializeField] public float depth = 1;

    void Start()
    {

    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, depth);
            Vector3 apiwpos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector3 wpos = StoWWorldPos(mousePos);
            Debug.LogFormat("mousePos = {0} apiwpos = {1} wpos = {2}", mousePos, apiwpos, wpos);
        }
    }



    //mainCamera处理local到world的TRS变换
    private Vector3 CameraLocalToWorldPos(Vector3 localpos)
    {
        return Camera.main.transform.localToWorldMatrix.MultiplyPoint3x4(localpos);
    }

    //将视口空间相对坐标转到世界坐标
    //处理TRS矩阵变换
    private Vector3 StoWWorldPos(Vector3 mousePos)
    {
        return CameraLocalToWorldPos(StoWLocalPos(mousePos));
    }

    //计算视口空间中相对坐标
    private Vector3 StoWLocalPos(Vector3 mousePos)
    {
        float halfhei = 0f;
        if (Camera.main.orthographic)
        {
            //正交
            halfhei = Camera.main.orthographicSize * depth;
        }
        else
        {
            //透视
            float halffov = Camera.main.fieldOfView / 2;
            halfhei = Mathf.Tan(halffov * Mathf.Deg2Rad) * depth;
        }
        
        float halfwid = halfhei * Camera.main.aspect;
        float ratiox = (mousePos.x - Screen.width / 2) / (Screen.width / 2);
        float ratioy = (mousePos.y - Screen.height / 2) / (Screen.height / 2);
        float localx = halfwid * ratiox;
        float localy = halfhei * ratioy;
        Vector3 localxyz = new Vector3(localx, localy, depth);
        return localxyz;
    }
}
