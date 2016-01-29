using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public abstract class BaseDemon : MonoBehaviour
{
    private PlayerStats m_PlayerStats;
    private AbilityMeter m_AbilityMeter;

    private String m_ActionButton { get { return String.Format("Action{0}", m_PlayerStats.PlayerId); } }
    private String m_CancelButton { get { return String.Format("Cancel{0}", m_PlayerStats.PlayerId); } }
    private String m_HorizontalAxis { get { return String.Format("SecondaryHorizontal{0}", m_PlayerStats.PlayerId); } }
    private String m_VerticalAxis { get { return String.Format("SecondaryVertical{0}", m_PlayerStats.PlayerId); } }

    protected virtual void Start()
    {
        m_PlayerStats = GetComponent<PlayerStats>();
        m_AbilityMeter = GetComponentInChildren<AbilityMeter>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        inputUpdate();
    }

    private void inputUpdate()
    {
        if (Input.GetButton(m_ActionButton))
        {
            activateSkill();
        }

        if (Input.GetButton(m_CancelButton))
        {
            cancelSkill();
        }

        moveSkill(Input.GetAxis(m_HorizontalAxis), Input.GetAxis(m_VerticalAxis));
    }

    protected abstract void activateSkill();

    protected abstract void cancelSkill();

    protected abstract void moveSkill(float i_HorizontalInput, float i_VerticalInput);
}
