using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript2 : MonoBehaviour
{

    // ��ײ����ʱ����
    private bool isTriggerFlag = false;

    void Update()
    {
        // �ƶ�����isTriggerFlag����
        if (Input.GetKeyDown(KeyCode.D) && !isTriggerFlag)
        transform.Translate(new Vector3(0.5f, 0, 0));
    }

    void OnCollisionEnter(Collision collision)
    {
        // ��ӡ��ײ��Ϸ���������
        Debug.Log("��ײ��Ϸ��������� " + collision.gameObject.name + " ������ײ");
    }

    void OnTriggerEnter(Collider other)
    {
        isTriggerFlag = true;
        Debug.Log("��ײ���� " + other.gameObject.name + " ����");
    }

    void OnTriggerExit(Collider other)
    {
        isTriggerFlag = false;
    }
}
