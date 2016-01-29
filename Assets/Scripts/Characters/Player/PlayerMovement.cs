using UnityEngine;
using System.Collections;
using System;

[AddComponentMenu("Player/Player Movement")]
public class PlayerMovement : CharacterMovement
{
    private PlayerStats m_PlayerStats;

    private String m_HorizontalAxis { get { return String.Format("Horizontal{0}", m_PlayerStats.PlayerId);  } }
    private String m_VerticalAxis { get { return String.Format("Vertical{0}", m_PlayerStats.PlayerId); } }
    
    protected override void Start()
    {
        m_PlayerStats = GetComponent<PlayerStats>();
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
    }
}
