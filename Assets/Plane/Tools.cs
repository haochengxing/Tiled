using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{

    public GameObject plane;

    public Camera mCamera;
    public GameObject cube0;
    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;

    // Start is called before the first frame update
    void Start()
    {
        //GetPosition();

        //DrawRay();

        GetPerspectivePosition();
    }

    // Update is called once per frame
    void Update()
    {
        //GetPosition();

        //DrawRay();

        GetPerspectivePosition();
    }


    private void GetPerspectivePosition()
    {
        cube0.transform.position = CalcScreenToPlanePositon(mCamera, 0, 0, 0);
        cube1.transform.position = CalcScreenToPlanePositon(mCamera, 1, 0, 0);
        cube2.transform.position = CalcScreenToPlanePositon(mCamera, 1, 1, 0);
        cube3.transform.position = CalcScreenToPlanePositon(mCamera, 0, 1, 0);

    }


    private void GetPosition()
    {
        float minX = 0;
        float minY = 0;
        float maxX = 0;
        float maxY = 0;
        int x = 0;
        int y = 0;
        for (int i = 0; i < 3; i++)
        {
            x = i / 2;
            y = i % 2;
            Vector3 pos = CalcScreenToPlanePositon(mCamera, x, y, 0);

            if (minX == 0||pos.x<minX)
            {
                minX = pos.x;
            }
            if (minY == 0 || pos.z < minY)
            {
                minY = pos.z;
            }
            if (maxX == 0 || pos.x > maxX)
            {
                maxX = pos.x;
            }
            if (maxY == 0 || pos.z > maxY)
            {
                maxY = pos.z;
            }
        }

        cube0.transform.position = new Vector3(minX, 0, minY);
        cube1.transform.position = new Vector3(minX, 0, maxY);
        cube2.transform.position = new Vector3(maxX, 0, maxY);
        cube3.transform.position = new Vector3(maxX, 0, minY);
    }

    private Vector3 CalcPlanePosition(Camera camera,Vector3 screenPos,float planeHeight)
    {
        Vector3 worldPos = camera.ScreenToWorldPoint(screenPos);
        Vector3 direction = (worldPos - camera.transform.position).normalized;
        Vector3 planeNormal = new Vector3(0, 1, 0);
        Vector3 planePos = new Vector3(0, planeHeight, 0);
        if (camera.orthographic)
        {
            direction = camera.transform.forward;
        }
        float hitTime = Vector3.Dot(planeNormal,(planePos-worldPos))/Vector3.Dot(planeNormal,direction);
        Vector3 targetPoint = worldPos + hitTime * direction;
        return targetPoint;
    }

    private Vector3 CalcScreenToPlanePositon(Camera camera,float x,float y,float planeHeight)
    {
        Vector3 screenPos = new Vector3(camera.pixelWidth * x, camera.pixelHeight * y, camera.nearClipPlane);
        return CalcPlanePosition(camera, screenPos, planeHeight);
    }



    private void DrawRay()
    {

        var plane = new Plane(this.plane.transform.up, this.plane.transform.position);


        var leftButtom = new Vector3(0, 0, 12);
        var leftTop = new Vector3(0, Screen.height, 12);
        var rightTop = new Vector3(Screen.width, Screen.height, 12);
        var rightButtom = new Vector3(Screen.width, 0, 12);

        var ray_leftButtom = mCamera.ScreenPointToRay(leftButtom);
        var ray_leftTop = mCamera.ScreenPointToRay(leftTop);
        var ray_rightTop = mCamera.ScreenPointToRay(rightTop);
        var ray_rightButtom = mCamera.ScreenPointToRay(rightButtom);

        Debug.DrawRay(ray_leftButtom.origin, ray_leftButtom.direction, Color.white, 1f);
        Debug.DrawRay(ray_leftTop.origin, ray_leftTop.direction, Color.white, 1f);
        Debug.DrawRay(ray_rightTop.origin, ray_rightTop.direction, Color.white, 1f);
        Debug.DrawRay(ray_rightButtom.origin, ray_rightButtom.direction, Color.white, 1f);

        float enter = 0;

        if (plane.Raycast(ray_leftButtom, out enter))
        {
            cube0.transform.position = ray_leftButtom.GetPoint(enter);
        }

        if (plane.Raycast(ray_leftTop, out enter))
        {
            cube1.transform.position = ray_leftTop.GetPoint(enter);
        }

        if (plane.Raycast(ray_rightTop, out enter))
        {
            cube2.transform.position = ray_rightTop.GetPoint(enter);
        }

        if (plane.Raycast(ray_rightButtom, out enter))
        {
            cube3.transform.position = ray_rightButtom.GetPoint(enter);
        }

    }
}
