using UnityEngine;
using System.Collections;
using System;

[AddComponentMenu("Player/Demon Classes/Grappler")]
public class GrapplerDemon : BaseDemon
{
    [SerializeField]
    [Range(0, 1)]
    private float m_AbilityCostPerSecond;

    [SerializeField]
    private GameObject m_HerdingPenObject;

    private GameObject m_HerdingPen;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        initGrappler();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    private void initGrappler()
    {
        m_HerdingPen = GameObject.Instantiate(m_HerdingPenObject, transform.position, Quaternion.identity) as GameObject;
        m_HerdingPen.gameObject.SetActive(false);

        m_HerdingPen.transform.SetParent(transform);
    }

    protected override void activateSkill()
    {
        if (!m_SkillActive)
        {
            if (m_AbilityMeter.IsEnergySufficientFor(m_AbilityCostPerSecond))
            {
                m_SkillActive = true;

                EventBus.GrapplerActivated.Dispatch();

                StartCoroutine(depleteEnergy(m_AbilityCostPerSecond));

                m_HerdingPen.gameObject.SetActive(true);
            }
        }
    }

    protected override void cancelSkill()
    {
        if (m_SkillActive)
        {
            m_SkillActive = false;

            EventBus.GrapplerDeactivated.Dispatch();

            EnableEnergyReplenishment();

            m_HerdingPen.gameObject.SetActive(false);
        }
    }

    protected override void moveSkill(float i_HorizontalInput, float i_VerticalInput)
    {
    }
}
