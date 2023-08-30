using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayClickScript : MonoBehaviour
{
    // ����Sphere3
    private GameObject sphere3;

    // �Ƿ��ƶ�
    private bool isMove = false;

    // Ŀ���
    private Vector3 target = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        // ��ȡ����Sphere3
        sphere3 = GameObject.Find("Sphere3");
    }

    // Update is called once per frame
    void Update()
    {
        // ����������
        if (Input.GetMouseButtonDown(0))
        {
            // �����λ�÷���һ�����߾�����Ļ�ϵ������λ��
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // ����һ��������ײ��Ϣ��
            RaycastHit hit;

            // ������ײ���
            bool res = Physics.Raycast(ray, out hit);

            // �����������ײ
            if (res && hit.transform.name == "Plane")
            {
                // Ŀ���(Y���ϱ��ֲ���)
                isMove = true;
                target = new Vector3(hit.point.x, sphere3.transform.position.y, hit.point.z);
                //Debug.Log("��ײ�㣺" + hit.point);
                //Debug.Log("��ײĿ�꣺" + hit.transform.name);
            }
        }

        // ���������ײ���������ƶ���Ŀ���
        if (isMove)
        {
            // ������Ŀ���
            sphere3.transform.LookAt(target);

            // ��ɫ�ƶ���Ŀ���ľ���
            float distance = Vector3.Distance(target, sphere3.transform.position);

            // û�е���Ŀ����һֱ�ƶ���ȥ
            if (distance > 0.1f)
            {
                // ��ת����ǰ�ƶ�����
                sphere3.transform.Translate(transform.forward * 0.2f);
            }
            else
            {
                // �ƶ�����
                isMove = false;
            }
        }
    }
}
