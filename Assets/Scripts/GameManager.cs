﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> _playerPrefabs;
    [SerializeField] private Vector3 _playerCreationRange = new Vector3(600,600,0);

    private List<bool> playerQueue;
 
    void Awake()
    {
        EventBus.PlayerWantToStart.AddListener(AddPlayerToPlayQueue);
        EventBus.GameLost.AddListener(EndGame);

        playerQueue = new List<bool>() { false, false, false, false};
    }

    private void AddPlayerToPlayQueue(int playerId)
    {
        playerQueue[playerId] = true;
    }

    public void StartGame()
    {
        CreatePlayers();

        EventBus.StartGame.Dispatch();
        EventBus.UpdateBar.Dispatch();
    }

    public void EndGame()
    {
        
        foreach (var virgin in FindObjectsOfType<VirginController>())
        {
            Destroy(virgin.gameObject);
        }

        foreach (var player in FindObjectsOfType<PlayerMovement>())
        {
            Destroy(player.gameObject);
        }

        playerQueue = new List<bool>() { false, false, false, false };
        EventBus.EndGame.Dispatch();
    }

    public void RestartGame()
    {
        EventBus.RestartGame.Dispatch();
    }

    private void CreatePlayers()
    {
        for (int i = 0; i < 4; i++)
        {
            if (playerQueue[i])
            {
                Vector3 pos = _playerCreationRange;
                pos.Scale(Random.onUnitSphere);
                Instantiate(_playerPrefabs[i], pos, Quaternion.identity);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            StartGame();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            EndGame();
        }
    }
	
}
