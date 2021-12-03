using UnityEngine;

namespace Extensions
{
    public static class BoundsExtensions
    {
        public static Vector3 GetRandomPositionInsideBound(this Bounds inputBound)
        {
            return new Vector3(
                Random.Range(inputBound.min.x,inputBound.max.x),
                Random.Range(inputBound.min.y,inputBound.max.y),
                Random.Range(inputBound.min.z,inputBound.max.z)
            );
        }
    }
}   