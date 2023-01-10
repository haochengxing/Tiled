using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{

    public Camera mCamera;
    public GameObject cube0;
    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;

    // Start is called before the first frame update
    void Start()
    {
        GetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        GetPosition();
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
}
