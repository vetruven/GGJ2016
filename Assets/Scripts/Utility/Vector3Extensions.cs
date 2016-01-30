using UnityEngine;
using System.Collections;

public static class Vector3Extensions
{
    public static Vector3 Rotate(this Vector3 i_Vector, float i_Degrees)
    {
        return i_Vector.RotateRadians(i_Degrees * Mathf.Deg2Rad);
    }

    public static Vector3 RotateRadians(this Vector3 i_Vector, float i_Radians)
    {
        float cos = Mathf.Cos(i_Radians);
        float sin = Mathf.Sin(i_Radians);

        return new Vector3(cos * i_Vector.x - sin * i_Vector.y, sin * i_Vector.x + cos * i_Vector.y);
    }
}
