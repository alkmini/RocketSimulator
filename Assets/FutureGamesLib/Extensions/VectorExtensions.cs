using UnityEngine;

namespace FutureGamesLib
{
    public static class VectorExtensions
    {
        public static Vector3 With(this Vector3 t,
            float? x = null,
            float? y = null,
            float? z = null)
        {
            return new Vector3(
                x ?? t.x,
                y ?? t.y,
                z ?? t.z);
        }
        public static Vector2 With(this Vector2 t,
            float? x = null,
            float? y = null
            )
        {
            return new Vector2(
                x ?? t.x,
                y ?? t.y);
        }
    }
}