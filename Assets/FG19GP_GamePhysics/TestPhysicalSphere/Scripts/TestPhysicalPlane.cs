using FutureGamesLib.Physics;
using UnityEngine;

namespace FutureGames.GamePhysics
{
    public class TestPhysicalPlane : MonoBehaviour
    {
        PhysicalPlane plane = null;
        public PhysicalPlane Plane { get => plane; }

        void Start()
        {
            plane = new PhysicalPlane(transform.position, transform.TransformDirection(Vector3.up));
        }
    }
}