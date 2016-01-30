using UnityEngine;
using System.Collections.Generic;
using System;

[AddComponentMenu("Player/Demon Classes/Conjurer")]
public class ConjurerDemon : BaseDemon
{
    [SerializeField]
    private const int k_MaxProxiesPlacable = 2;

    [SerializeField]
    [Range(0, 1)]
    private float m_AbilityCostPerSecond;

    [SerializeField]
    private DemonProxy m_DemonProxy;

    private Queue<DemonProxy> m_PlacedProxies = new Queue<DemonProxy>(k_MaxProxiesPlacable);

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public void KillProxy()
    {
        if (m_PlacedProxies.Count > 0)
        {
            do
            {
                GameObject.Destroy(m_PlacedProxies.Dequeue().GetComponent<DemonProxy>().gameObject);

                EventBus.BeaconDeactivated.Dispatch();
            } while (!m_AbilityMeter.IsEnergySufficientFor(m_AbilityCostPerSecond) && m_PlacedProxies.Count > 0);

            if (m_PlacedProxies.Count == 0)
            {
                EnableEnergyReplenishment();
                m_SkillActive = false;
            }
        }
    }

    protected override void activateSkill()
    {
        if (m_PlacedProxies.Count < k_MaxProxiesPlacable)
        {
            if (m_AbilityMeter.IsEnergySufficientFor(0))
            {
                EventBus.BeaconActivated.Dispatch();

                GameObject instantiatedObject = GameObject.Instantiate(m_DemonProxy.gameObject, transform.position, Quaternion.identity) as GameObject;
                DemonProxy proxy = instantiatedObject.GetComponent<DemonProxy>();
                m_PlacedProxies.Enqueue(proxy);

                StartCoroutine(depleteEnergy(m_AbilityCostPerSecond));

                DisableEnergyReplenishment();

                m_SkillActive = true;
            }
        }
    }

    protected override void cancelSkill()
    {
        KillProxy();
    }

    protected override void moveSkill(float i_HorizontalInput, float i_VerticalInput)
    {
    }

    protected override void skillHeld()
    {
    }

    protected override void skillReleased()
    {
    }
}
