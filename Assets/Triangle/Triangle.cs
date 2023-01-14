using UnityEngine;

public class Triangle : MonoBehaviour
{
    Vector3 A = new Vector3(2f, 2f, 0);
    Vector3 B = new Vector3(2f, -2f, 0);
    Vector3 C = new Vector3(-2f, -2f, 0);

    Vector3 P = new Vector3(1f, -0.5f, 0);
    Vector3 Q = new Vector3(-3f, 3f, 0);


    // Start is called before the first frame update
    void Start()
    {
        CreateTriangle();

        Debug.Log("点P是否在三角形ABC内（第一种方法）：" + IsPointInTriangle1(A, B, C, P));
        Debug.Log("点P是否在三角形ABC内（第二种方法）：" + IsPointInTriangle2(A, B, C, P));
        Debug.Log("点P是否在三角形ABC内（第三种方法）：" + IsPointInTriangle3(A, B, C, P));
        Debug.Log("点P是否在三角形ABC内（第四种方法）：" + IsPointInTriangle4(A, B, C, P));

        Debug.Log("点Q是否在三角形ABC内（第一种方法）：" + IsPointInTriangle1(A, B, C, Q));
        Debug.Log("点Q是否在三角形ABC内（第二种方法）：" + IsPointInTriangle2(A, B, C, Q));
        Debug.Log("点Q是否在三角形ABC内（第三种方法）：" + IsPointInTriangle3(A, B, C, Q));
        Debug.Log("点Q是否在三角形ABC内（第四种方法）：" + IsPointInTriangle4(A, B, C, Q));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateTriangle()
    {
        GameObject go = new GameObject("Triangle");
        MeshFilter meshFilter = go.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = go.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();

        Vector3[] vertices = { new Vector3(2f, 2f, 0), new Vector3(2f, -2f, 0), new Vector3(-2f, -2f, 0) };
        int[] triangles = { 0, 1, 2 };

        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);

        meshFilter.mesh = mesh;

        Material material = new Material(Shader.Find("Standard"));

        meshRenderer.material = material;


        GameObject p = GameObject.CreatePrimitive(PrimitiveType.Cube);
        p.name = "P";
        p.transform.position = P;

        GameObject q = GameObject.CreatePrimitive(PrimitiveType.Cube);
        q.name = "Q";
        q.transform.position = Q;

    }

    float Cross(Vector2 a, Vector2 b)
    {
        return a.x * b.y - a.y * b.x;
    }


    //判断点M,N是否在直线AB的同一侧
    bool IsPointAtSameSideOfLine(Vector2 pointM, Vector2 pointN, Vector2 pointA, Vector2 pointB)
    {
        Vector2 AB = pointB - pointA;
        Vector2 AM = pointM - pointA;
        Vector2 AN = pointN - pointA;

        //等于0时表示某个点在直线上
        return Cross(AB, AM) * Cross(AB, AN) >= 0;
    }


    //计算三角形面积
    float ComputeTriangleArea(Vector2 pointA, Vector2 pointB, Vector2 pointC)
    {
        //依据两个向量的叉乘来计算
        Vector2 AB = pointB - pointA;
        Vector2 AC = pointC - pointA;
        return Mathf.Abs(Cross(AB, AC) / 2f);
    }

    //通过判断面积是否相等
    bool IsPointInTriangle1(Vector2 pointA, Vector2 pointB, Vector2 pointC, Vector2 pointP)
    {
        float area_ABC = ComputeTriangleArea(pointA, pointB, pointC);
        float area_PAB = ComputeTriangleArea(pointP, pointA, pointB);
        float area_PAC = ComputeTriangleArea(pointP, pointA, pointC);
        float area_PBC = ComputeTriangleArea(pointP, pointB, pointC);

        if (Mathf.Abs(area_PAB + area_PAC + area_PBC - area_ABC) < .00001f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    //通过判断点在直线同侧
    bool IsPointInTriangle2(Vector2 pointA, Vector2 pointB, Vector2 pointC, Vector2 pointP)
    {
        return IsPointAtSameSideOfLine(pointP, pointA, pointB, pointC) &&
            IsPointAtSameSideOfLine(pointP, pointB, pointA, pointC) &&
            IsPointAtSameSideOfLine(pointP, pointC, pointA, pointB);
    }

    //根据向量基本定理和点在三角形内部充要条件判断
    bool IsPointInTriangle3(Vector2 pointA, Vector2 pointB, Vector2 pointC, Vector2 pointP)
    {
        Vector2 AB = pointB - pointA;
        Vector2 AC = pointC - pointA;
        Vector2 AP = pointP - pointA;
        float dot_ac_ac = Vector2.Dot(AC, AC);
        float dot_ac_ab = Vector2.Dot(AC, AB);
        float dot_ac_ap = Vector2.Dot(AC, AP);
        float dot_ab_ab = Vector2.Dot(AB, AB);
        float dot_ab_ap = Vector2.Dot(AB, AP);

        float tmp = 1f / (dot_ac_ac * dot_ab_ab - dot_ac_ab * dot_ac_ab);

        float u = (dot_ab_ab * dot_ac_ap - dot_ac_ab * dot_ab_ap) * tmp;
        if (u < 0 || u > 1)
            return false;
        float v = (dot_ac_ac * dot_ab_ap - dot_ac_ab * dot_ac_ap) * tmp;
        if (v < 0 || v > 1)
            return false;

        return u + v <= 1;
    }

    // t1 = PA^PB, t2 = PB^PC,  t3 = PC^PA, t1,t2,t3 同号则 P在三角形内部
    bool IsPointInTriangle4(Vector2 pointA, Vector2 pointB, Vector2 pointC, Vector2 pointP)
    {
        Vector2 PA = pointA - pointP;
        Vector2 PB = pointB - pointP;
        Vector2 PC = pointC - pointP;

        float t1 = Cross(PA, PB);
        float t2 = Cross(PB, PC);
        float t3 = Cross(PC, PA);

        return t1 * t2 >= 0 && t2 * t3 >= 0 && t3 * t1 >= 0;
    }
}
