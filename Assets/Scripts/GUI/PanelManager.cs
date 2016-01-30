using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{

    [SerializeField] private GameObject _hungerBar;
    [SerializeField] private GameObject _playerchoosingPanel;
    [SerializeField] private GameObject _endGamePanel;
    [SerializeField] private Text _virginzAmount;
    [SerializeField] private Text _timeElapsed;

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private NpcManager _npcManager;

    void Awake()
    {
        EventBus.StartGame.AddListener(StartGame);
        EventBus.EndGame.AddListener(EndGame);
        EventBus.RestartGame.AddListener(RestartGame);

        _playerchoosingPanel.SetActive(true);
        _endGamePanel.SetActive(false);
        _hungerBar.SetActive(false);
    }

    public void StartGame()
    {
        _playerchoosingPanel.SetActive(false);
        _hungerBar.SetActive(true);
    }

    public void RestartGame()
    {
        _playerchoosingPanel.SetActive(true);
        _endGamePanel.SetActive(false);
    }

    private void EndGame()
    {
        _endGamePanel.SetActive(true);
        _virginzAmount.text = _npcManager.TotalVirginDeaths.ToString("0");
        _timeElapsed.text = _gameManager.GameTime.ToString("0");
        _hungerBar.SetActive(false);
    }
}
