using UnityEngine;
using System.Collections;
using System;

public abstract class CharacterMovement : MonoBehaviour
{
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
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + new Vector2(m_Velocity.HorizontalVelocity, m_Velocity.VerticalVelocity) * Time.deltaTime);
    }

    protected void Move(float i_HorizontalInput, float i_VerticalInput)
    {
        m_Velocity.VelocityUpdate(i_HorizontalInput, i_VerticalInput);
    }
}
