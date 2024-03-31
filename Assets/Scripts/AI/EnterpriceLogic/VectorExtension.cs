using UnityEngine;

public static class VectorExtension
{
    public static Vector3 ExcludeY(this Vector3 vec)
    {
        Vector3 newVec = new Vector3(vec.x, 0, vec.z);
        return newVec;
    }
    
    public static Vector3 ExcludeZ(this Vector3 vec)
    {
        Vector3 newVec = new Vector3(vec.x, vec.y,0);
        return newVec;
    }
    
    public static Vector3 ExcludeX(this Vector3 vec)
    {
        Vector3 newVec = new Vector3(0, vec.y,vec.z);
        return newVec;
    }
}