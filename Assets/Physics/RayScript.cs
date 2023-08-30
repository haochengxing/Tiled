using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // 射线的起点（Sphere1的位置）
            Vector3 origin = transform.position;
            // 射线的方向（X轴负方向）
            Vector3 direction = Vector3.left;
            // 射线碰撞的目标对象
            //RaycastHit hitInfo;
            // 射线的最大长度
            float maxDistance = 100;
            
            // 创建射线，返回是否检测到碰撞对象
            //bool raycast = Physics.Raycast(origin, direction, out hitInfo, maxDistance);
            RaycastHit[] hitInfos = Physics.RaycastAll(origin, direction, maxDistance);


            // 如果发生碰撞，碰撞信息就被存储到 hitInfo 中
            //if (raycast)
            foreach(RaycastHit hitInfo in hitInfos)
            {
                // 获取碰撞点坐标
                Vector3 point = hitInfo.point;
                Debug.Log("碰撞点坐标：" + point);

                // 获取碰撞目标的名称
                string name = hitInfo.collider.name;
                Debug.Log("碰撞对象名称：" + name);

                // 获取目标的碰撞体组件
                Collider coll = hitInfo.collider;

                //获取目标的Transgorm组件
                Transform trans = hitInfo.transform;
            }

            // 画一条蓝线来模拟射线
            Debug.DrawRay(origin, direction * 100, Color.blue, 100);
        }
    }
}
