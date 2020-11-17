using UnityEngine;

namespace FutureGamesLib.Physics
{
    public class PhysicalPlane
    {
        Vector3 position = Vector3.zero;
        Vector3 normal = Vector3.up;
        public PhysicalPlane(Vector3 position, Vector3 normal)
        {
            this.position = position;
            this.normal = normal.normalized;
        }

        public void Bounce(PhysicalSphere sphere)
        {

        }

        public bool IsContact(PhysicalSphere sphere)
        {
            float dot = Vector3.Dot(sphere.Velocity, normal);

            if (dot > 0f)
                return false;

            return Distance(sphere) <= sphere.Radius;
        }

        public Vector3 ContactPosition(PhysicalSphere sphere)
        {
            return sphere.Position - Distance(sphere) * normal;
        }

        public Vector3 CorrectedPosition(PhysicalSphere sphere)
        {
            return ContactPosition(sphere) + normal * sphere.Radius;
        }

        float Distance(PhysicalSphere sphere)
        {
            return Vector3.Dot(sphere.Position - position, normal);
        }
    }
}