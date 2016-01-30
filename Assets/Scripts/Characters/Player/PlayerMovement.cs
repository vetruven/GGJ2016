using UnityEngine;
using System.Collections;
using System;

[AddComponentMenu("Player/Player Movement")]
public class PlayerMovement : CharacterMovement
{
    private PlayerStats m_PlayerStats;

    private Animator m_Animator;

    private float m_LastSpeedValue;

    private String m_HorizontalAxis { get { return String.Format("Horizontal{0}", m_PlayerStats.PlayerId);  } }
    private String m_VerticalAxis { get { return String.Format("Vertical{0}", m_PlayerStats.PlayerId); } }
    
    protected override void Start()
    {
        m_PlayerStats = GetComponent<PlayerStats>();
        m_Animator = GetComponent<Animator>();
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        inputUpdate();

        base.Update();
    }

    private void inputUpdate()
    {
        move(Input.GetAxis(m_HorizontalAxis), Input.GetAxis(m_VerticalAxis));

        float horizontalSpeed = Input.GetAxis(m_HorizontalAxis);

        if (Mathf.Abs(horizontalSpeed) > 0)
        {
            m_LastSpeedValue = horizontalSpeed;
        }
        else
        {
            if (Input.GetAxis(m_VerticalAxis) == 0)
            {
                m_LastSpeedValue = 0;
            }
        }
        
        m_Animator.SetFloat("Speed", m_LastSpeedValue);


		Vector3 newScale = new Vector3(m_Animator.gameObject.transform.localScale.x, m_Animator.gameObject.transform.localScale.y, m_Animator.gameObject.transform.localScale.z);
		if(horizontalSpeed > 0)
		{
			newScale.x = -1;
		}
		else
		{
			newScale.x = 1;
		}
		m_Animator.gameObject.transform.localScale = newScale;
    }
}
