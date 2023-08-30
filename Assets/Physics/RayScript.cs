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
            // ���ߵ���㣨Sphere1��λ�ã�
            Vector3 origin = transform.position;
            // ���ߵķ���X�Ḻ����
            Vector3 direction = Vector3.left;
            // ������ײ��Ŀ�����
            //RaycastHit hitInfo;
            // ���ߵ���󳤶�
            float maxDistance = 100;
            
            // �������ߣ������Ƿ��⵽��ײ����
            //bool raycast = Physics.Raycast(origin, direction, out hitInfo, maxDistance);
            RaycastHit[] hitInfos = Physics.RaycastAll(origin, direction, maxDistance);


            // ���������ײ����ײ��Ϣ�ͱ��洢�� hitInfo ��
            //if (raycast)
            foreach(RaycastHit hitInfo in hitInfos)
            {
                // ��ȡ��ײ������
                Vector3 point = hitInfo.point;
                Debug.Log("��ײ�����꣺" + point);

                // ��ȡ��ײĿ�������
                string name = hitInfo.collider.name;
                Debug.Log("��ײ�������ƣ�" + name);

                // ��ȡĿ�����ײ�����
                Collider coll = hitInfo.collider;

                //��ȡĿ���Transgorm���
                Transform trans = hitInfo.transform;
            }

            // ��һ��������ģ������
            Debug.DrawRay(origin, direction * 100, Color.blue, 100);
        }
    }
}
