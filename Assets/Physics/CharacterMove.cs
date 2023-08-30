using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    // 移动速度
    private float speed = 10.0f;

    // 角色控制器组件
    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //transform.Translate(Vector3.left * speed * Time.deltaTime);
            //controller.Move(Vector3.left * speed * Time.deltaTime);
            controller.SimpleMove(Vector3.left * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //transform.Translate(Vector3.right * speed * Time.deltaTime);
            //controller.Move(Vector3.right * speed * Time.deltaTime);
            controller.SimpleMove(Vector3.right * speed);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("碰撞物体为：" + hit.gameObject.name);
    }
}
