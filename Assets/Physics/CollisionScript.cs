using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // 打印碰撞游戏对象的名称
        Debug.Log("当前对象 " + gameObject.name + " 与 " + collision.gameObject.name + " 发生碰撞");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("当前对象被" + other.gameObject.name + "碰撞到了！");
    }

    void OnTriggerStay(Collider other)
    {
        // 去掉地面碰撞的情况
        if (other.gameObject.name == "Sphere2")
        transform.Translate(0.1f, 0, 0);
    }
}
