using System;
using UnityEngine;

namespace FutureGames.GamePhysics
{
    public class MonoPhysicalSphere : MonoBehaviour
    {
        [SerializeField]
        Vector3 velocity = Vector3.zero;
        public Vector3 Velocity { get => velocity; set => velocity = value; }


        public float mass = 1f;


        [SerializeField]
        bool useGravity = false;


        public bool isVerlet = false;

        [SerializeField]
        MonoPlane plane = null;

        [SerializeField]
        float chocEnergyDissipation = 0.05f;

        public bool onPlane = false;

        public float Radius => transform.localScale.x * 0.5f;

        private void Update()
        {
            ApplyForce(new Vector3(0f, 0f, 0f));

            Vector3 hitPoint = plane.Projection(this);
            bool isColliding = plane.IsColliding(this);

            Debug.DrawLine(transform.position, hitPoint, isColliding ? Color.red : Color.blue);

            //CorrectPosition(isColliding, hitPoint);

            if(onPlane)
                plane.Choc(this, chocEnergyDissipation);
        }

        void CorrectPosition(bool isColliding, Vector3 hitPoint)
        {
            if (isColliding == false)
                return;

            Vector3 correctedPosition = plane.CorrectedPosition(this);
            Debug.DrawLine(hitPoint, correctedPosition, Color.green);

            transform.position = correctedPosition;
        }

        /// <summary>
        /// Euler integration
        /// </summary>
        /// <param name="force"></param>
        public void ApplyForce(Vector3 force)
        {
            Vector3 totalForce = useGravity ? force + mass * Physics.gravity : force;

            // f = m * a
            // a = f / m
            Vector3 acc = totalForce / mass;

            Integrate(acc, isVerlet);
        }

        void Integrate(Vector3 acc, bool isVerlet = false)
        {
            if (isVerlet == false) // use Euler
            {
                // v1 = v0 + a*detaTime
                velocity = velocity + acc * Time.deltaTime;

                // p1 = p0 + v*deltatime
                transform.position = transform.position + velocity * Time.deltaTime;
            }
            else // use Verlet
            {
                transform.position +=
                    velocity * Time.deltaTime +
                    acc * Time.deltaTime * Time.deltaTime * 0.5f;

                velocity += acc * Time.deltaTime * 0.5f; // ??
            }
        }

        /// <summary>
        /// Assuming the initial velocity is 0
        /// </summary>
        /// <returns></returns>
        public float VelocityOnGround()
        {
            return Mathf.Sqrt(2f * Physics.gravity.magnitude * (transform.position.y - plane.transform.position.y));
        }

        public float ErrorVelocityOnTheGround()
        {
            return Mathf.Abs(velocity.magnitude - VelocityOnGround());
        }

        private void OnTriggerEnter(Collider other)
        {
            UpdateOnPlaneWhenEnter(other);
        }

        private void UpdateOnPlaneWhenEnter(Collider other)
        {
            MonoPlane plane = other.GetComponent<MonoPlane>();
            if (plane == null)
                onPlane = false;
            else
                onPlane = true;
        }

        private void OnTriggerExit(Collider other)
        {
            UpdateOnPlaneWhenExit(other);
        }

        private void UpdateOnPlaneWhenExit(Collider other)
        {
            MonoPlane plane = other.GetComponent<MonoPlane>();
            if (plane != null)
                onPlane = false;
        }
    }
}