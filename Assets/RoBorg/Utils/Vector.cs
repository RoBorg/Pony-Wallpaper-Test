using UnityEngine;

namespace RoBorg.Utils
{
    public static class Vector
    {
        public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
        {
            var dir = point - pivot; // Get point direction relative to pivot
            dir = Quaternion.Euler(angles) * dir; // Rotate it

            return dir + pivot;
        }
    }
}
