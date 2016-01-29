using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public abstract class BaseDemon : MonoBehaviour
{
    protected bool m_SkillActive = false;

    [SerializeField]
    private float m_EnergyReplenishmentRate = 1f;
    [SerializeField]
    [Range(0, 1)]
    private float m_EnergyReplenishedPerTick = 0.07f;

    private PlayerStats m_PlayerStats;
    protected AbilityMeter m_AbilityMeter;
    private bool m_EnergyReplenishable;

    private String m_ActionButton { get { return String.Format("Action{0}", m_PlayerStats.PlayerId); } }
    private String m_CancelButton { get { return String.Format("Cancel{0}", m_PlayerStats.PlayerId); } }
    private String m_HorizontalAxis { get { return String.Format("SecondaryHorizontal{0}", m_PlayerStats.PlayerId); } }
    private String m_VerticalAxis { get { return String.Format("SecondaryVertical{0}", m_PlayerStats.PlayerId); } }

    protected virtual void Start()
    {
        m_PlayerStats = GetComponent<PlayerStats>();
        m_AbilityMeter = GetComponentInChildren<AbilityMeter>();

        StartCoroutine(replenishEnergy());
    }

    private IEnumerator replenishEnergy()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_EnergyReplenishmentRate);

            if (m_EnergyReplenishable)
            {
                m_AbilityMeter.ReplenishEnergy(m_EnergyReplenishedPerTick);
            }
        }
    }

    protected IEnumerator depleteEnergy(float i_EnergyDepletionPerTick)
    {
        DisableEnergyReplenishment();

        while (!m_EnergyReplenishable)
        {
            yield return new WaitForSeconds(m_EnergyReplenishmentRate);

            if (!m_AbilityMeter.ConsumeEnergy(i_EnergyDepletionPerTick))
            {
                break;
            }
        }
        
        cancelSkill();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        inputUpdate();
    }

    private void inputUpdate()
    {
        if (Input.GetButtonDown(m_ActionButton))
        {
            Debug.Log(m_ActionButton);
            activateSkill();
        }

        if (Input.GetButtonDown(m_CancelButton))
        {
            cancelSkill();
        }

        moveSkill(Input.GetAxis(m_HorizontalAxis), Input.GetAxis(m_VerticalAxis));
    }

    protected void DisableEnergyReplenishment()
    {
        m_EnergyReplenishable = false;
    }

    protected void EnableEnergyReplenishment()
    {
        m_EnergyReplenishable = true;
    }

    protected abstract void activateSkill();

    protected abstract void cancelSkill();

    protected abstract void moveSkill(float i_HorizontalInput, float i_VerticalInput);
}
