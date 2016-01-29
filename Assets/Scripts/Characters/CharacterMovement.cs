using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class CharacterMovement : MonoBehaviour
{
    //[SerializeField]
    //private float m_CollisionDetectionRange = 0.01f;

    private Rigidbody2D m_Rigidbody;

    [Serializable]
    private class Velocity
    {
        [SerializeField]
        private float m_PeakMovementSpeed = 2.5f;
        [SerializeField]
        private float m_VelocityIncrementationRate = 0.5f;
        [SerializeField]
        private float m_VelocityDecrementationRate = -0.5f;

        [SerializeField]
        private AnimationCurve m_VelocityGraph;

        private float m_UpVelocity = 0;
        private float m_DownVelocity = 0;
        private float m_LeftVelocity = 0;
        private float m_RightVelocity = 0;

        public float VerticalVelocity { get { return (m_UpVelocity > m_DownVelocity ? m_UpVelocity : -m_DownVelocity) * m_PeakMovementSpeed; } }
        public float HorizontalVelocity { get { return (m_RightVelocity > m_LeftVelocity ? m_RightVelocity : -m_LeftVelocity) * m_PeakMovementSpeed; } }

        public void ApplySpeedModifier(float i_SpeedModifier)
        {
            m_PeakMovementSpeed *= i_SpeedModifier;
        }

        public void VelocityUpdate(float i_HorizontalInput, float i_VerticalInput)
        {
            m_LeftVelocity = Mathf.Clamp01(m_LeftVelocity + (i_HorizontalInput < 0 ? m_VelocityIncrementationRate : m_VelocityDecrementationRate));
            m_RightVelocity = Mathf.Clamp01(m_RightVelocity + (i_HorizontalInput > 0 ? m_VelocityIncrementationRate : m_VelocityDecrementationRate));
            
            m_UpVelocity = Mathf.Clamp01(m_UpVelocity + (i_VerticalInput > 0 ? m_VelocityIncrementationRate : m_VelocityDecrementationRate));
            m_DownVelocity = Mathf.Clamp01(m_DownVelocity + (i_VerticalInput < 0 ? m_VelocityIncrementationRate : m_VelocityDecrementationRate));
        }
    }
    [SerializeField]
    private Velocity m_Velocity = new Velocity();
    
    protected virtual void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        updatePosition();
    }
    
    public void ApplySpeedModifier(float i_SpeedModifier)
    {
        m_Velocity.ApplySpeedModifier(i_SpeedModifier);
    }

    private void updatePosition()
    {
        Vector2 velocity = new Vector2(m_Velocity.HorizontalVelocity, m_Velocity.VerticalVelocity);

        //RaycastHit2D[] hits = Physics2D.BoxCastAll(m_Rigidbody.position + velocity.normalized, Vector2.one, 0, velocity.normalized, m_CollisionDetectionRange);
        //List<RaycastHit2D> hitList = new List<RaycastHit2D>(hits);

        //if(hitList.Exists((hit) => (hit && hit.rigidbody && hit.rigidbody != m_Rigidbody && hit.rigidbody.CompareTag("Player"))))
        //{
        //    m_Rigidbody.velocity = Vector2.zero;
        //}
        //else
        //{
        //    m_Rigidbody.velocity = velocity;
        //}

        m_Rigidbody.velocity = velocity;
    }

    protected void move(float i_HorizontalInput, float i_VerticalInput)
    {
        m_Velocity.VelocityUpdate(i_HorizontalInput, i_VerticalInput);
    }
}
