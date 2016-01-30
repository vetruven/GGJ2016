using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerButtonController : MonoBehaviour
{
    [SerializeField]
    [Range(1, 4)]
    private int m_PlayerId = 1;

    private string m_HorizontalAxis;
    private string m_VerticalAxis;
    private string m_SecondaryVerticalAxis;
    private string m_SecondaryHorizontalAxis;
    
    private bool _isSelected;

    void Awake()
    {
        m_HorizontalAxis = string.Format("Horizontal{0}", m_PlayerId); 
        m_VerticalAxis = string.Format("Vertical{0}", m_PlayerId);

        m_SecondaryHorizontalAxis = string.Format("SecondaryHorizontal{0}", m_PlayerId);
        m_SecondaryVerticalAxis = string.Format("SecondaryVertical{0}", m_PlayerId); 
    }

    // Update is called once per frame
    void Update()
    {
        inputUpdate();
    }

    private void inputUpdate()
    {
        if (!_isSelected)
        {
            if (Input.GetAxis(m_HorizontalAxis) > 0
                || Input.GetAxis(m_VerticalAxis) > 0
                || Input.GetAxis(m_SecondaryHorizontalAxis) > 0
                || Input.GetAxis(m_SecondaryVerticalAxis) > 0
                )
            {
                _isSelected = true;
                GetComponent<Image>().color = Color.white;
                EventBus.PlayerShouldToStart.Dispatch(m_PlayerId);
            }
        }
    }
}
