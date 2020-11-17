using System;
using UnityEngine;

namespace FutureGames.GamePhysics
{
    public class MonoPlane : MonoBehaviour
    {
        Vector3 Normal => transform.up;
        Vector3 Position => transform.position;

        Renderer Renderer => GetComponent<Renderer>();

        private void Update()
        {
            Vector3 lowestPoint = Renderer.bounds.center - Renderer.bounds.extents;
            Vector3 higestPoint = Renderer.bounds.center + Renderer.bounds.extents;

            Debug.DrawLine(lowestPoint, higestPoint, Color.cyan);
        }

        public float Distance(MonoPhysicalSphere sphere)
        {
            Vector3 sphereToPlane = Position - sphere.transform.position;

            //Vector3 sphereToPlane = sphere.transform.position - Position;

            return Vector3.Dot(sphereToPlane, Normal);
        }

        public Vector3 Projection(MonoPhysicalSphere sphere)
        {
            Vector3 sphereToProjection = Distance(sphere) * Normal;

            return sphere.transform.position + sphereToProjection;
        }

        public bool IsColliding(MonoPhysicalSphere sphere)
        {
            if (WillBeCollision(sphere) == false)
                return false;

            return Distance(sphere) >= 0f || Mathf.Abs(Distance(sphere)) <= sphere.Radius;
        }

        public bool WillBeCollision(MonoPhysicalSphere sphere)
        {
            return Vector3.Dot(sphere.Velocity, Normal) < 0f;
        }

        public Vector3 CorrectedPosition(MonoPhysicalSphere sphere)
        {
            return Projection(sphere) + Normal * sphere.Radius;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sphere"></param>
        /// <param name="energyDissipation" the impact of the energy dissipation on the reflected velocity></param>
        public void Choc(MonoPhysicalSphere sphere, float energyDissipation = 0f)
        {
            if (IsColliding(sphere) == false)
                return;

            //Debug.Log("Velocity Error: isVerlet: " + sphere.isVerlet + " " + sphere.ErrorVelocityOnTheGround());

            //sphere.Velocity = Vector3.Reflect(sphere.Velocity, Normal);

            if (IsSphereStatic(sphere))
            {
                sphere.transform.position = CorrectedPosition(sphere);
                sphere.ApplyForce(-sphere.mass * Physics.gravity);
            }
            else // sphere is dynamic
            {
                sphere.Velocity = Reflect(sphere.Velocity, energyDissipation);
            }
        }

        private Vector3 Reflect(Vector3 v, float energyDissipation = 0f)
        {
            Vector3 r = (v - 2f * Vector3.Dot(v, Normal) * Normal) * (1f - energyDissipation);

            //Debug.Log(r.y);
            return r;
        }

        bool IsSphereStatic(MonoPhysicalSphere sphere)
        {
            bool lowVelocity = sphere.Velocity.magnitude < 0.02f;
            bool touchingThePlane = (CorrectedPosition(sphere) - sphere.transform.position).magnitude < 0.2f;

            return lowVelocity && touchingThePlane;
        }
    }
}