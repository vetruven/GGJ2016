using UnityEngine;
using System.Collections;

public class PanelManager : MonoBehaviour
{

    [SerializeField] private GameObject _playerchoosingPanel;
    [SerializeField] private GameObject _endGamePanel;

    void Awake()
    {
        EventBus.StartGame.AddListener(StartGame);
        EventBus.EndGame.AddListener(EndGame);
        EventBus.RestartGame.AddListener(RestartGame);

        _playerchoosingPanel.SetActive(true);
        _endGamePanel.SetActive(false);
    }

    public void StartGame()
    {
        _playerchoosingPanel.SetActive(false);
    }

    public void RestartGame()
    {
        _playerchoosingPanel.SetActive(true);
        _endGamePanel.SetActive(false);
    }

    private void EndGame()
    {
        _endGamePanel.SetActive(true);
    }
}
