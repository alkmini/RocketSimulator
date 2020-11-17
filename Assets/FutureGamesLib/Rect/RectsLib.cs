using UnityEngine;

namespace FutureGamesLib
{
    public static class RectsLib
    {
        public static Rect RectByCenterAndSize(Vector2 center, Vector2 size)
        {
            float x = center.x - 0.5f * size.x;
            float y = center.y - 0.5f * size.y;

            return new Rect(x, y, size.x, size.y);
        }
    }
}