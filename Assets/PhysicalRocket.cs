using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalRocket : MonoBehaviour
{
    //collision member variables
    [SerializeField] private float m_GravitySpeed = Physics.gravity.y;
    [SerializeField] private GameObject m_OtherBox = null;
    [SerializeField] private Vector3 m_ColliderSize = Vector3.zero;
    [SerializeField] private Vector3 m_ColliderSizeOther = Vector3.zero;
    private Vector3 m_Velocity;

    //lift off member variables
    [SerializeField] private float m_VesselMass = 10f;
    [SerializeField] private float m_EnginePower = 10f;
    [SerializeField] private float m_FuelPerPower = 5f;
    public float m_MaxFuelMass = 10f;
    private bool m_Combustion = true;

    //fuel member variables
    public FuelIndicator FuelBar;
    private float m_TotalMass => m_VesselMass + m_CurrentFuelMass;
    private float m_CurrentFuelMass;

    void Start()
    {
        m_CurrentFuelMass = m_MaxFuelMass;
        FuelBar.SetMaxFuel(m_MaxFuelMass);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CustomGravity();
        PlayerInput();
        transform.position += m_Velocity * Time.fixedDeltaTime;
        CustomCollisionDetectionOnPlane();
        CustomCollisionDetectionAbovePlane();
    }

    private void CustomGravity()
    {
        float RelationMassGravity = m_GravitySpeed;
        m_Velocity.y += RelationMassGravity * Time.fixedDeltaTime;
    }

    private void CustomCollision(float collisionDistThisToOther, float dist)
    {
        m_Velocity.y = 0f;
        transform.position +=Vector3.up * (collisionDistThisToOther - dist);
    }

    private void CustomCollisionDetectionOnPlane()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        float distExtentsToCenter = m_ColliderSize.y * 0.5f;
        float distExtentsToCenterOther = m_ColliderSizeOther.y * 0.5f;
        float collisionDistThisToOther = distExtentsToCenter + distExtentsToCenterOther;
        float dist = Vector3.Dot(transform.position - m_OtherBox.transform.position, Vector3.up);

        if (rot.magnitude == 0)
        {
            if (dist <= collisionDistThisToOther)
            {
                CustomCollision(collisionDistThisToOther, dist);
            }
        }
    }

    private void CalculatePowerForLanding(out float distToPlane, out float necessaryLandingMeters)
    {
        float OtherBoxYPos = m_OtherBox.transform.position.y + (m_ColliderSizeOther.y * 0.5f);
        distToPlane = (this.transform.position.y - m_ColliderSize.y * 0.5f) - OtherBoxYPos;

        necessaryLandingMeters = (m_Velocity.y * m_Velocity.y) / (2 * (Mathf.Sqrt(m_EnginePower / m_TotalMass + m_GravitySpeed)));
    }

    private void CustomCollisionDetectionAbovePlane()
    {
        CalculatePowerForLanding(out float distToPlane, out float necessaryLandingMeters);
        if (distToPlane <= necessaryLandingMeters && m_Combustion == true && m_Velocity.y < 0)
        {
            RocketCombustion();
        }
    }

    private void FuelController()
    {
        m_CurrentFuelMass -= (Time.fixedDeltaTime * m_FuelPerPower * m_EnginePower);
        if(m_CurrentFuelMass < 0)
        {
            m_Combustion = false;
        }
    }

    private void PlayerInput()
    {
        if (m_Combustion)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                RocketCombustion();
            }
        }
    }

    private void RocketCombustion()
    {
        FuelController();
        FuelBar.SetFuel(m_CurrentFuelMass);
        if (m_CurrentFuelMass > 0)
        {
            AddForce(Vector3.up, m_EnginePower * Time.fixedDeltaTime);
        }
    }

    private void AddForce(Vector3 dir, float force)
    {
        m_Velocity += dir * (force / m_TotalMass);
    }

}
