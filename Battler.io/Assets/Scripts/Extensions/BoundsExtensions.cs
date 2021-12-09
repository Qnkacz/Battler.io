using UnityEngine;

namespace Extensions
{
    public static class BoundsExtensions
    {
        public static Vector3 GetRandomPositionInsideBound(this Bounds inputBound)
        {
            var extents = inputBound.extents / 2f;
            return new Vector3(
                Random.Range( -extents.x, extents.x ),
                Random.Range( -extents.y, extents.y ),
                Random.Range( -extents.z, extents.z )
            )+inputBound.center;
        }
    }
}   