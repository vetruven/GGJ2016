using UnityEngine;
using System.Collections;
using System;

[AddComponentMenu("Objects/Demon Proxy")]
public class DemonProxy : MonoBehaviour
{
    //private float m_EnergyConsumptionPerSecond;
    //private float m_EnergyTickRate;

    //private AbilityMeter m_MeterConsumedFrom;
    //private ConjurerDemon m_OwningDemon;

    //// Use this for initialization
    //void Start()
    //{
    //}

    //private IEnumerator energyConsumptionOverTime()
    //{
    //    while (true)
    //    {
    //        if (m_MeterConsumedFrom.IsEnergySufficientFor(m_EnergyConsumptionPerSecond))
    //        {
    //            m_MeterConsumedFrom.ConsumeEnergy(m_EnergyConsumptionPerSecond);
    //        }
    //        else
    //        {
    //            m_OwningDemon.KillProxy();
    //        }

    //        yield return new WaitForSeconds(1);
    //    }
    //}

    //public void Init(BaseDemon i_OwningDemon, float i_EnergyTickRate, float i_EnergyConsumptionPerSecond)
    //{
    //    m_MeterConsumedFrom = i_OwningDemon.GetComponentInChildren<AbilityMeter>();

    //    m_EnergyConsumptionPerSecond = i_EnergyConsumptionPerSecond;
    //    m_EnergyTickRate = i_EnergyTickRate;
    //    m_OwningDemon = i_OwningDemon as ConjurerDemon;

    //    StartCoroutine(energyConsumptionOverTime());
    //}
}
