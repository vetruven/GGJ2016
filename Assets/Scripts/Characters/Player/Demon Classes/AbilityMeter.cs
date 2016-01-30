using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class AbilityMeter : MonoBehaviour
{
    private Scrollbar m_AbilityBar;

    private bool m_FatiguePenalty = false;
    private float m_PenaltyDuration = 0.4f;

    // Use this for initialization
    void Start()
    {
        m_AbilityBar = GetComponent<Scrollbar>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private bool alterEnergy(float i_EnergyDelta)
    {
        float newEnergy = m_AbilityBar.size + i_EnergyDelta;
        m_AbilityBar.size = Mathf.Clamp01(newEnergy);

        if(m_AbilityBar.size == 1)
        {
            setBarColor(Color.green);
        }
        else if(!m_FatiguePenalty)
        {
            setBarColor(Color.yellow);
        }

        return newEnergy == m_AbilityBar.size;
    }

    public void ReplenishEnergy(float i_EnergyAddition)
    {
        if (m_AbilityBar.size < 1)
        {
            alterEnergy(i_EnergyAddition);
        }
    }

    public void ConsumeEnergy(float i_EnergyConsumption)
    {
        alterEnergy(-i_EnergyConsumption);
    }

    public bool IsEnergySufficientFor(float i_EnergyConsumption)
    {
        bool energySufficient;

        if (m_FatiguePenalty)
        {
            energySufficient = false;
        }
        else
        {
            float newEnergy = m_AbilityBar.size - i_EnergyConsumption;
            energySufficient = newEnergy == Mathf.Clamp01(newEnergy);
            
            if (!energySufficient)
            {
                StartCoroutine(givePenalty());
            }
        }

        return energySufficient;
    }

    private IEnumerator givePenalty()
    {
        setBarColor(Color.red);
        m_FatiguePenalty = true;

        yield return new WaitForSeconds(m_PenaltyDuration);

        setBarColor(Color.yellow);
        m_FatiguePenalty = false;
    }

    private void setBarColor(Color i_Color)
    {
        ColorBlock colorBlock = m_AbilityBar.colors;
        colorBlock.normalColor = i_Color;

        m_AbilityBar.colors = colorBlock;
    }
}
