using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayClickScript : MonoBehaviour
{
    // 绿球Sphere3
    private GameObject sphere3;

    // 是否移动
    private bool isMove = false;

    // 目标点
    private Vector3 target = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        // 获取绿球Sphere3
        sphere3 = GameObject.Find("Sphere3");
    }

    // Update is called once per frame
    void Update()
    {
        // 鼠标左键按下
        if (Input.GetMouseButtonDown(0))
        {
            // 从相机位置发射一条射线经过屏幕上的鼠标点击位置
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // 声明一个射线碰撞信息类
            RaycastHit hit;

            // 进行碰撞检测
            bool res = Physics.Raycast(ray, out hit);

            // 如果产生了碰撞
            if (res && hit.transform.name == "Plane")
            {
                // 目标点(Y轴上保持不变)
                isMove = true;
                target = new Vector3(hit.point.x, sphere3.transform.position.y, hit.point.z);
                //Debug.Log("碰撞点：" + hit.point);
                //Debug.Log("碰撞目标：" + hit.transform.name);
            }
        }

        // 如果发生碰撞就让绿球移动到目标点
        if (isMove)
        {
            // 绿球朝向目标点
            sphere3.transform.LookAt(target);

            // 角色移动到目标点的距离
            float distance = Vector3.Distance(target, sphere3.transform.position);

            // 没有到达目标点就一直移动下去
            if (distance > 0.1f)
            {
                // 旋转后向前移动即可
                sphere3.transform.Translate(transform.forward * 0.2f);
            }
            else
            {
                // 移动结束
                isMove = false;
            }
        }
    }
}
