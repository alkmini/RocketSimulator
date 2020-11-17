using FutureGamesLib;
using FutureGamesLib.Physics;
using UnityEngine;
using UnityEngine.Video;

namespace FutureGames.GamePhysics
{
    public class TestPhysicalSphere : MonoBehaviour
    {
        [SerializeField]
        float density = 1f;

        PhysicalSphere sphere = null;

        [SerializeField]
        TestPhysicalPlane plane = null;

        private void Start()
        {
            sphere = new PhysicalSphere(transform.localScale.x*0.5f, density, true, transform.position, Vector3.zero);
        }

        private void Update()
        {
            //sphere.AddForce(Vector3.zero.With(z: 10f));
            //transform.position = sphere.Position;

            if(Input.GetKeyDown(KeyCode.K))
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = plane.Plane.ContactPosition(sphere);
                Debug.Log(plane.Plane.IsContact(sphere));

                sphere.SetPosition(plane.Plane.CorrectedPosition(sphere));
                UpdateTransformPosition();
            }    
        }

        void UpdateTransformPosition()
        {
            transform.position = sphere.Position;
        }
    }
}