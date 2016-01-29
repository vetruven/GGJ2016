using UnityEngine;
using System.Collections;
using System;

[AddComponentMenu("Player/Player Movement")]
public class PlayerMovement : CharacterMovement
{
    [SerializeField]
    [Range(1, 4)]
    private int m_PlayerId = 1;

    private String m_HorizontalAxis { get { return String.Format("Horizontal{0}", m_PlayerId);  } }
    private String m_VerticalAxis { get { return String.Format("Vertical{0}", m_PlayerId); } }
    
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        InputUpdate();

        base.Update();
    }

    private void InputUpdate()
    {
        Move(Input.GetAxis(m_HorizontalAxis), Input.GetAxis(m_VerticalAxis));
    }
}
