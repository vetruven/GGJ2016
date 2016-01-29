using UnityEngine;
using System.Collections;

[AddComponentMenu("Player/Player Stats")]
public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    [Range(1, 4)]
    private int m_PlayerId = 1;

    public int PlayerId { get { return m_PlayerId; } }
}
