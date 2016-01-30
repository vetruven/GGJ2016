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
        EventBus.RestartGame.Dispatch();
    }

    private void EndGame()
    {
        _endGamePanel.SetActive(true);
    }
}
