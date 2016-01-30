using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class AbilityMeter : MonoBehaviour
{
    private Scrollbar m_AbilityBar;

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

        return newEnergy == m_AbilityBar.size;
    }

    public void ReplenishEnergy(float i_EnergyAddition)
    {
        if (m_AbilityBar.size < 1)
        {
            setBarColor(Color.blue);

            if (!alterEnergy(i_EnergyAddition))
            {
                setBarColor(Color.green);
            }
        }
    }

    public void ConsumeEnergy(float i_EnergyConsumption)
    {
        alterEnergy(-i_EnergyConsumption);

        setBarColor(Color.yellow);
    }

    public bool IsEnergySufficientFor(float i_EnergyConsumption)
    {
        float newEnergy = m_AbilityBar.size - i_EnergyConsumption;
        bool energySufficient = newEnergy == Mathf.Clamp01(newEnergy);

        if (!energySufficient)
        {
            setBarColor(Color.red);
        }

        return energySufficient;
    }

    private void setBarColor(Color i_Color)
    {
        ColorBlock colorBlock = m_AbilityBar.colors;
        colorBlock.normalColor = i_Color;

        m_AbilityBar.colors = colorBlock;
    }
}
