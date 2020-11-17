using UnityEngine;

namespace FutureGamesLib.Physics
{
    public class PhysicalSphere
    {
        float radius = 1f;
        public float Radius { get => radius; }

        public float Volume { get => (4f / 3f) * Mathf.PI * radius * radius * radius; }

        float density = 1f;
        public float Mass { get => Volume * density; }

        #region state
        Vector3 velocity = Vector3.zero;
        public Vector3 Velocity { get => velocity; }
        public float Speed { get => velocity.magnitude; }

        Vector3 position = Vector3.zero;
        public Vector3 Position { get => position; }
        #endregion state

        public float KineticEnergy { get => 0.5f * Mass * Speed * Speed; }

        public float GravityPotentialEnergy { get => UnityEngine.Physics.gravity.magnitude * Mass * position.y; }

        bool useGravity = false;
        Vector3 GravityForce { get => useGravity ? UnityEngine.Physics.gravity * Mass : Vector3.zero; }

        public PhysicalSphere(float radius, float density, bool useGravity, Vector3 position, Vector3 velocity)
        {
            this.radius = radius;
            this.density = density;

            this.useGravity = useGravity;

            this.position = position;
            this.velocity = velocity;
        }

        public void AddForce(Vector3 force)
        {
            Vector3 forceWithGravity = GravityForce + force;

            Vector3 acc = forceWithGravity / Mass;

            velocity = VelocityByEuler(acc);
            position = PositionByEuler();
        }

        Vector3 VelocityByEuler(Vector3 acc)
        {
            return velocity + acc * Time.deltaTime;
        }

        Vector3 PositionByEuler()
        {
            return position + velocity * Time.deltaTime;
        }
    
        public void SetPosition(Vector3 position)
        {
            this.position = position;
        }
    }
}