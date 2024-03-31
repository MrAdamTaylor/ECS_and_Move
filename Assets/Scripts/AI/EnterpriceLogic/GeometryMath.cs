using UnityEngine;

public static class GeometryMath
{
    public static float GetHypotenuseX(Transform position1, Transform position2)
    {
        return position1.transform.position.x - position2.transform.position.x;
    }
    
    public static float GetHypotenuseY(Transform position1, Transform position2)
    {
        return position1.transform.position.y - position2.transform.position.y;
    }
    
    public static float GetHypotenuseZ(Transform position1, Transform position2)
    {
        return position1.transform.position.z - position2.transform.position.z;
    }

    public static float DotProductXZ(Vector3 dir1, Vector3 dir2)
    {
        float dot = dir1.x * dir2.x + dir1.z * dir2.z;
        return dot;
    }

    public static float DotProductXY(Vector3 dir1, Vector3 dir2)
    {
        float dot = dir1.x * dir2.x + dir1.y * dir2.y;
        Debug.Log("Dot calculate Operation: "+dot);
        return dot;
    }

    public static Vector3 CrossProduct(Vector3 dir1, Vector3 dir2)
    {
        float xMult = dir1.y * dir2.z - dir1.z * dir2.y;
        float yMult = dir1.x * dir2.z - dir1.z * dir2.x;
        float zMult = dir1.x * dir2.y - dir1.y * dir2.x;

        return new Vector3(xMult, yMult, zMult);
    }
}