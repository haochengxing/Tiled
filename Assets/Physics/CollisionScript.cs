using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // ��ӡ��ײ��Ϸ���������
        Debug.Log("��ǰ���� " + gameObject.name + " �� " + collision.gameObject.name + " ������ײ");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("��ǰ����" + other.gameObject.name + "��ײ���ˣ�");
    }

    void OnTriggerStay(Collider other)
    {
        // ȥ��������ײ�����
        if (other.gameObject.name == "Sphere2")
        transform.Translate(0.1f, 0, 0);
    }
}
