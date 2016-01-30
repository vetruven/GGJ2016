using UnityEngine;
using System.Collections;
using System;

[AddComponentMenu("Player/Demon Classes/Illusionist")]
public class IllusionistDemon : BaseDemon
{
    [SerializeField]
    [Range(0, 1)]
    private float m_AbilityCostPerSecond;

    [SerializeField]
    private GameObject m_IllusionObject;

    [SerializeField]
    private float m_DegreeBetweenIllusions;
    [SerializeField]
    private float m_IllusionDistanceFromDemon;

    private Transform m_IllusionContainer;
    private GameObject[] m_Illusions = new GameObject[2];

    private Vector3 m_LastRecordedDirection;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        startIllusions();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    private void startIllusions()
    {
        m_Illusions[0] = GameObject.Instantiate(m_IllusionObject, transform.position, Quaternion.identity) as GameObject;
        m_Illusions[1] = GameObject.Instantiate(m_IllusionObject, transform.position, Quaternion.identity) as GameObject;

        GameObject illusionContainer = new GameObject();
        illusionContainer.name = "Illusions";
        illusionContainer.transform.position = transform.position;
        m_Illusions[0].transform.SetParent(illusionContainer.transform);
        m_Illusions[1].transform.SetParent(illusionContainer.transform);
        illusionContainer.transform.SetParent(transform);
        m_IllusionContainer = illusionContainer.transform;

        setIllusionsToVector(-transform.up);

        m_IllusionContainer.gameObject.SetActive(false);
    }

    private void setIllusionsToVector(Vector3 i_PerpendicularVector)
    {
        m_LastRecordedDirection = i_PerpendicularVector;

        m_Illusions[0].transform.position = transform.position + i_PerpendicularVector.Rotate(m_DegreeBetweenIllusions / 2).normalized * m_IllusionDistanceFromDemon;

        m_Illusions[1].transform.position = transform.position + i_PerpendicularVector.Rotate(-m_DegreeBetweenIllusions / 2).normalized * m_IllusionDistanceFromDemon;
    }

    protected override void activateSkill()
    {
        if (!m_SkillActive)
        {
            m_SkillActive = true;

            EventBus.IllusionActivated.Dispatch();

            StartCoroutine(depleteEnergy(m_AbilityCostPerSecond));

            m_IllusionContainer.gameObject.SetActive(true);
        }
    }

    protected override void cancelSkill()
    {
        if (m_SkillActive)
        {
            m_SkillActive = false;

            EventBus.IllusionDeactivated.Dispatch();

            EnableEnergyReplenishment();

            m_IllusionContainer.gameObject.SetActive(false);
        }
    }

    protected override void moveSkill(float i_HorizontalInput, float i_VerticalInput)
    {
        if (m_SkillActive)
        {
            if (i_HorizontalInput != 0 || i_VerticalInput != 0)
            {
                setIllusionsToVector(new Vector3(i_HorizontalInput, i_VerticalInput).normalized);

                m_Illusions[0].transform.rotation = Quaternion.identity;
                m_Illusions[1].transform.rotation = Quaternion.identity;
            }
            else
            {
                setIllusionsToVector(m_LastRecordedDirection);
            }
        }
    }
}