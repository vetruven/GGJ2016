using UnityEngine;
using System.Collections;
using System;

[AddComponentMenu("Player/Demon Classes/Sprinter")]
public class SprinterDemon : BaseDemon {

    [SerializeField]
    private float m_SpeedModifier = 2f;

    [SerializeField]
    [Range(0, 1)]
    private float m_AbilityCostPerSecond;

    // Use this for initialization
    protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
	}

    protected override void activateSkill()
    {
        if (!m_SkillActive)
        {
            m_SkillActive = true;

            EventBus.SprintActivated.Dispatch();

            StartCoroutine(depleteEnergy(m_AbilityCostPerSecond));

            GetComponent<CharacterMovement>().ApplySpeedModifier(m_SpeedModifier);
        }
    }

    protected override void cancelSkill()
    {
        if (m_SkillActive)
        {
            m_SkillActive = false;

            EnableEnergyReplenishment();

            GetComponent<CharacterMovement>().ApplySpeedModifier(1 / m_SpeedModifier);
        }
    }

    protected override void moveSkill(float i_HorizontalInput, float i_VerticalInput)
    {
    }
}
