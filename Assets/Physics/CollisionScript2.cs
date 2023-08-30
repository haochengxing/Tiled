using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript2 : MonoBehaviour
{

    // 碰撞进行时变量
    private bool isTriggerFlag = false;

    void Update()
    {
        // 移动加入isTriggerFlag条件
        if (Input.GetKeyDown(KeyCode.D) && !isTriggerFlag)
        transform.Translate(new Vector3(0.5f, 0, 0));
    }

    void OnCollisionEnter(Collision collision)
    {
        // 打印碰撞游戏对象的名称
        Debug.Log("碰撞游戏对象的名称 " + collision.gameObject.name + " 发生碰撞");
    }

    void OnTriggerEnter(Collider other)
    {
        isTriggerFlag = true;
        Debug.Log("碰撞到了 " + other.gameObject.name + " 对象！");
    }

    void OnTriggerExit(Collider other)
    {
        isTriggerFlag = false;
    }
}
