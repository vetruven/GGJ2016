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
}
