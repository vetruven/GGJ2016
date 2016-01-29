using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

    public bool ReplenishEnergy(float i_EnergyAddition)
    {
        return alterEnergy(i_EnergyAddition);
    }

    public bool ConsumeEnergy(float i_EnergyConsumption)
    {
        return alterEnergy(-i_EnergyConsumption);
    }

    public bool IsEnergySufficientFor(float i_EnergyConsumption)
    {
        float newEnergy = m_AbilityBar.size - i_EnergyConsumption;
        return newEnergy == Mathf.Clamp01(newEnergy);
    }
}
